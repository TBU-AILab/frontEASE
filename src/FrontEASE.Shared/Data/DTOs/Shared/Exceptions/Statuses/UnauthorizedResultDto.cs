using System.Net;

namespace FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses
{
    /// <summary>
    /// Model transporting information about result of unauthorized action
    /// </summary>
    public class UnauthorizedResultDto : StatusResultDto
    {
        public UnauthorizedResultDto()
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }

        /// <summary>
        /// Error reason - insufficient rights, section not allowed, auth error...
        /// </summary>
        public string? Reason { get; set; }
    }
}
