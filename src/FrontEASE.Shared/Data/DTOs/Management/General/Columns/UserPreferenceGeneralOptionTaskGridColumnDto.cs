using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management.General.Columns
{
    /// <summary>
    /// DTO for storing user preference related to visibility of columns in the task grid.
    /// </summary>
    public class UserPreferenceGeneralOptionTaskGridColumnDto
    {
        /// <summary>
        /// Column type.
        /// </summary>
        [Resource($"{nameof(UserPreferenceGeneralOptionTaskGridColumnDto)}.{nameof(TaskGridColumnsVisibility)}")]
        [RequiredValidation<UserPreferenceGeneralOptionTaskGridColumnDto>]
        [EnumValidation(typeof(TaskGridColumnsVisibility))]
        public TaskGridColumnsVisibility Column { get; set; }
    }
}
