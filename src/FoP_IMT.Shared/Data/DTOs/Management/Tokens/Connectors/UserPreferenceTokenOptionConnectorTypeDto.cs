﻿using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;
using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Management.Tokens.Connectors
{
    /// <summary>
    /// Token preference target connector type
    /// </summary>
    public class UserPreferenceTokenOptionConnectorTypeDto
    {
        public UserPreferenceTokenOptionConnectorTypeDto()
        {
            ConnectorType = string.Empty;
        }

        #region Data

        /// <summary>
        /// Enum signalising what type of connector has been selected of this task.
        /// </summary>
        [Resource($"{nameof(UserPreferenceTokenOptionConnectorTypeDto)}.{nameof(ConnectorType)}")]
        [RequiredValidation<UserPreferenceTokenOptionConnectorTypeDto>]
        [StringLengthValidation(4, 128)]
        public string ConnectorType { get; set; }

        #endregion
    }
}
