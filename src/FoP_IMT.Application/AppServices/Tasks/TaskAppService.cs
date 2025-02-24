using AutoMapper;
using FoP_IMT.Domain.Services.Tasks;
using FoP_IMT.Domain.Services.Users;
using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;
using FoP_IMT.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FoP_IMT.Application.AppServices.Tasks
{
    public class TaskAppService : ITaskAppService
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public TaskAppService(
            ITaskService taskService,
            IUserService userService,
            IMapper mapper,
            IHttpContextAccessor contextAccessor)
        {
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

            var taskEntities = await _taskService.LoadAll(Guid.Parse(user!.Id));
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

        public async Task<TaskDto> Duplicate(Guid id)
        {
            var duplicatedEntity = await _taskService.Load(id);
            duplicatedEntity = await _taskService.Duplicate(duplicatedEntity);

            var duplicatedDto = _mapper.Map<TaskDto>(duplicatedEntity);
            return duplicatedDto;
        }

        public async Task Delete(Guid id)
        {
            var deletedEntity = await _taskService.Load(id);
            await _taskService.Delete(deletedEntity);
        }

        public async Task ChangeState(Guid id, TaskState state)
        {
            var modifiedTask = await _taskService.Load(id);
            await _taskService.ChangeState(modifiedTask, state);
        }
    }
}
