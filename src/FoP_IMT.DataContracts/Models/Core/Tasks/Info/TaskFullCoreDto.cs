using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Info
{
    public class TaskFullCoreDto : ITaskCoreDto
    {
        [JsonPropertyName("task_info")]
        public TaskInfoCoreDto? TaskInfo { get; set; }

        [JsonPropertyName("task_data")]
        public TaskDynamicInfoCoreDto? TaskData { get; set; }

        [JsonPropertyName("task_config")]
        public TaskConfigFullCoreDto? TaskConfig { get; set; }
    }
}
