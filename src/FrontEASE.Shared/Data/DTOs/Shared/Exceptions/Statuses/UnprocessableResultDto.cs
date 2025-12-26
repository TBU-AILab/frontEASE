using System.Net;

namespace FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses
{
    /// <summary>
    /// Model transporting information about incorrectly performed API call - referring to unprocessable entity
    /// </summary>
    public class UnprocessableResultDto : StatusResultDto
    {
        public UnprocessableResultDto()
        {
            StatusCode = HttpStatusCode.UnprocessableContent;

            Errors = [];
        }

        /// <summary>
        /// List of reasons for call invalidity
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
