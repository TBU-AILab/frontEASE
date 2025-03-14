using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Solutions
{
    public class TaskSolutionCoreDto : ITaskCoreDto
    {
        public TaskSolutionCoreDto()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonPropertyName("msg_id")]
        public Guid TaskMessageID { get; set; }

        [JsonPropertyName("fitness")]
        public float? Fitness { get; set; }

        [JsonPropertyName("feedback")]
        public string? Feedback { get; set; }

        [JsonPropertyName("metadata")]
        public IDictionary<string, string>? Metadata { get; set; }
    }
}
