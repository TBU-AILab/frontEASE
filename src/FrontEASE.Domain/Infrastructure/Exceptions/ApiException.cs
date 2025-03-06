using FrontEASE.Domain.Infrastructure.Exceptions.Enums;

namespace FrontEASE.Domain.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ApiInternalExceptionCode InternalExceptionCode { get; set; }
    }
}
