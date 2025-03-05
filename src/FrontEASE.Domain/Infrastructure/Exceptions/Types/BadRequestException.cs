using FrontEASE.Domain.Infrastructure.Exceptions.Enums;

namespace FrontEASE.Domain.Infrastructure.Exceptions.Types
{
    public class BadRequestException : ApiException
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public BadRequestException()
        {
            Errors = new Dictionary<string, string[]>();
            InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST;
        }
    }
}
