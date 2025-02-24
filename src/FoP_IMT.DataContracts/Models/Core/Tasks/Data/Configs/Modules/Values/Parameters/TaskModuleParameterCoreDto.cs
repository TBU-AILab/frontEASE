using FoP_IMT.DataContracts.Converters.Tasks.Parameters;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Options.Values.Parameters
{
    public class TaskModuleParameterCoreDto : ITaskCoreDto
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
    }
}
