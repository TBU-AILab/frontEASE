using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Application.Infrastructure.Jobs;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;
using FrontEASE.Domain.Services.Core;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class InitialTaskSyncJob : IJob
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEASECoreService _coreService;
        private readonly ITaskRepository _taskRepository;

        public InitialTaskSyncJob(
            IMapper mapper,
            IEASECoreService coreService,
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            AppSettings appSettings)
        {
            _appSettings = appSettings;
            _mapper = mapper;
            _userRepository = userRepository;
            _coreService = coreService;
            _taskRepository = taskRepository;
        }


        private async Task DeleteTasksMissingInCore(IList<Guid> coreTaskIDs)
        {
            var tasksToDelete = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && !coreTaskIDs.Contains(x.ID), new TasksQuery());
            await _taskRepository.DeleteRange(tasksToDelete, true, false);
        }

        private async Task UpdateTasksExistingInBoth(IList<TaskFullCoreDto> coreTaskData, IList<Guid> coreTaskIDs, IList<TaskModuleCoreDto> moduleOptions)
        {
            var query = new TasksQuery()
            {
                LoadConfig = true,
                LoadConfigModules = true,
                LoadConfigRepeatedMessage = true,
                LoadMessages = true,
                LoadSolutions = true
            };
            var existingTasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && coreTaskIDs.Contains(x.ID), query);
            var authorsToBind = coreTaskData.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            var superadmins = await _userRepository.LoadWhere(x => x.UserRole!.RoleId == _appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString());
            var authorsMatching = await _userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()));

            foreach (var coreTask in coreTaskData)
            {
                var taskId = coreTask.TaskInfo!.ID!.Value;
                var matchingTask = existingTasks.FirstOrDefault(x => x.ID == taskId);
                if (matchingTask is not null)
                {
                    _mapper.Map(coreTask.TaskInfo, matchingTask);

                    matchingTask.Messages = _mapper.Map<IList<TaskMessage>>(coreTask.TaskData?.Messages);
                    matchingTask.Solutions = _mapper.Map<IList<TaskSolution>>(coreTask.TaskData?.Solutions);
                    matchingTask.Config = _mapper.Map<TaskConfig>(coreTask.TaskConfig);
                }
            }
        }

        private async Task InsertTasksMissingInDatabase(IList<TaskFullCoreDto> coreTaskData, IList<TaskModuleCoreDto> moduleOptions)
        {
            var taskIDsInDatabase = (await _taskRepository.LoadAllWhere(null, new TasksQuery())).Select(x => x.ID).ToList();
            var tasksToAdd = coreTaskData.Where(x => !taskIDsInDatabase.Contains(x.TaskInfo!.ID!.Value));

            var authorsToBind = tasksToAdd.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            var superadmins = await _userRepository.LoadWhere(x => x.UserRole!.RoleId == _appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString());
            var authorsMatching = await _userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()));

            /* TODO - mapping */
            var tasksForInsertion = new List<Domain.Entities.Tasks.Task>();
            foreach (var task in tasksToAdd)
            {
                var newTask = _mapper.Map<Domain.Entities.Tasks.Task>(task.TaskInfo);
                newTask.Messages = _mapper.Map<IList<TaskMessage>>(task.TaskData?.Messages);
                newTask.Solutions = _mapper.Map<IList<TaskSolution>>(task.TaskData?.Solutions);
                newTask.Config = _mapper.Map<TaskConfig>(task.TaskConfig);

                var author = authorsMatching.FirstOrDefault(x => x.Email!.Equals(task.TaskConfig?.Author, StringComparison.InvariantCultureIgnoreCase)) ?? superadmins.FirstOrDefault();

                newTask.AuthorID = Guid.Parse(author!.Id);
                newTask.Members.Add(author);

                tasksForInsertion.Add(newTask);
            }

            await _taskRepository.InsertRange(tasksForInsertion, false);
        }

        public async Task Execute(PerformContext context)
        {
            try
            {
                var coreTaskData = (await _coreService.GetTasksFullData()).Where(x => x.TaskInfo?.ID is not null).ToList();
                var coreTaskOptions = await _coreService.GetModuleTypes();
                var coreTaskIDs = coreTaskData.Select(x => x.TaskInfo!.ID!.Value).Distinct().ToList();
                var databaseTaskCount = await _taskRepository.LoadTaskCount();

                context.WriteLine($"Syncing {coreTaskData.Count} items from Core with {databaseTaskCount} items in the database.");

                bool updatePerformed = false;
                if (databaseTaskCount != 0)
                {
                    await DeleteTasksMissingInCore(coreTaskIDs);
                    updatePerformed = true;
                }

                if (coreTaskData.Count != 0)
                {
                    //await UpdateTasksExistingInBoth(coreTaskData, coreTaskIDs, coreTaskOptions);
                    await InsertTasksMissingInDatabase(coreTaskData, coreTaskOptions);
                    updatePerformed = true;
                }

                if (updatePerformed)
                {
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