using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.List.Helpers;
using FrontEASE.Client.Pages.Tasks.Overview.Components.Sections.Config.Components.Modules.Params.Helpers;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks
{
    public interface ITaskManipulationService
    {
        void AddTaskRepeatedMessageItem(TaskDto task);
        void RemoveTaskRepeatedMessageItem(TaskDto task, TaskConfigRepeatedMessageItemDto item);

        void AddModule(IList<TaskModuleDto> modules, ModuleType moduleType);
        void RemoveModule(IList<TaskModuleDto> modules, TaskModuleDto item);
        void SwapSelectedModule(TaskModuleNoValidationDto? source, TaskModuleDto destination);

        bool UpdateTaskStatuses(IList<TaskInfoDto> tasks, IList<TaskStatusDto> taskStatuses);

        void CleanUsersInfo(TaskDto task);
        void CleanCompaniesInfo(TaskDto task);
        void AssignTaskImages(TaskDto task, IList<CompanyDto> availableCompanies, IList<ApplicationUserDto> availableUsers);

        (IList<ApplicationUserDto> PreservedMembers, IList<CompanyDto> PreservedGroups) PrepareTaskRequest(TaskDto task, bool cleanImages, bool cleanOptions);
        void PrepareTaskFilter(TaskFilterActionRequestDto filter);

        (bool DefaultValuePresent, string? DefaultValue) ExtractDefaultValue(TaskModuleParameterNoValidationDto? parameter);
        void FillParamDefaultValue(TaskModuleParameterDto parameter, string? defaultValue);
        bool CheckDescriptionPresent(TaskModuleParameterNoValidationDto? paramOption, TaskModuleParameterDto paramValue);
        bool RemoveListParameter(TaskModuleParameterListOptionParamsDto listParam, TaskModuleParameterDto paramValue);
        TaskModuleParameterNoValidationDto? GetListValueParamOption(string shortName, TaskModuleParameterNoValidationDto paramOption);


        TaskModuleParamFlags GetParamFlags(TaskModuleDto? module, TaskModuleParameterNoValidationDto paramOption, TaskModuleParameterDto paramValue);
        ListParamFlags GetListParamFlags(TaskModuleParameterNoValidationDto paramOption, TaskModuleParameterDto paramVal);
        string GetListParamInternalDescription(TaskModuleParameterNoValidationDto paramOption, TaskModuleParameterDto paramVal);
    }
}
