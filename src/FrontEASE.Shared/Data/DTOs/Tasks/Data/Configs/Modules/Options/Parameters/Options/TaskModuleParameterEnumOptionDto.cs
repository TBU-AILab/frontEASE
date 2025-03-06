using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options
{
    /// <summary>
    /// Enum value option for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterEnumOptionDto
    {
        /// <summary>
        /// String value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterEnumOptionDto)}.{nameof(StringValue)}")]
        [ParameterOneOfEnumValuesValidation]
        [ParameterEnumPropertyFilledValidation]
        public string? StringValue { get; set; }

        /// <summary>
        /// Nested module value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterEnumOptionDto)}.{nameof(ModuleValue)}")]
        [ParameterOneOfEnumValuesValidation]
        [ParameterEnumPropertyFilledValidation]
        public TaskModuleDto? ModuleValue { get; set; }

        #region Metadata

        /// <summary>
        /// Validation-related metadata
        /// </summary>
        public TaskModuleParameterNoValidationMetadataDto? Metadata { get; set; }

        #endregion
    }
}
