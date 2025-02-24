using FoP_IMT.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageCoreDto : ITaskCoreDto
    {
        public TaskConfigRepeatedMessageCoreDto()
        {
            Msgs = [];
            Weights = [];
        }

        [JsonPropertyName("msgs")]
        public IList<string> Msgs { get; set; }

        [JsonPropertyName("weights")]
        public IList<float> Weights { get; set; }

        [JsonPropertyName("type")]
        public RepeatedMessageType Type { get; set; }
    }
}
