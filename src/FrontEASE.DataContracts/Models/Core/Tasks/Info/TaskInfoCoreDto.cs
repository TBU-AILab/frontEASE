using FrontEASE.Shared.Data.Enums.Tasks;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Info
{
    public class TaskInfoCoreDto : ITaskCoreDto
    {
        [JsonPropertyName("id")]
        public Guid? ID { get; set; }

        [JsonPropertyName("state")]
        public TaskState? State { get; set; }

        [JsonPropertyName("date_updated")]
        public DateTime? DateUpdated { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime? DateCreated { get; set; }

        [JsonPropertyName("current_iteration")]
        public int? CurrentIteration { get; set; }

        [JsonPropertyName("iterations_valid")]
        public int? IterationsValid { get; set; }

        [JsonPropertyName("iterations_invalid_consecutive")]
        public int? IterationsInvalidConsecutive { get; set; }
    }
}
