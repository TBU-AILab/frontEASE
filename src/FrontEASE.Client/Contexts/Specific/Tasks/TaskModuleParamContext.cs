using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Helpers;
using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.List.Helpers;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Client.Contexts.Specific.Tasks
{
    public class TaskModuleParamContext(
        string paramName,
        TaskModuleParameterNoValidationDto paramOption,
        TaskModuleParameterDto? paramValue,
        ParameterType? paramType)
    {
        public Guid ParamID { get; set; } = Guid.NewGuid();
        public TaskModuleParameterNoValidationDto ParamOption { get; set; } = paramOption;
        public TaskModuleParameterDto? ParamValue { get; set; } = paramValue;
        public TaskModuleParamFlags? ParamFlags { get; set; }
        public TaskModuleListParamFlags? ListFlags { get; set; }
        public string ParamName { get; set; } = paramName;
        public ParameterType? ParamType { get; set; } = paramType;
    }
}
