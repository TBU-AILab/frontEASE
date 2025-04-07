using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Packages
{
    public class CorePackageCoreDto : ICoreDto
    {
        public CorePackageCoreDto()
        {
            this.Name = this.Version = string.Empty; 
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("system")]
        public bool System { get; set; }
    }
}
