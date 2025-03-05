namespace FrontEASE.Client.Services.HelperServices.Auth.Models
{
    /// <summary>
    /// Represents the result of an authentication attempt.
    /// </summary>
    public class AuthAttemptResultDto
    {
        public AuthAttemptResultDto()
        {
            Errors = [];
        }

        /// <summary>
        /// Indicates whether the authentication attempt was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The errors that occurred during the authentication attempt.
        /// </summary>
        public string[] Errors { get; set; }
    }
}
