using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Management.Tokens
{
    /// <summary>
    /// Dto used for a singular token option management.
    /// </summary>
    public class UserPreferenceTokenOptionDto
    {
        public UserPreferenceTokenOptionDto()
        {
            this.Token = string.Empty;
            this.ConnectorType = string.Empty;
            this.Name = string.Empty;
        }

        #region Data

        /// <summary>
        /// Datetime of this token preference´s creation.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Token priority for ordering purposes.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Token name.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(Name)}")]
        [RequiredValidation<UserPreferenceTokenOptionDto>]
        [StringLengthValidation(4, 128)]
        public string Name { get; set; }

        /// <summary>
        /// Connection token.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(Token)}")]
        [RequiredValidation<UserPreferenceTokenOptionDto>]
        [StringLengthValidation(12, 128)]
        public string Token { get; set; }

        /// <summary>
        /// Enum signalising what type of connector has been selected of this task.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(ConnectorType)}")]
        [RequiredValidation<UserPreferenceTokenOptionDto>]
        [StringLengthValidation(4, 128)]
        public string ConnectorType { get; set; }

        /// <summary>
        /// Optional description of this token.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(Description)}")]
        [StringLengthValidation(0, 512)]
        public string? Description { get; set; }

        #endregion
    }
}
