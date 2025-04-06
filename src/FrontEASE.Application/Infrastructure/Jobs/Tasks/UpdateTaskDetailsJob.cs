using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskDetailsJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly IEASECoreService _coreService;
        private readonly ITaskRepository _taskRepository;

        private readonly string _jobName;

        public UpdateTaskDetailsJob(
            IMapper mapper,
            IEASECoreService coreService,
            ITaskRepository taskRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _coreService = coreService;
            _taskRepository = taskRepository;

            _jobName = appSettings.HangfireSettings!.Jobs!.UpdateTaskDetailsJob!.CronName!;
        }

        public async Task Execute(PerformContext context)
        {
            var checkID = SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.InProgress);
            var lastMessage = await _taskRepository.LoadLastMessage();
            var lastExecution = lastMessage?.DateCreated;

            try
            {
                var newTaskData = await _coreService.GetTaskRunData(lastExecution);
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
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Ok, checkID);
            }
            catch (Exception ex)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(UpdateTaskDetailsJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                SentrySdk.CaptureCheckIn(_jobName, CheckInStatus.Error, checkID);
            }
        }
    }
}
