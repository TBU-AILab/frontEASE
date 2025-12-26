using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Helpers
{
    public class TaskModuleParamFlags
    {
        public ParameterType? ParamType { get; set; }
        public string? ParamName { get; set; }
        public string? ParamDefaultValue { get; set; }
        public bool SkipLabel { get; set; }
        public bool IsDescriptionPresent { get; set; }
        public bool IsDefaultPresent { get; set; }
        public bool DisplayActions { get; set; }
        public string? InternalDescription { get; set; }

        public bool IsListParam { get; set; }
        public bool IsTokenSelector { get; set; }
        public bool IsSpecialCaseParam { get; set; }
    }
}
