using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageCoreDto : ICoreDto
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
