using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs
{
    public class TaskConfigCoreDto : TaskConfigCoreBaseDto, ITaskCoreDto
    {
        [JsonPropertyName("modules")]
        public IList<TaskModuleBaseCoreDto>? Modules { get; set; }
    }
}
