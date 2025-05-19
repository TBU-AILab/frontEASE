using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.RepeatedMessage;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs
{
    public abstract class TaskConfigCoreBaseDto
    {
        [JsonPropertyName("repeated_message")]
        public TaskConfigRepeatedMessageCoreDto? RepeatedMessage { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("feedback_from_solution")]
        public bool? FeedbackFromSolution { get; set; }

        [JsonPropertyName("max_context_size")]
        public int? MaxContextSize { get; set; }

        [JsonPropertyName("system_message")]
        public string? SystemMessage { get; set; }

        [JsonPropertyName("initial_message")]
        public string? InitialMessage { get; set; }
    }
}
