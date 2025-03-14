using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs
{
    public class TaskConfigFullCoreDto : TaskConfigCoreBaseDto, ITaskCoreDto
    {
        [JsonPropertyName("modules")]
        public IList<TaskModuleInputCoreDto>? Modules { get; set; }
    }
}
