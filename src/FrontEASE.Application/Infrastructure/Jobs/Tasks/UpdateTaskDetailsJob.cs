using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskDetailsJob : JobBase, IJob
    {
        private readonly IMapper _mapper;
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;

        public string JobName { get; init; }

        public UpdateTaskDetailsJob(
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

            JobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskDetailsJob!.CronName!;
        }

        public async Task Execute(PerformContext context)
        {
            var currentExecutionStart = DateTime.UtcNow;
            var cancellationToken = context.CancellationToken.ShutdownToken;
            var log = await InsertJobLog(JobName, currentExecutionStart, null, false, cancellationToken);
            var checkID = SentrySdk.CaptureCheckIn(JobName, CheckInStatus.InProgress);

            var lastExecutedLog = await _jobLogRepository.LoadLastSuccessful(JobName, cancellationToken);
            context.WriteLine($"Last successful execution:{(lastExecutedLog is null ? "N/A" : $"{lastExecutedLog.DateStart} - {lastExecutedLog.DateEnd}")}");

            await using var transaction = await _taskRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var queryDetailsCheck = new TasksQuery()
                {
                    LoadSolutions = true,
                    LoadMessages = true
                };
                var tasksForDataSync = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && x.State == TaskState.RUN, queryDetailsCheck, cancellationToken);
                var tasksForDataSyncIDs = tasksForDataSync.Select(x => x.ID).ToList();

                context.WriteLine($"Checking info, statuses, messages and solutions for: {tasksForDataSync.Count} items.");

                if (tasksForDataSync.Count > 0)
                {
                    var infoResults = await _coreService.GetTaskStates(tasksForDataSyncIDs, cancellationToken);
                    var dataResults = await _coreService.GetTaskRunData(tasksForDataSyncIDs, null, cancellationToken);

                    foreach (var taskStateInfo in infoResults)
                    {
                        var matchingTask = tasksForDataSync.SingleOrDefault(x => x.ID == taskStateInfo.ID);
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

                    foreach (var taskDataInfo in dataResults)
                    {
                        var matchingTask = tasksForDataSync.SingleOrDefault(x => x.ID == taskDataInfo.ID);
                        if (matchingTask is not null)
                        {
                            var presentSolutions = matchingTask.Solutions.Select(x => x.TaskMessageID);
                            var presentMessages = matchingTask.Messages.Select(x => x.ID);

                            var missingSolutions = taskDataInfo.Solutions.Where(x => !presentSolutions.Contains(x.TaskMessageID)).ToList();
                            var missingMessages = taskDataInfo.Messages.Where(x => !presentMessages.Contains(x.ID)).ToList();

                            context.WriteLine($"Mapping {missingSolutions.Count} new solutions and {missingMessages.Count} new messages for task: {matchingTask.ID}.");
                            var messagesToBeAdded = _mapper.Map<IList<TaskMessage>>(missingMessages);
                            var solutionsToBeAdded = _mapper.Map<IList<TaskSolution>>(missingSolutions);

                            matchingTask.Messages.AddRange(messagesToBeAdded);
                            matchingTask.Solutions.AddRange(solutionsToBeAdded);
                        }
                    }
                    await _taskRepository.SaveChangesAsync(cancellationToken);
                }

                await UpdateJobLog(log, DateTime.UtcNow, true, cancellationToken);
                context.WriteLine($"{JobName} SUCCESS.");
                SentrySdk.CaptureCheckIn(JobName, CheckInStatus.Ok);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await UpdateJobLog(log, DateTime.UtcNow, false, cancellationToken);
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{JobName} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                SentrySdk.CaptureCheckIn(JobName, CheckInStatus.Error, checkID);
                throw;
            }
        }
    }
}
