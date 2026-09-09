using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs
{
    /// <summary>
    /// Complete task configuration submitted as one item in a Core bulk update.
    /// </summary>
    public class TaskBulkUpdateItemCoreDto : ICoreDto
    {
        [JsonPropertyName("task_id")]
        public Guid TaskID { get; set; }

        [JsonPropertyName("task_configuration")]
        public TaskConfigFullCoreDto TaskConfiguration { get; set; } = new();
    }
}
