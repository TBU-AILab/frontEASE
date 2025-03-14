using FrontEASE.Shared.Data.Enums.Tasks.Messages;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Messages
{
    public class TaskMessageCoreDto : ITaskCoreDto
    {
        public TaskMessageCoreDto()
        {
            Content = string.Empty;
        }

        [JsonPropertyName("msg_id")]
        public Guid ID { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("tokens")]
        public int? Tokens { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("role")]
        public MessageRole Role { get; set; }
    }
}
