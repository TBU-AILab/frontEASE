using AutoMapper;
using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using FoP_IMT.Domain.Entities.Tasks.Solutions;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Domain.Services.Tasks.Core;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FoP_IMT.Application.Infrastructure.Jobs.Tasks
{
    public class UpdateTaskDetailsJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly ITaskCoreService _taskCoreService;
        private readonly ITaskRepository _taskRepository;

        private readonly string _jobName;

        public UpdateTaskDetailsJob(
            IMapper mapper,
            ITaskCoreService taskCoreService,
            ITaskRepository taskRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _taskCoreService = taskCoreService;
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
                var newTaskData = await _taskCoreService.GetTaskRunData(lastExecution);
                context.WriteLine($"Checking messages, runs, solutions for: {newTaskData.Count} items.");

                var taskIDs = newTaskData.Select(x => x.Key);

                var query = new TasksQuery()
                {
                    LoadSolutions = true,
                    LoadMessages = true
                };

                var tasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && taskIDs.Contains(x.ID), query);

                foreach (var newTaskInfo in newTaskData)
                {
                    var matchingTask = tasks.SingleOrDefault(x => x.ID == newTaskInfo.Key);
                    if (matchingTask is not null)
                    {
                        var messages = _mapper.Map<IList<TaskMessage>>(newTaskInfo.Value.Messages);
                        var solutions = _mapper.Map<IList<TaskSolution>>(newTaskInfo.Value.Solutions);

                        context.WriteLine($"Task {matchingTask.ID} - Messages: {messages.Count} | Solutions: {solutions.Count}.");
                        foreach (var message in messages)
                        {
                            var messageIDs = matchingTask.Messages.Select(x => x.ID);
                            if (!messageIDs.Contains(message.ID))
                            {
                                matchingTask.Messages.Add(message);
                            }
                        }
                        foreach (var solution in solutions)
                        {
                            var solutionIDs = matchingTask.Solutions.Select(x => x.ID);
                            if (!solutionIDs.Contains(solution.ID))
                            {
                                matchingTask.Solutions.Add(solution);
                            }
                        }
                    }
                }
                await _taskRepository.SaveChangesAsync();

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
