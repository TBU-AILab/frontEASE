using System.Net;

namespace FrontEASE.Shared.Data.DTOs.Shared.Exceptions
{
    /// <summary>
    /// Generic model of error (unexpected) API call result
    /// </summary>
    public abstract class StatusResultDto
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Generic message specifying error cause
        /// </summary>
        public string? Message { get; set; }
    }
}
