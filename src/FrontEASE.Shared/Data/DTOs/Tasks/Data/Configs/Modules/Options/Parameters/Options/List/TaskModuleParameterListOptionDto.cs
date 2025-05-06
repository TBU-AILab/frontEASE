using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;
using FrontEASE.Shared.Infrastructure.Attributes;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List
{
    /// <summary>
    /// List value option for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterListOptionDto
    {
        public TaskModuleParameterListOptionDto()
        {
            ParameterValues = [];
        }

        /// <summary>
        /// Nested list options.
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterListOptionDto)}.{nameof(ParameterValues)}")]
        public IList<TaskModuleParameterListOptionParamsDto> ParameterValues { get; set; }
    }
}
