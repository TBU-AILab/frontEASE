using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules
{
    public class TaskModuleBaseCoreDto
    {
        public TaskModuleBaseCoreDto()
        {
            ShortName = string.Empty;
        }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }
    }
}
