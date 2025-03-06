using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options
{
    /// <summary>
    /// DTO for class dynamic module
    /// </summary>
    public class TaskModuleNoValidationDto
    {
        public TaskModuleNoValidationDto()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
            Description = string.Empty;

            Parameters = [];
        }

        /// <summary>
        /// Module short name - identifier
        /// </summary>
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
        public IList<TaskModuleParameterNoValidationDto> Parameters { get; set; }
    }
}
