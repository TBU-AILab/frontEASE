using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core.Connector;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskDetailsJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;
        private readonly IJobLogRepository _jobLogRepository;

        private readonly string _jobName;

        public UpdateTaskDetailsJob(
            IMapper mapper,
            ICoreConnector coreService,
            ITaskRepository taskRepository,
            IJobLogRepository jobLogRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _coreService = coreService;
            _taskRepository = taskRepository;
            _jobLogRepository = jobLogRepository;

            _jobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskDetailsJob!.CronName!;
        }

        private async Task InsertJobLog(DateTime dateStart, DateTime dateEnd, bool success)
        {
            var log = new JobLog()
            {
                DateStart = dateStart,
                DateEnd = dateEnd,
                Success = success,
                JobName = _jobName,
            };

            await _jobLogRepository.Insert(log);
        }

        private async Task ExecuteStrayItemsCleanup(PerformContext context, JobLog? lastExecutedLog)
        {
            context.WriteLine($"Pre-check and cleanup running...");

            var messages = lastExecutedLog is null ? await _taskRepository.LoadAllMessagesWhere(null) : await _taskRepository.LoadAllMessagesWhere(x => x.DateCreated > lastExecutedLog.DateStart);
            var solutions = messages!.Select(x => x.TaskSolution)?.ToList() ?? [];

            await _taskRepository.DeleteRange(messages!, false);
            await _taskRepository.DeleteRange(solutions!, false);
            await _taskRepository.SaveChangesAsync();

            context.WriteLine($"Pre-check and cleanup done.");
        }

        public async Task Execute(PerformContext context)
        {
            var checkID = SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.InProgress);
            var lastExecutedLog = await _jobLogRepository.LoadLastSuccessful(_jobName);
            context.WriteLine($"Last successful execution:{(lastExecutedLog is null ? "N/A" : $"{lastExecutedLog.DateStart} - {lastExecutedLog.DateEnd}")}");

            var currentExecutionStart = DateTime.UtcNow;
            await ExecuteStrayItemsCleanup(context, lastExecutedLog);

            try
            {
                var newTaskData = await _coreService.GetTaskRunData(lastExecutedLog?.DateStart);
                if (newTaskData.Count > 0)
                {
                    context.WriteLine($"Checking messages, runs, solutions for: {newTaskData.Count} items.");
                    var taskIDs = newTaskData.Select(x => x.ID);

                    var query = new TasksQuery()
                    {
                        LoadSolutions = true,
                        LoadMessages = true
                    };

                    var tasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && taskIDs.Contains(x.ID), query);

                    foreach (var newTaskInfo in newTaskData)
                    {
                        var matchingTask = tasks.SingleOrDefault(x => x.ID == newTaskInfo.ID);
                        if (matchingTask is not null)
                        {
                            var messages = _mapper.Map<IList<TaskMessage>>(newTaskInfo.Messages);
                            var solutions = _mapper.Map<IList<TaskSolution>>(newTaskInfo.Solutions);
                            context.WriteLine($"Task {matchingTask.ID} - Messages: {messages.Count} | Solutions: {solutions.Count}.");

                            foreach (var message in messages)
                            {
                                if (!matchingTask.Messages.Any(x => x.ID == message.ID))
                                {
                                    message.TaskID = matchingTask.ID;
                                    message.Task = matchingTask;
                                    matchingTask.Messages.Add(message);
                                }
                            }
                            foreach (var solution in solutions)
                            {
                                if (!matchingTask.Solutions.Any(x => x.ID == solution.ID))
                                {
                                    solution.TaskID = matchingTask.ID;
                                    solution.Task = matchingTask;
                                    matchingTask.Solutions.Add(solution);
                                }
                            }
                        }
                    }
                    await _taskRepository.SaveChangesAsync();
                }

                context.WriteLine($"{nameof(UpdateTaskDetailsJob)} SUCCESS.");

                await InsertJobLog(currentExecutionStart, DateTime.UtcNow, true);
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Ok, checkID);
            }
            catch (Exception ex)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(UpdateTaskDetailsJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));

                await InsertJobLog(currentExecutionStart, DateTime.UtcNow, false);
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Error, checkID);
            }
        }
    }
}
