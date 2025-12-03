using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management.General
{
    /// <summary>
    /// DTO for holding user preference general options
    /// </summary>
    public class UserPreferenceGeneralOptionsDto
    {
        #region Data

        /// <summary>
        /// Selected color scheme
        /// </summary>
        [Resource($"{nameof(UserPreferenceGeneralOptionsDto)}.{nameof(ColorScheme)}")]
        [RequiredValidation<UserPreferenceGeneralOptionsDto>]
        [EnumValidation(typeof(ColorScheme))]
        public ColorScheme ColorScheme { get; set; }

        [Resource($"{nameof(UserPreferenceGeneralOptionsDto)}.{nameof(TokenVisibility)}")]
        [RequiredValidation<UserPreferenceGeneralOptionsDto>]
        [EnumValidation(typeof(TokenVisibility))]
        public TokenVisibility TokenVisibility { get; set; }

        #endregion
    }
}
