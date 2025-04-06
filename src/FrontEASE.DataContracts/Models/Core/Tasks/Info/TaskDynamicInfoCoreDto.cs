using FrontEASE.DataContracts.Models.Core.Tasks.Data.Messages;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Solutions;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Info
{
    public class TaskDynamicInfoCoreDto : ICoreDto
    {
        public TaskDynamicInfoCoreDto()
        {
            Messages = [];
            Solutions = [];
        }

        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("messages")]
        public IList<TaskMessageCoreDto> Messages { get; set; }

        [JsonPropertyName("solutions")]
        public IList<TaskSolutionCoreDto> Solutions { get; set; }
    }
}
