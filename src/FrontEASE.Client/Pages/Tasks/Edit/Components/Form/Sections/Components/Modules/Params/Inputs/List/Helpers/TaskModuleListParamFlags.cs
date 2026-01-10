using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.List.Helpers
{
    public class TaskModuleListParamFlags
    {
        public ParameterType? ListParamType { get; set; }
        public string? ListDefaultValue { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsDescriptionPresent { get; set; }
        public bool IsListDefaultPresent { get; set; }
        public bool SkipListLabel { get; set; }
        public bool DisplayActions { get; set; }
    }
}
