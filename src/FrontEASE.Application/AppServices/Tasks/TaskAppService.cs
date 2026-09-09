using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Services.Tasks;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Data.Enums.Tasks.Config;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Tasks
{
    public class TaskAppService(
        AppSettings appSettings,
        ITaskService taskService,
        IUserService userService,
        IMapper mapper,
        IHttpContextAccessor contextAccessor) : ITaskAppService
    {
        public async Task<TaskDto> Load(Guid id, CancellationToken cancellationToken)
        {
            var taskEntity = await taskService.Load(id, cancellationToken);
            var taskDto = mapper.Map<TaskDto>(taskEntity);

            var refreshedOptions = await taskService.RefreshOptions(taskEntity, cancellationToken);
            taskDto.Config.AvailableModules = mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);

            return taskDto;
        }

        public async Task<TaskDto> LoadSimple(Guid id, CancellationToken cancellationToken)
        {
            var taskEntity = await taskService.LoadSimple(id, cancellationToken);
            var taskDto = mapper.Map<TaskDto>(taskEntity);
            return taskDto;
        }

        public async Task<TaskDto> Refresh(TaskDto task, CancellationToken cancellationToken)
        {
            var taskEntity = mapper.Map<Domain.Entities.Tasks.Task>(task);

            var emptyModules = new List<TaskModuleEntity>();
            foreach (var module in taskEntity.Config.Modules)
            {
                if (module.Parameters.Count == 0)
                {
                    emptyModules.Add(module);
                }
            }
            taskEntity.Config.Modules = [.. taskEntity.Config.Modules.Except(emptyModules)];

            var refreshedOptions = await taskService.RefreshOptions(taskEntity, cancellationToken);
            task.Config.AvailableModules = mapper.Map<IList<TaskModuleNoValidationDto>>(refreshedOptions);
            return task;
        }

        public async Task<IList<TaskInfoDto>> LoadAll(TaskFilterActionRequestDto? filter, CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);

            var appliedFilter = filter is null ? null : mapper.Map<TaskFilterActionRequest>(filter);
            var taskEntities = user?.UserRole?.RoleId == appSettings.AuthSettings?.Defaults?.Roles?.SuperadminGuid?.ToString() ?
                await taskService.LoadAll(null, appliedFilter, cancellationToken) :
                await taskService.LoadAll(Guid.Parse(user!.Id), appliedFilter, cancellationToken);


            var taskInfoDtos = mapper.Map<IList<TaskInfoDto>>(taskEntities);
            return taskInfoDtos;
        }


        public async Task<IList<TaskStatusDto>> LoadAllStatuses(CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);

            var taskEntities = await taskService.LoadAllBase(Guid.Parse(user!.Id), null, cancellationToken);
            var taskStatusDtos = mapper.Map<IList<TaskStatusDto>>(taskEntities);
            return taskStatusDtos;
        }

        public async Task<TaskDto> Update(TaskDto task, CancellationToken cancellationToken)
        {
            var taskEntity = mapper.Map<Domain.Entities.Tasks.Task>(task);

            taskEntity = await taskService.Update(taskEntity, cancellationToken);
            var updatedDto = mapper.Map<TaskDto>(taskEntity);
            return updatedDto;
        }

        public async Task<TaskBulkEditResultDto> BulkEdit(TaskBulkEditRequestDto request, CancellationToken cancellationToken)
        {
            var taskIDs = request.TaskIDs.Distinct().ToList();
            if (taskIDs.Count < 2)
            {
                throw new UnprocessableException(["Select at least two tasks to edit."]);
            }
            if (!request.Fields.HasChanges())
            {
                throw new UnprocessableException(["Select at least one change to apply."]);
            }

            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);
            var userID = Guid.Parse(user!.Id);
            var isSuperadmin = user.UserRole?.RoleId == appSettings.AuthSettings?.Defaults?.Roles?.SuperadminGuid?.ToString();

            if (!isSuperadmin)
            {
                var accessibleTaskIDs = (await taskService.LoadAllBase(userID, null, cancellationToken))
                    .Select(task => task.ID)
                    .ToHashSet();
                if (taskIDs.Any(taskID => !accessibleTaskIDs.Contains(taskID)))
                {
                    throw new UnauthorizedException();
                }
            }

            var tasks = await taskService.Load(taskIDs, cancellationToken);
            if (tasks.Count != taskIDs.Count)
            {
                throw new NotFoundException();
            }

            var nonEditable = tasks.Where(task => task.State is not TaskState.CREATED and not TaskState.INIT).ToList();
            if (nonEditable.Count > 0)
            {
                throw new UnprocessableException(
                    nonEditable.Select(task => $"Task {task.Config.Name} cannot be edited in state {task.State}.").ToList());
            }

            var template = mapper.Map<TaskConfig>(request.Template);
            foreach (var task in tasks)
            {
                ApplyBulkEdit(task.Config, template, request.Fields);
            }

            var updated = await taskService.BulkUpdate(tasks, cancellationToken);
            return new TaskBulkEditResultDto
            {
                Tasks = mapper.Map<IList<TaskInfoDto>>(updated)
            };
        }

        private void ApplyBulkEdit(TaskConfig target, TaskConfig template, TaskBulkEditFieldsDto fields)
        {
            if (fields.OptimizationGoal) { target.OptimizationGoal = template.OptimizationGoal; }
            if (fields.MaxContextSize) { target.MaxContextSize = template.MaxContextSize; }
            if (fields.FeedbackFromSolution) { target.FeedbackFromSolution = template.FeedbackFromSolution; }
            if (fields.SystemMessage) { target.SystemMessage = template.SystemMessage; }
            if (fields.InitialMessage) { target.InitialMessage = template.InitialMessage; }
            if (fields.RepeatedMessage) { mapper.Map(template.RepeatedMessage, target.RepeatedMessage); }

            ApplySingleModule(target, template, fields.Connector, ModuleType.LLM_CONNECTOR);
            ApplySingleModule(target, template, fields.Evaluator, ModuleType.EVALUATOR);
            ApplySingleModule(target, template, fields.Solution, ModuleType.SOLUTION);

            UpsertModules(target, template, fields.Tests, ModuleType.TEST);
            UpsertModules(target, template, fields.StoppingConditions, ModuleType.STOPPING_CONDITION);
            UpsertModules(target, template, fields.Analyses, ModuleType.ANALYSIS);
            UpsertModules(target, template, fields.Stats, ModuleType.STATISTIC);
        }

        private void ApplySingleModule(TaskConfig target, TaskConfig template, bool selected, ModuleType moduleType)
        {
            if (!selected) { return; }

            var source = template.Modules.FirstOrDefault(module => module.PackageType == moduleType);
            if (source is null || string.IsNullOrWhiteSpace(source.ShortName))
            {
                throw new UnprocessableException([$"Select a {moduleType} module to apply."]);
            }

            var destination = target.Modules.FirstOrDefault(module => module.PackageType == moduleType);
            if (destination is null)
            {
                target.Modules.Add(mapper.Map<TaskModuleEntity>(source));
            }
            else
            {
                mapper.Map(source, destination);
            }
        }

        private void UpsertModules(TaskConfig target, TaskConfig template, bool selected, ModuleType moduleType)
        {
            if (!selected) { return; }

            var sources = template.Modules
                .Where(module => module.PackageType == moduleType && !string.IsNullOrWhiteSpace(module.ShortName))
                .GroupBy(module => module.ShortName, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.Last())
                .ToList();

            if (sources.Count == 0)
            {
                throw new UnprocessableException([$"Add at least one {moduleType} module to apply."]);
            }

            foreach (var source in sources)
            {
                var destination = target.Modules.FirstOrDefault(module =>
                    module.PackageType == moduleType &&
                    module.ShortName.Equals(source.ShortName, StringComparison.OrdinalIgnoreCase));

                if (destination is null)
                {
                    target.Modules.Add(mapper.Map<TaskModuleEntity>(source));
                }
                else
                {
                    mapper.Map(source, destination);
                }
            }
        }

        public async Task<TaskDto> Share(TaskDto task, CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);
            var taskLocal = await taskService.LoadSimple(task.ID, cancellationToken);

            if(taskLocal!.AuthorID != Guid.Parse(user!.Id))
            {
                throw new UnauthorizedException();
            }
            else
            {
                var taskEntity = mapper.Map<Domain.Entities.Tasks.Task>(task);

                taskEntity = await taskService.Share(taskEntity, cancellationToken);
                var updatedDto = mapper.Map<TaskDto>(taskEntity);
                return updatedDto;
            }
        }

        public async Task<TaskDto> Create(CancellationToken cancellationToken)
        {
            var insertedEntity = new Domain.Entities.Tasks.Task();

            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);
            insertedEntity.AuthorID = Guid.Parse(user!.Id);

            insertedEntity = await taskService.Create(insertedEntity, cancellationToken);
            await taskService.RefreshOptions(insertedEntity, cancellationToken);

            var insertedDto = mapper.Map<TaskDto>(insertedEntity);
            return insertedDto;
        }

        public async Task<IList<TaskDto>> Duplicate(Guid id, TaskDuplicateActionRequestDto request, CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);
            var currentUserID = Guid.Parse(user!.Id);

            var duplicatedEntity = await taskService.Load(id, cancellationToken);

            var isOwner = duplicatedEntity.AuthorID == currentUserID;
            var isSuperadmin = user?.UserRole?.RoleId == appSettings.AuthSettings?.Defaults?.Roles?.SuperadminGuid?.ToString();
            var preserveLinkedEntities = isOwner || isSuperadmin;

            var duplicates = await taskService.Duplicate(duplicatedEntity, request.Name, request.Copies, currentUserID, preserveLinkedEntities, cancellationToken);
            var duplicatesDto = mapper.Map<IList<TaskDto>>(duplicates);
            return duplicatesDto;
        }

        public async Task Delete(IList<Guid> ids, CancellationToken cancellationToken)
        {
            var deletedEntities = await taskService.Load(ids, cancellationToken);
            await taskService.Delete(deletedEntities, cancellationToken);
        }

        public async Task ChangeState(IList<Guid> ids, TaskState state, CancellationToken cancellationToken)
        {
            var modifiedEntities = await taskService.Load(ids, cancellationToken);
            await taskService.ChangeState(modifiedEntities!, state, cancellationToken);
        }
    }
}
