using FoP_IMT.Domain.Infrastructure.Exceptions.Enums;

namespace FoP_IMT.Domain.Infrastructure.Exceptions.Types
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() => InternalExceptionCode = ApiInternalExceptionCode.NOT_FOUND;
    }
}
