using FoP_IMT.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Management.General
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

        #endregion
    }
}
