using FoP_IMT.Domain.Infrastructure.Exceptions.Enums;

namespace FoP_IMT.Domain.Infrastructure.Exceptions.Types
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
