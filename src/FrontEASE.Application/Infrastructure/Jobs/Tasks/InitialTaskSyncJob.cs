using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Core.Connector;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;

namespace FrontEASE.Application.Infrastructure.Jobs.Tasks
{
    public class InitialTaskSyncJob : JobBase, IJob
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;

        public string JobName { get; init; }

        public InitialTaskSyncJob(
            IMapper mapper,
            ICoreConnector coreService,
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            IJobLogRepository jobLogRepository,
            AppSettings appSettings)
            : base(jobLogRepository)
        {
            _appSettings = appSettings;
            _mapper = mapper;
            _userRepository = userRepository;
            _coreService = coreService;
            _taskRepository = taskRepository;

            JobName = appSettings.HangfireSettings!.Jobs!.InitialTaskSyncJob!.CronName!;
        }


        private async Task DeleteTasksMissingInCore(PerformContext context, IEnumerable<Guid> coreTaskIDs)
        {
            context.WriteLine($"Delete executing...");

            var tasksToDelete = await _taskRepository.LoadAllWhere(x => !coreTaskIDs.Contains(x.ID), new TasksQuery());
            await _taskRepository.DeleteRange(tasksToDelete, true, false);

            context.WriteLine($"Deleted {tasksToDelete.Count} items from the database.");
        }

        private async Task UpdateTasksExistingInBoth(PerformContext context, IList<TaskFullCoreDto> coreTaskData, IEnumerable<Guid> coreTaskIDs, IList<TaskModuleCoreDto> moduleOptions)
        {
            context.WriteLine($"Update executing...");

            var query = new TasksQuery()
            {
                LoadConfig = true,
                LoadConfigModules = true,
                LoadConfigRepeatedMessage = true,
                LoadMessages = true,
                LoadSolutions = true,
                IncludeMembers = true,
            };

            var existingTasks = await _taskRepository.LoadAllWhere(x => !x.IsDeleted && coreTaskIDs.Contains(x.ID), query);
            var authorsToBind = coreTaskData.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
            var superadmins = await _userRepository.LoadWhere(x => x.UserRole!.RoleId == _appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString());
            var authorsMatching = await _userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()));

            var updatesPerformed = 0;
            foreach (var coreTask in coreTaskData)
            {
                var taskId = coreTask.TaskInfo!.ID!.Value;
                var matchingTask = existingTasks.FirstOrDefault(x => x.ID == taskId);
                if (matchingTask is not null)
                {
                    ++updatesPerformed;
                    _mapper.Map(coreTask.TaskInfo, matchingTask);

                    matchingTask.Messages = _mapper.Map<IList<TaskMessage>>(coreTask.TaskData?.Messages);
                    matchingTask.Solutions = _mapper.Map<IList<TaskSolution>>(coreTask.TaskData?.Solutions);

                    coreTask.TaskConfig?.Modules?.Clear();
                    matchingTask.Config = _mapper.Map<TaskConfig>(coreTask.TaskConfig);
                    matchingTask.Config.Modules = _mapper.Map<IList<TaskModuleEntity>>(_mapper.Map<IList<TaskModule>>(coreTask.TaskModules));

                    var author = authorsMatching.FirstOrDefault(x => x.Email!.Equals(coreTask.TaskConfig?.Author, StringComparison.InvariantCultureIgnoreCase)) ?? superadmins.FirstOrDefault();
                    matchingTask.AuthorID = Guid.Parse(author!.Id);
                    if (!matchingTask.Members.Any(x => x.Id == author!.Id))
                    {
                        matchingTask.Members.Add(author);
                    }
                }
            }

            context.WriteLine($"Updated {updatesPerformed} matching items.");
        }

        private async Task InsertTasksMissingInDatabase(PerformContext context, IList<TaskFullCoreDto> coreTaskData, IList<TaskModuleCoreDto> moduleOptions)
        {
            context.WriteLine($"Insert executing...");

            var taskIDsInDatabase = (await _taskRepository.LoadAllWhere(null, new TasksQuery())).Select(x => x.ID);
            var tasksToAdd = coreTaskData.Where(x => !taskIDsInDatabase.Contains(x.TaskInfo!.ID!.Value));

            var authorsToBind = tasksToAdd.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
            var superadmins = await _userRepository.LoadWhere(x => x.UserRole!.RoleId == _appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString());
            var authorsMatching = await _userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()));

            /* TODO - mapping */
            var tasksForInsertion = new List<Domain.Entities.Tasks.Task>();
            foreach (var coreTask in tasksToAdd)
            {
                var newTask = _mapper.Map<Domain.Entities.Tasks.Task>(coreTask.TaskInfo);
                newTask.Messages = _mapper.Map<IList<TaskMessage>>(coreTask.TaskData?.Messages);
                newTask.Solutions = _mapper.Map<IList<TaskSolution>>(coreTask.TaskData?.Solutions);

                coreTask.TaskConfig?.Modules?.Clear();
                newTask.Config = _mapper.Map<TaskConfig>(coreTask.TaskConfig);
                newTask.Config.Modules = _mapper.Map<IList<TaskModuleEntity>>(_mapper.Map<IList<TaskModule>>(coreTask.TaskModules));

                var author = authorsMatching.FirstOrDefault(x => x.Email!.Equals(coreTask.TaskConfig?.Author, StringComparison.InvariantCultureIgnoreCase)) ?? superadmins.FirstOrDefault();
                newTask.AuthorID = Guid.Parse(author!.Id);
                newTask.Members.Add(author);

                tasksForInsertion.Add(newTask);
            }

            await _taskRepository.InsertRange(tasksForInsertion, false);
            context.WriteLine($"Inserted {tasksForInsertion.Count} items into the database.");
        }

        public async Task Execute(PerformContext context)
        {
            var currentExecutionStart = DateTime.UtcNow;
            var log = await InsertJobLog(JobName, currentExecutionStart, null, false);
            var checkID = SentrySdk.CaptureCheckIn(JobName, CheckInStatus.InProgress);

            try
            {
                var coreTaskData = (await _coreService.GetTasksFullData()).Where(x => x.TaskInfo?.ID is not null).ToList();
                var coreTaskOptions = await _coreService.GetModuleTypes();
                var coreTaskIDs = coreTaskData.Select(x => x.TaskInfo!.ID!.Value).Distinct();
                var databaseTaskCount = await _taskRepository.LoadTaskCount();

                context.WriteLine($"Syncing {coreTaskData.Count} items from Core with {databaseTaskCount} items in the database.");
                var updatePerformed = false;
                if (databaseTaskCount != 0)
                {
                    await DeleteTasksMissingInCore(context, coreTaskIDs);
                    updatePerformed = true;
                }

                if (coreTaskData.Count != 0)
                {
                    await UpdateTasksExistingInBoth(context, coreTaskData, coreTaskIDs, coreTaskOptions);
                    await InsertTasksMissingInDatabase(context, coreTaskData, coreTaskOptions);
                    updatePerformed = true;
                }

                if (updatePerformed)
                {
                    await _taskRepository.SaveChangesAsync();
                }

                await UpdateJobLog(log, DateTime.UtcNow, true);
                context.WriteLine($"{nameof(InitialTaskSyncJob)} SUCCESS.");
            }
            catch (Exception ex)
            {
                await UpdateJobLog(log, DateTime.UtcNow, false);
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{nameof(InitialTaskSyncJob)} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
            }
        }
    }
}