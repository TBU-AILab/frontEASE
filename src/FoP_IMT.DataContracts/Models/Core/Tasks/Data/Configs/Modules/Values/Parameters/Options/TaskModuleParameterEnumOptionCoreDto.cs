using FoP_IMT.DataContracts.Converters.Tasks.Parameters;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options
{
    [JsonConverter(typeof(ParameterEnumOptionConverter))]
    public class TaskModuleParameterEnumOptionCoreDto : ITaskCoreDto
    {
        public string? StringValue { get; set; }
        public TaskModuleCoreDto ModuleValue { get; set; }
    }
}
