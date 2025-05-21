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

        private TasksQuery GetFullQuery()
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

        private TasksQuery GetBaseQuery()
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

        public async Task<Entities.Tasks.Task> Load(Guid id)
        {
            var task = await _taskRepository.Load(id, GetFullQuery()) ?? throw new NotFoundException();
            return task;
        }

        public async Task<Entities.Tasks.Task> LoadSimple(Guid id)
        {
            var task = await _taskRepository.Load(id, GetBaseQuery()) ?? throw new NotFoundException();
            return task;
        }

        public async Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids)
        {
            var tasks = await _taskRepository.Load(ids, GetFullQuery()) ?? throw new NotFoundException();
            return tasks;
        }

        public async Task<IList<Entities.Tasks.Task>> LoadAll(Guid? userID, TaskFilterActionRequest? filter)
        {
            var tasks = await _taskRepository.LoadInfo(userID, filter);
            return tasks;
        }

        public async Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid? userID, TaskFilterActionRequest? filter)
        {
            var tasks = await _taskRepository.LoadInfoBase(userID, filter);
            return tasks;
        }

        public async Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task)
        {
            var author = await _userRepository.Load(task.AuthorID);
            task.Members.Add(author!);

            await _coreService.HandleTaskCreate(task);
            var inserted = await _taskRepository.Insert(task);
            return inserted!;
        }

        public async Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task)
        {
            await _coreService.RefreshTaskOptions(task);
            return task.Config.AvailableModules;
        }

        public async Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task)
        {
            var updated = await Load(task.ID);
            var connectedEntities = await SelectConnectedEntities(task);

            _mapper.Map(task, updated);
            UpdateConnectedEntities(updated, task, connectedEntities);
            await _coreService.HandleTaskInit(updated);

            updated = await _taskRepository.Update(updated);
            return updated;
        }

        public async Task<IList<Entities.Tasks.Task>> Duplicate(Entities.Tasks.Task task, string baseName, int copies)
        {
            var duplicates = new List<Entities.Tasks.Task>();
            for (int i = 0; i < copies; i++)
            {
                var newTask = new Entities.Tasks.Task();
                _mapper.Map(task, newTask);
                newTask.Config.Name = string.IsNullOrWhiteSpace(baseName) ? $"{task.Config.Name}_{i + 1}" : $"{baseName}_{i + 1}";
                newTask.AuthorID = task.AuthorID;

                var connectedEntities = await SelectConnectedEntities(task);
                UpdateConnectedEntities(newTask, task, connectedEntities);
                CleanTaskRunData(newTask);

                duplicates.Add(newTask);
            }

            await _coreService.HandleTaskDuplicate(duplicates, task.ID, baseName, copies);
            var duplicated = await _taskRepository.InsertRange(duplicates, true);
            return duplicated;
        }

        public async Task Delete(IList<Entities.Tasks.Task> tasks)
        {
            var runningTasks = tasks.Where(x => x.State == TaskState.RUN).ToList();
            if (runningTasks.Count > 0)
            {
                await ChangeState(runningTasks, TaskState.BREAK);
            }

            await _coreService.HandleTaskDelete(tasks);
            await _taskRepository.DeleteRange(tasks, false, true);
        }

        public async Task ChangeState(IList<Entities.Tasks.Task> modifiedTasks, TaskState state)
        {
            switch (state)
            {
                case TaskState.RUN:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.RUN);
                    break;
                case TaskState.STOP:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.STOP);
                    break;
                case TaskState.PAUSED:
                    await _coreService.ChangeTaskState(modifiedTasks, TaskState.PAUSED);
                    break;
            }
            await _taskRepository.UpdateRange(modifiedTasks);
        }

        private async Task<(IList<ApplicationUser> Users, IList<Company> Companies)> SelectConnectedEntities(Entities.Tasks.Task sourceTask)
        {
            var linkedUserIDs = sourceTask.Members.Select(x => x.Id);
            var linkedUsers = await _userRepository.LoadWhere(x => linkedUserIDs.Contains(x.Id));

            var linkedCompanyIDs = sourceTask.MemberGroups.Select(x => x.ID);
            var linkedCompanies = await _companyRepository.LoadWhere(x => linkedCompanyIDs.Contains(x.ID));

            return new(linkedUsers, linkedCompanies);
        }

        #region Task Data Operations

        private void UpdateConnectedEntities(Entities.Tasks.Task updatedTask, Entities.Tasks.Task incomingTask, (IList<ApplicationUser> Users, IList<Company> Companies) connectedEntities)
        {
            updatedTask.MemberGroups.Clear();
            updatedTask.Members.Clear();
            foreach (var user in connectedEntities.Users) { updatedTask.Members.Add(user); }
            foreach (var company in connectedEntities.Companies) { updatedTask.MemberGroups.Add(company); }
        }

        private void CleanTaskRunData(Entities.Tasks.Task task)
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