using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Errors
{
    public class CoreValidationErrorDetail : ICoreDto
    {
        public CoreValidationErrorDetail()
        {
            Msg = Type = string.Empty;
            Loc = [];
        }

        [JsonPropertyName("loc")]
        public IList<object> Loc { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
