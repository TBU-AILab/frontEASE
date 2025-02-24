using FoP_IMT.Domain.Infrastructure.Exceptions.Enums;

namespace FoP_IMT.Domain.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ApiInternalExceptionCode InternalExceptionCode { get; set; }
    }
}
