using FrontEASE.Shared.Data.DTOs.Management.General.Columns;
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
        public UserPreferenceGeneralOptionsDto()
        {
            this.TaskGridColumns = [];
        }

        #region Navigation

        /// <summary>
        /// Selected visible columns in the task grid
        /// </summary>
        [Resource($"{nameof(UserPreferenceGeneralOptionTaskGridColumnDto)}.{nameof(TaskGridColumns)}")]
        public IList<UserPreferenceGeneralOptionTaskGridColumnDto> TaskGridColumns { get; set; }

        #endregion

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

        [Resource($"{nameof(UserPreferenceGeneralOptionsDto)}.{nameof(UserMessageDisplayFormat)}")]
        [RequiredValidation<UserPreferenceGeneralOptionsDto>]
        [EnumValidation(typeof(MessageDisplayFormat))]
        public MessageDisplayFormat UserMessageDisplayFormat { get; set; }

        [Resource($"{nameof(UserPreferenceGeneralOptionsDto)}.{nameof(SystemMessageDisplayFormat)}")]
        [RequiredValidation<UserPreferenceGeneralOptionsDto>]
        [EnumValidation(typeof(MessageDisplayFormat))]
        public MessageDisplayFormat SystemMessageDisplayFormat { get; set; }

        #endregion
    }
}
