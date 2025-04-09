using FrontEASE.DataContracts.Converters.Tasks.Parameters;
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

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("long_name")]
        public string? LongName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("min_value")]
        public float? MinValue { get; set; }

        [JsonPropertyName("max_value")]
        public float? MaxValue { get; set; }

        [JsonPropertyName("enum_descriptions")]
        public IList<string>? EnumDescriptions { get; set; }

        [JsonPropertyName("enum_long_names")]
        public IList<string>? EnumLongNames { get; set; }

        [JsonPropertyName("enum_options")]
        public IList<TaskModuleParameterEnumOptionCoreDto>? EnumOptions { get; set; }

        [JsonPropertyName("default")]
        [JsonConverter(typeof(TaskModuleParameterValueConverter))]
        public TaskModuleParameterValueCoreDto? Default { get; set; }

        [JsonPropertyName("readonly")]
        public bool? Readonly { get; set; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }
    }
}
