using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Errors
{
    public class CoreValidationError : ITaskCoreDto
    {
        public CoreValidationError()
        {
            Detail = [];
        }

        [JsonPropertyName("detail")]
        public IList<CoreValidationErrorDetail> Detail { get; set; }
    }
}
