using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Messages;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Solutions;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Info
{
    public class TaskDynamicInfoCoreDto : ITaskCoreDto
    {
        public TaskDynamicInfoCoreDto()
        {
            Messages = [];
            Solutions = [];
        }

        [JsonPropertyName("messages")]
        public IList<TaskMessageCoreDto> Messages { get; set; }

        [JsonPropertyName("solutions")]
        public IList<TaskSolutionCoreDto> Solutions { get; set; }
    }
}
