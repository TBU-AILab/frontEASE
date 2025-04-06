using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Errors
{
    public class CoreValidationError : ICoreDto
    {
        public CoreValidationError()
        {
            Detail = [];
        }

        [JsonPropertyName("detail")]
        public IList<CoreValidationErrorDetail> Detail { get; set; }
    }
}
