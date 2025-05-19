using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Metadata
{
    /// <summary>
    /// Parameter metadata validation class
    /// </summary>
    public class TaskModuleParameterNoValidationMetadataDto
    {
        public TaskModuleParameterNoValidationMetadataDto()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is parameter required
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        /// Parameter is readonly
        /// </summary>
        public bool Readonly { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// Mininum value in case of numeric param
        /// </summary>
        public float? MinValue { get; set; }

        /// <summary>
        /// Maximum value in case of numeric param
        /// </summary>
        public float? MaxValue { get; set; }

        /// <summary>
        /// Enum allowed options
        /// </summary>
        public IList<TaskModuleParameterEnumOptionNoValidationDto>? EnumOptions { get; set; }
    }
}
