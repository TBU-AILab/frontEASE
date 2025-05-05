using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks.Config;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Utils.Helpers.Tasks;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks
{
    public class TaskManipulationService : ITaskManipulationService
    {
        private readonly IMapper _mapper;

        public TaskManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddTaskRepeatedMessageItem(TaskDto task) => task.Config.RepeatedMessage.RepeatedMessageItems.Add(new());
        public void RemoveTaskRepeatedMessageItem(TaskDto task, TaskConfigRepeatedMessageItemDto item) => task.Config.RepeatedMessage.RepeatedMessageItems.Remove(item);

        public void AddModule(IList<TaskModuleDto> modules, ModuleType moduleType) => modules.Add(new() { PackageType = moduleType });
        public void RemoveModule(IList<TaskModuleDto> modules, TaskModuleDto item) => modules.Remove(item);

        public void PrepareTaskFilter(TaskFilterActionRequestDto filter)
        {
            var states = filter.SelectedFilterStates.Select(x => x) ?? [];

            filter.State.Clear();
            foreach (var state in states)
            {
                filter.State.Add(state);
            }
        }

        public void PrepareTaskRequest(TaskDto task, bool cleanImages, bool cleanOptions)
        {
            if (cleanImages)
            {
                foreach (var user in task.Members)
                { user.Image = null; }

                foreach (var group in task.MemberGroups)
                {
                    group.Image = null;
                    foreach (var user in group.Users)
                    { user.Image = null; }
                }
            }
            if (cleanOptions)
            {
                task.Config.AvailableModules.Clear();
            }
        }

        public void SwapSelectedModule(TaskModuleNoValidationDto? source, TaskModuleDto destination)
        {
            if (!string.IsNullOrWhiteSpace(source?.ShortName))
            {
                _mapper.Map(source, destination);
            }
            else
            {
                _mapper.Map(new TaskModuleDto(), destination);
            }
        }

        public bool UpdateTaskStatuses(IList<TaskInfoDto> tasks, IList<TaskStatusDto> taskStatuses)
        {
            bool updated = false;
            foreach (var task in tasks)
            {
                var matchingStatus = taskStatuses.FirstOrDefault(status => status.ID == task.ID);
                if (matchingStatus is not null && task.State != matchingStatus.State)
                {
                    task.State = matchingStatus.State;
                    updated = true;
                }
            }

            return updated;
        }

        public void CleanUsersInfo(TaskDto task)
        {
            if (task.Author is not null)
            {
                var author = task.Members.SingleOrDefault(x => x.Id == task.Author.Id);
                task.Members.Clear();

                if (author is not null)
                {
                    task.Members.Add(author);
                }
            }
        }

        public void CleanCompaniesInfo(TaskDto task) => task.MemberGroups.Clear();

        public (bool DefaultValuePresent, string? DefaultValue) ExtractDefaultValue(TaskModuleParameterNoValidationDto parameter)
        {
            var defaultValuePresent = parameter.Default is not null && parameter.Readonly != true &&
                                      (!string.IsNullOrWhiteSpace(parameter.Default?.StringValue) ||
                                       parameter.Default?.FloatValue is not null ||
                                       parameter.Default?.IntValue is not null ||
                                       parameter.Default?.BoolValue is not null ||
                                       !string.IsNullOrWhiteSpace(parameter.Default?.EnumValue?.StringValue));

            var defaultValue = parameter.Default switch
            {
                { StringValue: { Length: > 0 } } => parameter.Default.StringValue,
                { FloatValue: not null } => parameter.Default.FloatValue?.ToString(),
                { IntValue: not null } => parameter.Default.IntValue?.ToString(),
                { BoolValue: not null } => parameter.Default.BoolValue?.ToString(),
                { EnumValue.StringValue: { Length: > 0 } } => parameter.Default.EnumValue.StringValue,
                _ => null
            };

            return (defaultValuePresent, defaultValue);
        }

        public void FillParamDefaultValue(TaskModuleParameterDto parameter, string? defaultValue)
        {
            var parameterType = DynamicParamUtils.GetParameterType(parameter.Type);
            if (parameterType is null || string.IsNullOrWhiteSpace(defaultValue)) { return; }

            switch (parameterType)
            {
                case ParameterType.INT:
                    parameter.Value!.IntValue = int.Parse(defaultValue);
                    break;

                case ParameterType.FLOAT:
                    parameter.Value!.FloatValue = float.Parse(defaultValue);
                    break;

                case ParameterType.STR:
                case ParameterType.MARKDOWN:
                    parameter.Value!.StringValue = defaultValue;
                    break;

                case ParameterType.BOOL:
                    parameter.Value!.BoolValue = bool.Parse(defaultValue);
                    break;

                case ParameterType.ENUM:
                    var enumVal = new TaskModuleParameterEnumOptionDto() { StringValue = defaultValue };
                    _mapper.Map(enumVal, parameter.Value!.EnumValue);
                    break;

                default:
                    break;
            }
        }

        public bool CheckDescriptionPresent(TaskModuleParameterNoValidationDto paramOption, TaskModuleParameterDto paramValue)
        {
            return (!string.IsNullOrWhiteSpace(paramOption.Description) ||
                (paramOption.EnumDescriptions?.Count > 0 && !string.IsNullOrWhiteSpace(paramValue.Value!.EnumValue?.StringValue))) && paramOption.Readonly != true;
        }

        public void RemoveListParameter(IList<TaskModuleParameterDto> listParam, TaskModuleParameterDto paramValue) =>
            paramValue.Value!.ListValue!.ParameterValues.Remove(listParam);

        public TaskModuleParameterNoValidationDto? GetListValueParamOption(string shortName, TaskModuleParameterNoValidationDto paramOption) =>
            paramOption.Default?.ListValue?.ParameterValues?.FirstOrDefault()?.FirstOrDefault(x => x.ShortName == shortName);
    }
}
