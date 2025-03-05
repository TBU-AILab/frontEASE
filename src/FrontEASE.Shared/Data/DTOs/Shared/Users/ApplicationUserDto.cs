using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Shared.Users
{
    /// <summary>
    /// Dto for transporting information about system user
    /// </summary>
    public class ApplicationUserDto
    {
        public ApplicationUserDto()
        {
            Role = UserRole.USER;
            Image = new ImageDto();
            UserPreferences = new UserPreferencesDto();
            Companies = [];
        }

        #region Navigation

        /// <summary>
        /// User Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// User profile image
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(Image)}")]
        public ImageDto? Image { get; set; }

        /// <summary>
        /// Companies user is associated into
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(Companies)}")]
        [RequiredValidation<ApplicationUserDto>]
        public IList<CompanyDto> Companies { get; set; }

        /// <summary>
        /// User configuration options
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(UserPreferences)}")]
        public UserPreferencesDto UserPreferences { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// User Identifier
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(UserName)}")]
        [RequiredValidation<ApplicationUserDto>]
        [StringLengthValidation(4, 256)]
        public string? UserName { get; set; }

        /// <summary>
        /// User email contact
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(Email)}")]
        [RequiredValidation<ApplicationUserDto>]
        [StringLengthValidation(4, 128)]
        [EmailValidation]
        public string? Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(Password)}")]
        [StringLengthValidation(8, 64)]
        public string? Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Resource($"{nameof(ApplicationUserDto)}.{nameof(Role)}")]
        [RequiredValidation<ApplicationUserDto>]
        [EnumValidation(typeof(UserRole))]
        public UserRole Role { get; set; }

        #endregion
    }
}
