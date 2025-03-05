using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options
{
    /// <summary>
    /// DTO for class dynamic module
    /// </summary>
    public class TaskModuleDto
    {
        public TaskModuleDto()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
            Description = string.Empty;

            Parameters = [];
        }

        /// <summary>
        /// Module short name - identifier
        /// </summary>
        [Resource($"{nameof(TaskModuleDto)}.{nameof(ShortName)}")]
        [RequiredValidation<TaskModuleDto>]
        [StringLengthValidation(2, 128)]
        public string ShortName { get; set; }

        /// <summary>
        /// Module long name - display
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// Module meaning description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of package this module is describing
        /// </summary>
        public ModuleType PackageType { get; set; }

        /// <summary>
        /// Set of dynamic parameters
        /// </summary>
        public IList<TaskModuleParameterDto> Parameters { get; set; }
    }
}
