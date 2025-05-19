using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Data.Enums.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskStatusesJob : JobBase, IJob
    {
        private readonly IMapper _mapper;
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;

        public string JobName { get; init; }

        public UpdateTaskStatusesJob(
            IMapper mapper,
            ICoreConnector coreService,
            ITaskRepository taskRepository,
            IJobLogRepository jobLogRepository,
            AppSettings appSettings)
            : base(jobLogRepository)
        {
            _mapper = mapper;
            _coreService = coreService;
            _taskRepository = taskRepository;

            JobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskStatusesJob!.CronName!;
        }

        public async Task Execute(PerformContext context)
        {
            var currentExecutionStart = DateTime.UtcNow;
            var log = await InsertJobLog(JobName, currentExecutionStart, null, false);
            var checkID = SentrySdk.CaptureCheckIn(JobName, CheckInStatus.InProgress);

            try
            {
                var query = new TasksQuery();
                var tasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && x.State == TaskState.RUN, query);
                var taskGuids = tasks.Select(x => x.ID).ToList();

                context.WriteLine($"Checking statuses for: {tasks.Count} items.");

                if (tasks.Count > 0)
                {
                    var stateResults = await _coreService.GetTaskStates();
                    foreach (var taskStateInfo in stateResults)
                    {
                        var matchingTask = tasks.SingleOrDefault(x => x.ID == taskStateInfo.ID);
                        if (matchingTask is not null)
                        {
                            context.WriteLine($"Mapping state for task: {matchingTask.ID}.");
                            _mapper.Map(taskStateInfo, matchingTask);

                            if (matchingTask.State != taskStateInfo.State)
                            {
                                context.WriteLine($"Task {matchingTask.ID} - Change state: {matchingTask.State} --> {taskStateInfo.State}.");
                            }
                        }
                    }
                    await _taskRepository.SaveChangesAsync();
                }

                await UpdateJobLog(log, DateTime.UtcNow, true);
                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} SUCCESS.");
                SentrySdk.CaptureCheckIn(JobName, CheckInStatus.Ok);
            }
            catch (Exception ex)
            {
                await UpdateJobLog(log, DateTime.UtcNow, false);
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                SentrySdk.CaptureCheckIn(JobName, CheckInStatus.Error);
            }
        }
    }
}
