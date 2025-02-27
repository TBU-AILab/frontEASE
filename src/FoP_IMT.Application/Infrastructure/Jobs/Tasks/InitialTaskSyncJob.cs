using AutoMapper;
using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Domain.Services.Tasks.Core;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FoP_IMT.Application.Infrastructure.Jobs.Tasks
{
    public class InitialTaskSyncJob : IJob
    {
        private readonly IMapper _mapper;
        private readonly ITaskCoreService _taskCoreService;
        private readonly ITaskRepository _taskRepository;

        public InitialTaskSyncJob(
            IMapper mapper,
            ITaskCoreService taskCoreService,
            ITaskRepository taskRepository,
            AppSettings appSettings)
        {
            _mapper = mapper;
            _taskCoreService = taskCoreService;
            _taskRepository = taskRepository;
        }

        public async Task Execute(PerformContext context)
        {
            try
            {
                var newTaskData = await _taskCoreService.GetTaskInfos();
                var taskCount = await _taskRepository.LoadTaskCount();

                if (newTaskData.Any())
                {
                    context.WriteLine($"Syncing {newTaskData.Count} items from Core with {taskCount} items in the database.");
                    var taskIDs = newTaskData.Select(x => x.ID);

                    var query = new TasksQuery();
                    var tasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted, query);

                    var tasksToDelete = tasks.Where(x => !taskIDs.Contains(x.ID)).ToList();
                    await _taskRepository.HardDeleteRange(tasksToDelete, false);

                    foreach (var newTaskInfo in newTaskData)
                    {
                        var matchingTask = tasks.SingleOrDefault(x => x.ID == newTaskInfo.ID);
                        if (matchingTask is not null)
                        {
                            _mapper.Map(newTaskInfo, matchingTask);
                        }
                    }
                    await _taskRepository.SaveChangesAsync();
                }

                context.WriteLine($"{nameof(InitialTaskSyncJob)} SUCCESS.");
            }
            catch (Exception ex)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(InitialTaskSyncJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
            }
        }
    }
}
