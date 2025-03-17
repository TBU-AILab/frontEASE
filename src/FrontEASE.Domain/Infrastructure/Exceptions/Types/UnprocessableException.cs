using FrontEASE.Domain.Infrastructure.Exceptions.Enums;

namespace FrontEASE.Domain.Infrastructure.Exceptions.Types
{
    public class UnprocessableException : ApiException
    {
        public IList<string> ReasonMessages { get; set; }

        public UnprocessableException(IList<string> messages)
        {
            InternalExceptionCode = ApiInternalExceptionCode.UNPROCESSABLE_ENTITY;
            ReasonMessages = messages;
        }
    }
}
