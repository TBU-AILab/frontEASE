using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs
{
    public class TaskConfigInputCoreDto : TaskConfigCoreBaseDto, ITaskCoreDto
    {
        [JsonPropertyName("modules")]
        public IList<TaskModuleInputCoreDto>? Modules { get; set; }
    }
}
