using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.Tags;

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

        #region Navigation

        /// <summary>
        /// Tag option ID
        /// </summary>
        public Guid? ID { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Tag text value
        /// </summary>
        [Resource($"{nameof(UserPreferenceTagOptionDto)}.{nameof(Tag)}")]
        [RequiredValidation<UserPreferenceTagOptionDto>]
        [TagFormatValidation]
        [StringLengthValidation(2, 32)]
        public string Tag { get; set; }

        #endregion

        #region Visualization

        /// <summary>
        /// UI helper - number of tasks linked to this tag
        /// </summary>
        [Resource($"{nameof(UserPreferenceTagOptionDto)}.{nameof(TaskCount)}")]
        public int TaskCount { get; set; }

        #endregion
    }
}
