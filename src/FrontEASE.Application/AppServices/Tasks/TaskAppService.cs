using AutoMapper;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Services.Tasks;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
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

        public async Task<TaskDto> Load(Guid id)
        {
            var taskEntity = await _taskService.Load(id);
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            var refreshedOptions = await _taskService.RefreshOptions(taskEntity);
            taskDto.Config.AvailableModules = _mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);

            return taskDto;
        }

        public async Task<TaskDto> Refresh(TaskDto task)
        {
            var taskEntity = _mapper.Map<Domain.Entities.Tasks.Task>(task);

            var refreshedOptions = await _taskService.RefreshOptions(taskEntity);
            task.Config.AvailableModules = _mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);
            return task;
        }

        public async Task<IList<TaskInfoDto>> LoadAll()
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail);

            var taskEntities = (user is not null && user.UserRole?.RoleId == _appSettings.AuthSettings?.Defaults?.Roles?.SuperadminGuid?.ToString()) ?
                await _taskService.LoadAll(null) :
                await _taskService.LoadAllBase(Guid.Parse(user!.Id));

            var taskInfoDtos = _mapper.Map<IList<TaskInfoDto>>(taskEntities);
            return taskInfoDtos;
        }


        public async Task<IList<TaskStatusDto>> LoadAllStatuses()
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail);

            var taskEntities = await _taskService.LoadAllBase(Guid.Parse(user!.Id));
            var taskStatusDtos = _mapper.Map<IList<TaskStatusDto>>(taskEntities);
            return taskStatusDtos;
        }

        public async Task<TaskDto> Update(TaskDto task)
        {
            var taskEntity = _mapper.Map<Domain.Entities.Tasks.Task>(task);

            taskEntity = await _taskService.Update(taskEntity);
            var updatedDto = _mapper.Map<TaskDto>(taskEntity);
            return updatedDto;
        }

        public async Task<TaskDto> Create()
        {
            var insertedEntity = new Domain.Entities.Tasks.Task();

            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail);
            insertedEntity.AuthorID = Guid.Parse(user!.Id);

            insertedEntity = await _taskService.Create(insertedEntity);
            await _taskService.RefreshOptions(insertedEntity);

            var insertedDto = _mapper.Map<TaskDto>(insertedEntity);
            return insertedDto;
        }

        public async Task<IList<TaskDto>> Duplicate(Guid id, TaskDuplicateActionRequestDto request)
        {
            var duplicatedEntity = await _taskService.Load(id);
            var duplicates = new List<Domain.Entities.Tasks.Task>();

            for(int i = 0; i < request.Copies; ++i)
            {
                duplicatedEntity = await _taskService.Duplicate(duplicatedEntity, $"{request.Name}_{i}");
                duplicates.Add(duplicatedEntity);
            }

            var duplicatesDto = _mapper.Map<IList<TaskDto>>(duplicates);
            return duplicatesDto;
        }

        public async Task<IList<TaskBulkActionResultDto>> Delete(IList<Guid> ids)
        {
            var resultList = new List<TaskBulkActionResultDto>();
            var deletedEntities = await _taskService.Load(ids);

            foreach (var taskID in ids)
            {
                var deletedEntity = deletedEntities.FirstOrDefault(x => x.ID == taskID);
                var result = new TaskBulkActionResultDto { TaskID = taskID };
                try
                {
                    await _taskService.Delete(deletedEntity!);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.ExceptionMessage = ex.Message;
                }
                resultList.Add(result);
            }
            return resultList;
        }

        public async Task<IList<TaskBulkActionResultDto>> ChangeState(IList<Guid> ids, TaskState state)
        {
            var resultList = new List<TaskBulkActionResultDto>();
            var modifiedEntities = await _taskService.Load(ids);

            foreach (var taskID in ids)
            {
                var modifiedTask = modifiedEntities.FirstOrDefault(x => x.ID == taskID);
                var result = new TaskBulkActionResultDto { TaskID = taskID };
                try
                {
                    await _taskService.ChangeState(modifiedTask!, state);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.ExceptionMessage = ex.Message;
                }
            }
            return resultList;
        }
    }
}
