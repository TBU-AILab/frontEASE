using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Data.Enums.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskStatusesJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;

        private readonly string _jobName;

        public UpdateTaskStatusesJob(
            IMapper mapper,
            ICoreConnector coreService,
            ITaskRepository taskRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _coreService = coreService;
            _taskRepository = taskRepository;

            _jobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskStatusesJob!.CronName!;
        }

        public async Task Execute(PerformContext context)
        {
            var checkID = SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.InProgress);

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

                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} SUCCESS.");
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Ok);
            }
            catch (Exception ex)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(UpdateTaskStatusesJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Error);
            }
        }
    }
}
