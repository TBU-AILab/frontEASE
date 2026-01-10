using FrontEASE.Domain.Infrastructure.Exceptions.Enums;

namespace FrontEASE.Domain.Infrastructure.Exceptions.Types
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException() => InternalExceptionCode = ApiInternalExceptionCode.UNAUTHORIZED;
    }
}
