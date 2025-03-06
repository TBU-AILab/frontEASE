using FrontEASE.Domain.Infrastructure.Exceptions.Enums;

namespace FrontEASE.Domain.Infrastructure.Exceptions.Types
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() => InternalExceptionCode = ApiInternalExceptionCode.NOT_FOUND;
    }
}
