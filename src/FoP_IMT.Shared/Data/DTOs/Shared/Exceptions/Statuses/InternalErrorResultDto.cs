using System.Net;

namespace FoP_IMT.Shared.Data.DTOs.Shared.Exceptions.Statuses
{
    /// <summary>
    /// Model transporting information about unhandled exception
    /// </summary>
    public class InternalErrorResultDto : StatusResultDto
    {
        public InternalErrorResultDto()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
