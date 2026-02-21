using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management.Tags
{
    /// <summary>
    /// Dto used for managing user preferences for tags and linking them to tasks.
    /// </summary>
    public class UserPreferenceTagOptionDto
    {
        public UserPreferenceTagOptionDto()
        {
            Tag = string.Empty;
        }

        #region Data

        [Resource($"{nameof(UserPreferenceTagOptionDto)}.{nameof(Tag)}")]
        [RequiredValidation<UserPreferenceTagOptionDto>]
        [StringLengthValidation(2, 32)]
        public string Tag { get; set; }

        #endregion
    }
}
