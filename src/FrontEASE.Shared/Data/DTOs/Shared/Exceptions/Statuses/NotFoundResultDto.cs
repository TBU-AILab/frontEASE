using System.Net;

namespace FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses
{
    /// <summary>
    /// Model transporting information about incorrectly performed API call - referring to not found entity / object
    /// </summary>
    public class NotFoundResultDto : StatusResultDto
    {
        public NotFoundResultDto()
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
