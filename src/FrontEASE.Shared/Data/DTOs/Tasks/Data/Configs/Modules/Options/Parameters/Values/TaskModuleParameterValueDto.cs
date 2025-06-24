using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.Tasks;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values
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
        public double? FloatValue { get; set; }

        /// <summary>
        /// Parameter - int value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(IntValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        [ParameterNumericRangeValidation(true)]
        [ParameterTimeValidation]
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

        /// <summary>
        /// Parameter - list module value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(ListValue)}")]
        [ParameterPropertyFilledValidation<TaskModuleParameterValueDto>]
        public TaskModuleParameterListOptionDto? ListValue { get; set; }

        #endregion

        #region Metadata

        /// <summary>
        /// Validation-related metadata
        /// </summary>
        public TaskModuleParameterNoValidationMetadataDto? Metadata { get; set; }

        #endregion
    }
}
