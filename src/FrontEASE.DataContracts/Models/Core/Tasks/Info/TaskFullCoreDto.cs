using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Info
{
    public class TaskFullCoreDto : ICoreDto
    {
        [JsonPropertyName("task_info")]
        public TaskInfoCoreDto? TaskInfo { get; set; }

        [JsonPropertyName("task_data")]
        public TaskDynamicInfoCoreDto? TaskData { get; set; }

        [JsonPropertyName("task_config")]
        public TaskConfigFullCoreDto? TaskConfig { get; set; }

        [JsonPropertyName("task_modules")]
        public IList<TaskModuleCoreDto>? TaskModules { get; set; }
    }
}