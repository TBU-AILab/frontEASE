using AutoMapper;
using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Domain.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEASECoreService _coreService;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IEASECoreService coreService,
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

        public async Task<Entities.Tasks.Task> Load(Guid id)
        {
            var task = await _taskRepository.Load(id, GetFullQuery()) ?? throw new NotFoundException();
            SortConnectedModules(task);
            return task;
        }

        public async Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids)
        {
            var tasks = await _taskRepository.Load(ids, GetFullQuery()) ?? throw new NotFoundException();
            foreach (var task in tasks) { SortConnectedModules(task); }
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
            UpdateConnectedModules(updated);
            await _coreService.HandleTaskInit(updated);

            updated = await _taskRepository.Update(updated);
            SortConnectedModules(updated);
            return updated;
        }

        public async Task<Entities.Tasks.Task> Duplicate(Entities.Tasks.Task task, string baseName)
        {
            var newTask = new Entities.Tasks.Task();
            _mapper.Map(task, newTask);
            newTask.Config.Name = string.IsNullOrWhiteSpace(baseName) ? task.Config.Name : baseName;
            newTask.AuthorID = task.AuthorID;

            var connectedEntities = await SelectConnectedEntities(task);
            UpdateConnectedEntities(newTask, task, connectedEntities);
            UpdateConnectedModules(newTask);
            CleanTaskRunData(newTask);

            await _coreService.HandleTaskDuplicate(newTask, task.ID);
            var duplicated = await _taskRepository.Insert(newTask);

            return duplicated;
        }

        public async Task Delete(Entities.Tasks.Task task)
        {
            if (task.State == TaskState.RUN)
            {
                await ChangeState(task, TaskState.BREAK);
            }

            await _coreService.HandleTaskDelete(task);
            await _taskRepository.Delete(task);
        }

        public async Task ChangeState(Entities.Tasks.Task modifiedTask, TaskState state)
        {
            switch (state)
            {
                case TaskState.RUN:
                    await _coreService.ChangeTaskState(modifiedTask, TaskState.RUN);
                    break;
                case TaskState.STOP:
                    await _coreService.ChangeTaskState(modifiedTask, TaskState.STOP);
                    break;
                case TaskState.PAUSED:
                    await _coreService.ChangeTaskState(modifiedTask, TaskState.PAUSED);
                    break;
            }
            await _taskRepository.Update(modifiedTask);
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

        private void UpdateConnectedModules(Entities.Tasks.Task updatedTask)
        {
            void UpdateModule(TaskModuleEntity module, Guid taskConfigID, TaskConfig taskConfig)
            {
                module.TaskConfigID = taskConfigID;
                module.TaskConfig = taskConfig;

                foreach (var parameter in module.Parameters)
                {
                    var enumVal = parameter?.Value?.EnumValue;
                    if (enumVal?.ModuleValue is not null)
                    {
                        UpdateModule(enumVal.ModuleValue, taskConfigID, taskConfig);
                    }
                }
            }

            foreach (var module in updatedTask.Config.Modules)
            {
                UpdateModule(module, updatedTask.Config.ID, updatedTask.Config);
            }
        }

        private void SortConnectedModules(Entities.Tasks.Task task)
        {
            var sorted = new List<TaskModuleEntity>();
            var nestedIDs = new List<Guid>();

            foreach (var module in task.Config.Modules)
            {
                var paramsWithModuleVal = module.Parameters?.Select(x => x?.Value)?.Select(x => x?.EnumValue)?.Where(x => x?.ModuleValueID is not null)?.ToList() ?? [];
                foreach (var nestedModuleParam in paramsWithModuleVal)
                {
                    var moduleID = nestedModuleParam!.ModuleValueID;
                    nestedIDs.Add(moduleID!.Value);

                    var matchedModule = task.Config.Modules.SingleOrDefault(x => x.ID == moduleID);
                    nestedModuleParam.ModuleValue = matchedModule;
                }
            }

            if (nestedIDs.Count > 0)
            {
                var nestedModules = task.Config.Modules.Where(x => nestedIDs.Contains(x.ID));
                task.Config.Modules = task.Config.Modules.Except(nestedModules).ToList();
            }
        }

        #endregion
    }
}