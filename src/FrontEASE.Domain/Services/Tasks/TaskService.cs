using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;

namespace FrontEASE.Domain.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICoreConnector _coreService;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ICoreConnector coreService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _companyRepository = companyRepository;
            _coreService = coreService;
            _mapper = mapper;
        }

        private static TasksQuery GetFullQuery()
        {
            var query = new TasksQuery()
            {
                IncludeGroups = true,
                IncludeMembers = true,
                LoadConfig = true,
                LoadConfigModules = true,
                LoadConfigRepeatedMessage = true,
                LoadMessages = true,
                LoadSolutions = true,
            };
            return query;
        }

        private static TasksQuery GetBaseQuery()
        {
            var query = new TasksQuery()
            {
                IncludeMembers = true,
                LoadConfig = true,
                LoadConfigRepeatedMessage = true,
                LoadMessages = true,
                LoadSolutions = true,
            };
            return query;
        }

        private static TasksQuery GetAccessQuery()
        {
            var query = new TasksQuery()
            {
                IncludeMembers = true,
                IncludeGroups = true,
            };
            return query;
        }

        public async Task<Entities.Tasks.Task> Load(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.Load(id, GetFullQuery(), cancellationToken) ?? throw new NotFoundException();
            return task;
        }

        public async Task<Entities.Tasks.Task> LoadSimple(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.Load(id, GetBaseQuery(), cancellationToken) ?? throw new NotFoundException();
            return task;
        }

        public async Task<Entities.Tasks.Task> LoadAccess(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.Load(id, GetAccessQuery(), cancellationToken) ?? throw new NotFoundException();
            return task;
        }

        public async Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.Load(ids, GetFullQuery(), cancellationToken) ?? throw new NotFoundException();
            return tasks;
        }

        public async Task<IList<Entities.Tasks.Task>> LoadAll(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.LoadInfo(userID, filter, cancellationToken);
            return tasks;
        }

        public async Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.LoadInfoBase(userID, filter, cancellationToken);
            return tasks;
        }

        public async Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task, CancellationToken cancellationToken)
        {
            var author = await _userRepository.Load(task.AuthorID, cancellationToken);
            task.Members.Add(author!);

            await _coreService.HandleTaskCreate(task, cancellationToken);
            var inserted = await _taskRepository.Insert(task, cancellationToken);
            return inserted!;
        }

        public async Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task, CancellationToken cancellationToken)
        {
            await _coreService.RefreshTaskOptions(task, cancellationToken);
            return task.Config.AvailableModules;
        }

        public async Task<Entities.Tasks.Task> Share(Entities.Tasks.Task task, CancellationToken cancellationToken)
        {
            var updated = await LoadAccess(task.ID, cancellationToken);
            var connectedEntities = await SelectConnectedEntities(task, cancellationToken);
            UpdateConnectedEntities(updated, connectedEntities);

            updated = await _taskRepository.Update(updated, cancellationToken);
            return updated;
        }

        public async Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task, CancellationToken cancellationToken)
        {
            var updated = await Load(task.ID, cancellationToken);
            var connectedEntities = await SelectConnectedEntities(task, cancellationToken);

            _mapper.Map(task, updated);
            UpdateConnectedEntities(updated, connectedEntities);
            await _coreService.HandleTaskInit(updated, cancellationToken);

            updated = await _taskRepository.Update(updated, cancellationToken);
            return updated;
        }

        public async Task<IList<Entities.Tasks.Task>> Duplicate(Entities.Tasks.Task task, string baseName, int copies, CancellationToken cancellationToken)
        {
            var duplicates = new List<Entities.Tasks.Task>();
            for (int i = 0; i < copies; i++)
            {
                var newTask = new Entities.Tasks.Task();
                _mapper.Map(task, newTask);
                newTask.AuthorID = task.AuthorID;

                var connectedEntities = await SelectConnectedEntities(task, cancellationToken);
                UpdateConnectedEntities(newTask, connectedEntities);
                CleanTaskRunData(newTask);

                duplicates.Add(newTask);
            }

            await _coreService.HandleTaskDuplicate(duplicates, task.ID, baseName, copies, cancellationToken);
            var duplicated = await _taskRepository.InsertRange(duplicates, true, cancellationToken);
            return duplicated;
        }

        public async Task Delete(IList<Entities.Tasks.Task> tasks, CancellationToken cancellationToken)
        {
            var runningTasks = tasks.Where(x => x.State == TaskState.RUN).ToList();
            if (runningTasks.Count > 0)
            {
                await ChangeState(runningTasks, TaskState.BREAK, cancellationToken);
            }

            await _coreService.HandleTaskDelete(tasks, cancellationToken);
            await _taskRepository.DeleteRange(tasks, false, true, cancellationToken);
        }

        public async Task ChangeState(IList<Entities.Tasks.Task> modifiedTasks, TaskState state, CancellationToken cancellationToken)
        {
            switch (state)
            {
                case TaskState.RUN:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.RUN, cancellationToken);
                    break;
                case TaskState.STOP:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.STOP, cancellationToken);
                    break;
                case TaskState.PAUSED:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.PAUSED, cancellationToken);
                    break;
            }
            await _taskRepository.UpdateRange(modifiedTasks, cancellationToken);
        }

        private async Task<(IList<ApplicationUser> Users, IList<Company> Companies)> SelectConnectedEntities(Entities.Tasks.Task sourceTask, CancellationToken cancellationToken)
        {
            var linkedUserIDs = sourceTask.Members.Select(x => x.Id);
            var linkedUsers = await _userRepository.LoadWhere(x => linkedUserIDs.Contains(x.Id), cancellationToken);

            var linkedCompanyIDs = sourceTask.MemberGroups.Select(x => x.ID);
            var linkedCompanies = await _companyRepository.LoadWhere(x => linkedCompanyIDs.Contains(x.ID), cancellationToken);

            return new(linkedUsers, linkedCompanies);
        }

        #region Task Data Operations

        private static void UpdateConnectedEntities(Entities.Tasks.Task updatedTask, (IList<ApplicationUser> Users, IList<Company> Companies) connectedEntities)
        {
            updatedTask.MemberGroups.Clear();
            updatedTask.Members.Clear();

            updatedTask.Members.AddRange(connectedEntities.Users);
            updatedTask.MemberGroups.AddRange(connectedEntities.Companies);
        }

        private static void CleanTaskRunData(Entities.Tasks.Task task)
        {
            task.Config.AvailableModules.Clear();

            task.State = TaskState.CREATED;
            task.CurrentIteration = task.IterationsValid = task.IterationsInvalidConsecutive = 0;

            task.Messages.Clear();
            task.Solutions.Clear();
        }

        #endregion
    }
}