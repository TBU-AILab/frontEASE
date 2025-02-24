using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific.FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValueDto
    {
        #region Data

        /// <summary>
        /// Parameter - float value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(FloatValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        [ParameterNumericRangeValidation(true)]
        public float? FloatValue { get; set; }

        /// <summary>
        /// Parameter - int value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(IntValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        [ParameterNumericRangeValidation(true)]
        public int? IntValue { get; set; }

        /// <summary>
        /// Parameter - string value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(StringValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        [StringLengthValidation(2, 16384, true)]
        public string? StringValue { get; set; }

        /// <summary>
        /// Parameter - bool value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(BoolValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        public bool? BoolValue { get; set; }

        /// <summary>
        /// Parameter - enum module value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(EnumValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        public TaskModuleParameterEnumOptionDto? EnumValue { get; set; }

        #endregion

        #region Metadata

        /// <summary>
        /// Validation-related metadata
        /// </summary>
        public TaskModuleParameterNoValidationMetadataDto? Metadata { get; set; }

        #endregion
    }
}
