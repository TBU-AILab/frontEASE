using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Shared.Users.Auth
{
    /// <summary>
    /// Dto for user login purposes
    /// </summary>
    public class LoginDto
    {
        public LoginDto()
        {
            Password = string.Empty;
            Username = string.Empty;
        }

        /// <summary>
        /// User password
        /// </summary>
        [Resource($"{nameof(LoginDto)}.{nameof(Password)}")]
        public string Password { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        [Resource($"{nameof(LoginDto)}.{nameof(Username)}")]
        public string Username { get; set; }

        /// <summary>
        /// Whether the user data should be remembered for future uses.
        /// </summary>
        [Resource($"{nameof(LoginDto)}.{nameof(RememberMe)}")]
        public bool RememberMe { get; set; }
    }
}
