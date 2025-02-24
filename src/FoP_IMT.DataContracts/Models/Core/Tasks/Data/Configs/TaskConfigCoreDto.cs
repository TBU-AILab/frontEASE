using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs
{
    public class TaskConfigCoreDto : TaskConfigCoreBaseDto, ITaskCoreDto
    {
        [JsonPropertyName("modules")]
        public IList<TaskModuleBaseCoreDto>? Modules { get; set; }
    }
}
