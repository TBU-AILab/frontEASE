using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters
{
    public class TaskModuleParameterNoValidationDto
    {
        public TaskModuleParameterNoValidationDto()
        {
            ShortName = string.Empty;
            Type = string.Empty;
            Key = string.Empty;
        }

        public string Key { get; set; }
        public string ShortName { get; set; }
        public string? LongName { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }
        public IList<string>? EnumDescriptions { get; set; }
        public IList<string>? EnumLongNames { get; set; }
        public IList<TaskModuleParameterEnumOptionNoValidationDto>? EnumOptions { get; set; }
        public TaskModuleParameterValueNoValidationDto? Default { get; set; }
        public bool? Readonly { get; set; }
        public bool? Required { get; set; }
    }
}
