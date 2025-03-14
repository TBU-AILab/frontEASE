namespace FrontEASE.Domain.Infrastructure.Exceptions.Enums
{
    /// <summary>
    /// Error code - handled exception
    /// </summary>
    public enum ApiInternalExceptionCode
    {
        /// <summary>
        /// Unauthorized (401)
        /// </summary>
        UNAUTHORIZED,

        /// <summary>
        /// Not found (404)
        /// </summary>
        NOT_FOUND,

        /// <summary>
        /// Bad request (404)
        /// </summary>
        BAD_REQUEST,

        /// <summary>
        /// Internal Server Error (500)
        /// </summary>
        INTERNAL_SERVER_ERROR,
    }
}
