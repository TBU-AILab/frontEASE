using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Helpers;
using FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.List.Helpers;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Client.Contexts.Specific.Tasks
{
    public class TaskModuleParamContext
    {
        public TaskModuleParamContext(
            string paramName,
            TaskModuleParameterNoValidationDto paramOption,
            TaskModuleParameterDto? paramValue,
            ParameterType? paramType)
        {
            ParamOption = paramOption;
            ParamValue = paramValue;
            ParamName = paramName;
            ParamType = paramType;

            ParamID = Guid.NewGuid();
        }

        public Guid ParamID { get; set; }
        public TaskModuleParameterNoValidationDto ParamOption { get; set; }
        public TaskModuleParameterDto? ParamValue { get; set; }
        public TaskModuleParamFlags? ParamFlags { get; set; }
        public TaskModuleListParamFlags? ListFlags { get; set; }
        public string ParamName { get; set; }
        public ParameterType? ParamType { get; set; }
    }
}
