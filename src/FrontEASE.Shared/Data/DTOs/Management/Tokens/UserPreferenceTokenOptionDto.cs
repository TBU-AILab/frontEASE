using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;
using System.Text.Json.Serialization;

namespace FrontEASE.Shared.Data.DTOs.Management.Tokens
{
    /// <summary>
    /// Dto used for a singular token option management.
    /// </summary>
    public class UserPreferenceTokenOptionDto
    {
        public UserPreferenceTokenOptionDto()
        {
            Token = string.Empty;
            Name = string.Empty;

            ConnectorTypes = [];
        }

        #region Navigation

        /// <summary>
        /// Connector types using this token.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(ConnectorTypes)}")]
        public IList<UserPreferenceTokenOptionConnectorTypeDto> ConnectorTypes { get; set; }

        #endregion

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
        [StringLengthValidation(1, 512)]
        public string Token { get; set; }

        /// <summary>
        /// Optional description of this token.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionDto)}.{nameof(Description)}")]
        [StringLengthValidation(0, 512)]
        public string? Description { get; set; }

        #endregion

        #region Visualization

        /// <summary>
        /// UI only - connector types selected for this token.
        /// </summary>
        [JsonIgnore]
        [CollectionNotEmptyValidation]
        public IReadOnlyList<string>? SelectedTokenConnectorTypes { get; set; }

        #endregion
    }
}