using AutoMapper;
using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Entities.Tasks.Configs;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;
using FoP_IMT.Domain.Infrastructure.Exceptions.Types;
using FoP_IMT.Domain.Repositories.Companies;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Domain.Repositories.Users;
using FoP_IMT.Domain.Services.Tasks.Core;
using FoP_IMT.Shared.Data.Enums.Tasks;

namespace FoP_IMT.Domain.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITaskCoreService _taskCoreService;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ITaskCoreService taskCoreService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _companyRepository = companyRepository;
            _taskCoreService = taskCoreService;
            _mapper = mapper;
        }

        public async Task<Entities.Tasks.Task> Load(Guid id)
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

            var task = await _taskRepository.Load(id, query) ?? throw new NotFoundException();
            SortConnectedModules(task);
            return task;
        }

        public async Task<IList<Entities.Tasks.Task>> LoadAll(Guid userID) => await _taskRepository.LoadInfo(userID);

        public async Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid userID) => await _taskRepository.LoadInfoBase(userID);

        public async Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task)
        {
            var author = await _userRepository.Load(task.AuthorID);
            task.Members.Add(author!);

            await _taskCoreService.HandleTaskCreate(task);
            var inserted = await _taskRepository.Insert(task);
            return inserted!;
        }

        public async Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task)
        {
            await _taskCoreService.RefreshTaskOptions(task);
            return task.Config.AvailableModules;
        }

        public async Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task)
        {
            var updated = await Load(task.ID);
            var connectedEntities = await SelectConnectedEntities(task);

            _mapper.Map(task, updated);
            UpdateConnectedEntities(updated, task, connectedEntities);
            UpdateConnectedModules(updated);
            await _taskCoreService.HandleTaskInit(updated);

            updated = await _taskRepository.Update(updated);
            SortConnectedModules(updated);
            return updated;
        }

        public async Task<Entities.Tasks.Task> Duplicate(Entities.Tasks.Task task)
        {
            var newTask = new Entities.Tasks.Task();
            _mapper.Map(task, newTask);
            newTask.Config.Name = $"{task.Config.Name} - Copy";
            newTask.AuthorID = task.AuthorID;

            var connectedEntities = await SelectConnectedEntities(task);
            UpdateConnectedEntities(newTask, task, connectedEntities);
            UpdateConnectedModules(newTask);
            CleanTaskRunData(newTask);

            await _taskCoreService.HandleTaskDuplicate(newTask, task.ID);
            var duplicated = await _taskRepository.Insert(newTask);

            return duplicated;
        }

        public async Task Delete(Entities.Tasks.Task task)
        {
            if (task.State == TaskState.RUN)
            {
                await ChangeState(task, TaskState.BREAK);
            }

            await _taskCoreService.HandleTaskDelete(task);
            await _taskRepository.Delete(task);
        }

        public async Task ChangeState(Entities.Tasks.Task modifiedTask, TaskState state)
        {
            switch (state)
            {
                case TaskState.RUN:
                    await _taskCoreService.ChangeTaskState(modifiedTask, TaskState.RUN);
                    break;
                case TaskState.STOP:
                    await _taskCoreService.ChangeTaskState(modifiedTask, TaskState.STOP);
                    break;
                case TaskState.PAUSED:
                    await _taskCoreService.ChangeTaskState(modifiedTask, TaskState.PAUSED);
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
