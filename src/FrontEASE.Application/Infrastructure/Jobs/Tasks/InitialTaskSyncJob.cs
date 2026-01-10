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
    public class InitialTaskSyncJob(
        IMapper mapper,
        ICoreConnector coreService,
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        IJobLogRepository jobLogRepository,
        AppSettings appSettings) : JobBase(jobLogRepository), IJob
    {

        public string JobName { get; init; } = appSettings.HangfireSettings!.Jobs!.InitialTaskSyncJob!.CronName!;

        private async Task DeleteTasksMissingInCore(PerformContext context, IEnumerable<Guid> coreTaskIDs, CancellationToken cancellationToken)
        {
            context.WriteLine($"Delete executing...");

            var tasksToDelete = await taskRepository.LoadAllWhere(x => !coreTaskIDs.Contains(x.ID), new TasksQuery(), cancellationToken);
            await taskRepository.DeleteRange(tasksToDelete, true, false, cancellationToken);

            context.WriteLine($"Deleted {tasksToDelete.Count} items from the database.");
        }

        private async Task UpdateTasksExistingInBoth(PerformContext context, IList<TaskFullCoreDto> coreTaskData, IEnumerable<Guid> coreTaskIDs, CancellationToken cancellationToken)
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

            var existingTasks = await taskRepository.LoadAllWhere(x => !x.IsDeleted && coreTaskIDs.Contains(x.ID), query, cancellationToken);
            var authorsToBind = coreTaskData.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
            var superadmins = await userRepository.LoadWhere(x => x.UserRole!.RoleId == appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString(), cancellationToken);
            var authorsMatching = await userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()), cancellationToken);

            var updatesPerformed = 0;
            foreach (var coreTask in coreTaskData)
            {
                var taskId = coreTask.TaskInfo!.ID!.Value;
                var matchingTask = existingTasks.FirstOrDefault(x => x.ID == taskId);
                if (matchingTask is not null)
                {
                    ++updatesPerformed;
                    mapper.Map(coreTask.TaskInfo, matchingTask);

                    matchingTask.Messages = mapper.Map<IList<TaskMessage>>(coreTask.TaskData?.Messages);
                    matchingTask.Solutions = mapper.Map<IList<TaskSolution>>(coreTask.TaskData?.Solutions);

                    coreTask.TaskConfig?.Modules?.Clear();
                    matchingTask.Config = mapper.Map<TaskConfig>(coreTask.TaskConfig);
                    matchingTask.Config.Modules = mapper.Map<IList<TaskModuleEntity>>(mapper.Map<IList<TaskModule>>(coreTask.TaskModules));

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

        private async Task InsertTasksMissingInDatabase(PerformContext context, IList<TaskFullCoreDto> coreTaskData, CancellationToken cancellationToken)
        {
            context.WriteLine($"Insert executing...");

            var taskIDsInDatabase = (await taskRepository.LoadAllWhere(null, new TasksQuery(), cancellationToken)).Select(x => x.ID);
            var tasksToAdd = coreTaskData.Where(x => !taskIDsInDatabase.Contains(x.TaskInfo!.ID!.Value));

            var authorsToBind = tasksToAdd.Select(x => x.TaskConfig?.Author?.ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
            var superadmins = await userRepository.LoadWhere(x => x.UserRole!.RoleId == appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid.ToString(), cancellationToken);
            var authorsMatching = await userRepository.LoadWhere(x => !string.IsNullOrWhiteSpace(x.Email) && authorsToBind.Contains(x.Email.ToUpper()), cancellationToken);

            var tasksForInsertion = new List<Domain.Entities.Tasks.Task>();
            foreach (var coreTask in tasksToAdd)
            {
                var newTask = mapper.Map<Domain.Entities.Tasks.Task>(coreTask.TaskInfo);
                newTask.Messages = mapper.Map<IList<TaskMessage>>(coreTask.TaskData?.Messages);
                newTask.Solutions = mapper.Map<IList<TaskSolution>>(coreTask.TaskData?.Solutions);

                coreTask.TaskConfig?.Modules?.Clear();
                newTask.Config = mapper.Map<TaskConfig>(coreTask.TaskConfig);
                newTask.Config.Modules = mapper.Map<IList<TaskModuleEntity>>(mapper.Map<IList<TaskModule>>(coreTask.TaskModules));

                var author = authorsMatching.FirstOrDefault(x => x.Email!.Equals(coreTask.TaskConfig?.Author, StringComparison.InvariantCultureIgnoreCase)) ?? superadmins.FirstOrDefault();
                newTask.AuthorID = Guid.Parse(author!.Id);
                newTask.Members.Add(author);

                tasksForInsertion.Add(newTask);
            }

            await taskRepository.InsertRange(tasksForInsertion, false, cancellationToken);
            context.WriteLine($"Inserted {tasksForInsertion.Count} items into the database.");
        }

        public async Task Execute(PerformContext context)
        {
            var currentExecutionStart = DateTime.UtcNow;
            var cancellationToken = context.CancellationToken.ShutdownToken;
            var log = await InsertJobLog(JobName, currentExecutionStart, null, false, cancellationToken);
            var checkID = SentrySdk.CaptureCheckIn(JobName, CheckInStatus.InProgress);

            await using var transaction = await taskRepository.BeginTransactionAsync(cancellationToken);

            try
            {
                var coreTaskData = (await coreService.GetTasksFullData(cancellationToken)).Where(x => x.TaskInfo?.ID is not null).ToList();
                var coreTaskIDs = coreTaskData.Select(x => x.TaskInfo!.ID!.Value).Distinct();
                var databaseTaskCount = await taskRepository.LoadTaskCount(cancellationToken);

                context.WriteLine($"Syncing {coreTaskData.Count} items from Core with {databaseTaskCount} items in the database.");
                var updatePerformed = false;
                if (databaseTaskCount != 0)
                {
                    await DeleteTasksMissingInCore(context, coreTaskIDs, cancellationToken);
                    updatePerformed = true;
                }

                if (coreTaskData.Count != 0)
                {
                    await UpdateTasksExistingInBoth(context, coreTaskData, coreTaskIDs, cancellationToken);
                    await InsertTasksMissingInDatabase(context, coreTaskData, cancellationToken);
                    updatePerformed = true;
                }

                if (updatePerformed)
                {
                    await taskRepository.SaveChangesAsync(cancellationToken);
                }

                await UpdateJobLog(log, DateTime.UtcNow, true, cancellationToken);
                context.WriteLine($"{JobName} SUCCESS.");

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await UpdateJobLog(log, DateTime.UtcNow, false, cancellationToken);
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine($"{JobName} FAILED: {ex.Message}");
                new BackgroundJobClient().ChangeState(context.BackgroundJob.Id, new FailedState(ex));
                throw;
            }
        }
    }
}