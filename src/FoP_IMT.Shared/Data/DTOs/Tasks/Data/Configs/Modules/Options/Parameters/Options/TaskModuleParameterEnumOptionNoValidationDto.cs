﻿using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options
{
    /// <summary>
    /// Enum value option for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterEnumOptionNoValidationDto
    {
        /// <summary>
        /// String value
        /// </summary>
        public string? StringValue { get; set; }

        /// <summary>
        /// Nested module value
        /// </summary>
        public TaskModuleNoValidationDto? ModuleValue { get; set; }

        #region Metadata

        /// <summary>
        /// Validation-related metadata
        /// </summary>
        public TaskModuleParameterNoValidationMetadataDto? Metadata { get; set; }

        #endregion
    }
}
