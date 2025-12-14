using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Services.Tasks;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Tasks
{
    public class TaskAppService : ITaskAppService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public TaskAppService(
            AppSettings appSettings,
            ITaskService taskService,
            IUserService userService,
            IMapper mapper,
            IHttpContextAccessor contextAccessor)
        {
            _appSettings = appSettings;
            _userService = userService;
            _taskService = taskService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<TaskDto> Load(Guid id, CancellationToken cancellationToken)
        {
            var taskEntity = await _taskService.Load(id, cancellationToken);
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            var refreshedOptions = await _taskService.RefreshOptions(taskEntity, cancellationToken);
            taskDto.Config.AvailableModules = _mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);

            return taskDto;
        }

        public async Task<TaskDto> LoadSimple(Guid id, CancellationToken cancellationToken)
        {
            var taskEntity = await _taskService.LoadSimple(id, cancellationToken);
            var taskDto = _mapper.Map<TaskDto>(taskEntity);
            return taskDto;
        }

        public async Task<TaskDto> Refresh(TaskDto task, CancellationToken cancellationToken)
        {
            var taskEntity = _mapper.Map<Domain.Entities.Tasks.Task>(task);

            var emptyModules = new List<TaskModuleEntity>();
            foreach (var module in taskEntity.Config.Modules)
            {
                if (module.Parameters.Count == 0)
                {
                    emptyModules.Add(module);
                }
            }
            taskEntity.Config.Modules = [.. taskEntity.Config.Modules.Except(emptyModules)];

            var refreshedOptions = await _taskService.RefreshOptions(taskEntity, cancellationToken);
            task.Config.AvailableModules = _mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);
            return task;
        }

        public async Task<IList<TaskInfoDto>> LoadAll(TaskFilterActionRequestDto? filter, CancellationToken cancellationToken)
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail, cancellationToken);

            var appliedFilter = filter is null ? null : _mapper.Map<TaskFilterActionRequest>(filter);
            var taskEntities = user?.UserRole?.RoleId == _appSettings.AuthSettings?.Defaults?.Roles?.SuperadminGuid?.ToString() ?
                await _taskService.LoadAll(null, appliedFilter, cancellationToken) :
                await _taskService.LoadAll(Guid.Parse(user!.Id), appliedFilter, cancellationToken);


            var taskInfoDtos = _mapper.Map<IList<TaskInfoDto>>(taskEntities);
            return taskInfoDtos;
        }


        public async Task<IList<TaskStatusDto>> LoadAllStatuses(CancellationToken cancellationToken)
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail, cancellationToken);

            var taskEntities = await _taskService.LoadAllBase(Guid.Parse(user!.Id), null, cancellationToken);
            var taskStatusDtos = _mapper.Map<IList<TaskStatusDto>>(taskEntities);
            return taskStatusDtos;
        }

        public async Task<TaskDto> Update(TaskDto task, CancellationToken cancellationToken)
        {
            var taskEntity = _mapper.Map<Domain.Entities.Tasks.Task>(task);

            taskEntity = await _taskService.Update(taskEntity, cancellationToken);
            var updatedDto = _mapper.Map<TaskDto>(taskEntity);
            return updatedDto;
        }

        public async Task<TaskDto> Share(TaskDto task, CancellationToken cancellationToken)
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail, cancellationToken);
            var taskLocal = await _taskService.LoadSimple(task.ID, cancellationToken);

            if(taskLocal!.AuthorID != Guid.Parse(user!.Id))
            {
                throw new UnauthorizedException();
            }
            else
            {
                var taskEntity = _mapper.Map<Domain.Entities.Tasks.Task>(task);

                taskEntity = await _taskService.Share(taskEntity, cancellationToken);
                var updatedDto = _mapper.Map<TaskDto>(taskEntity);
                return updatedDto;
            }
        }

        public async Task<TaskDto> Create(CancellationToken cancellationToken)
        {
            var insertedEntity = new Domain.Entities.Tasks.Task();

            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail, cancellationToken);
            insertedEntity.AuthorID = Guid.Parse(user!.Id);

            insertedEntity = await _taskService.Create(insertedEntity, cancellationToken);
            await _taskService.RefreshOptions(insertedEntity, cancellationToken);

            var insertedDto = _mapper.Map<TaskDto>(insertedEntity);
            return insertedDto;
        }

        public async Task<IList<TaskDto>> Duplicate(Guid id, TaskDuplicateActionRequestDto request, CancellationToken cancellationToken)
        {
            var duplicatedEntity = await _taskService.Load(id, cancellationToken);
            var duplicates = await _taskService.Duplicate(duplicatedEntity, request.Name, request.Copies, cancellationToken);
            var duplicatesDto = _mapper.Map<IList<TaskDto>>(duplicates);
            return duplicatesDto;
        }

        public async Task Delete(IList<Guid> ids, CancellationToken cancellationToken)
        {
            var deletedEntities = await _taskService.Load(ids, cancellationToken);
            await _taskService.Delete(deletedEntities, cancellationToken);
        }

        public async Task ChangeState(IList<Guid> ids, TaskState state, CancellationToken cancellationToken)
        {
            var modifiedEntities = await _taskService.Load(ids, cancellationToken);
            await _taskService.ChangeState(modifiedEntities!, state, cancellationToken);
        }
    }
}
