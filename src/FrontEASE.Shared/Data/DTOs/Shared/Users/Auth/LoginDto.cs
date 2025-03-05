using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Shared.Users.Auth
{
    /// <summary>
    /// Dto for user login purposes
    /// </summary>
    public class LoginDto
    {
        public LoginDto()
        {
            Password = Username = string.Empty;
        }

        /// <summary>
        /// User password
        /// </summary>
        [Resource($"{nameof(LoginDto)}.{nameof(Password)}")]
        [RequiredValidation<LoginDto>]
        [StringLengthValidation(8, 64)]
        public string Password { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        [Resource($"{nameof(LoginDto)}.{nameof(Username)}")]
        [RequiredValidation<LoginDto>]
        [StringLengthValidation(4, 256)]
        public string Username { get; set; }
    }
}
