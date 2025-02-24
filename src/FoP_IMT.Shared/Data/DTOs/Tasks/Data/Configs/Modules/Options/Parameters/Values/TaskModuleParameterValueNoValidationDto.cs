using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValueNoValidationDto
    {
        #region Data

        /// <summary>
        /// Parameter - float value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(FloatValue)}")]
        public float? FloatValue { get; set; }

        /// <summary>
        /// Parameter - int value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(IntValue)}")]
        public int? IntValue { get; set; }

        /// <summary>
        /// Parameter - string value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(StringValue)}")]
        public string? StringValue { get; set; }

        /// <summary>
        /// Parameter - bool value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(BoolValue)}")]
        public bool? BoolValue { get; set; }

        /// <summary>
        /// Parameter - enum module value
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterValueDto)}.{nameof(EnumValue)}")]
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
