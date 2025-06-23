using FrontEASE.DataContracts.Constants;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters
{
    public class TaskModuleParameterCoreDto : ICoreDto
    {
        public TaskModuleParameterCoreDto()
        {
            ShortName = string.Empty;
            Type = string.Empty;
        }

        [JsonPropertyName(ParameterDtoConstants.ShortName)]
        public string ShortName { get; set; }

        [JsonPropertyName(ParameterDtoConstants.LongName)]
        public string? LongName { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Description)]
        public string? Description { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Type)]
        public string Type { get; set; }

        [JsonPropertyName(ParameterDtoConstants.MinValue)]
        public double? MinValue { get; set; }

        [JsonPropertyName(ParameterDtoConstants.MaxValue)]
        public double? MaxValue { get; set; }

        [JsonPropertyName(ParameterDtoConstants.EnumDescriptions)]
        public IList<string>? EnumDescriptions { get; set; }

        [JsonPropertyName(ParameterDtoConstants.EnumLongNames)]
        public IList<string>? EnumLongNames { get; set; }

        [JsonPropertyName(ParameterDtoConstants.EnumOptions)]
        public IList<TaskModuleParameterEnumOptionCoreDto>? EnumOptions { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Default)]
        public TaskModuleParameterValueCoreDto? Default { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Value)]
        public TaskModuleParameterValueCoreDto? Value { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Readonly)]
        public bool? Readonly { get; set; }

        [JsonPropertyName(ParameterDtoConstants.Required)]
        public bool? Required { get; set; }
    }
}
