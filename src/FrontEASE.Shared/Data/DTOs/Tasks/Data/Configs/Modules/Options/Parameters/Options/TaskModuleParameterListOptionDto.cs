using FrontEASE.Shared.Infrastructure.Attributes;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options
{
    /// <summary>
    /// List value option for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterListOptionDto
    {
        public TaskModuleParameterListOptionDto()
        {
            ParameterValues = new Dictionary<string, TaskModuleParameterDto>();
        }

        /// <summary>
        /// Nested pararmeters.
        /// </summary>
        [Resource($"{nameof(TaskModuleParameterListOptionDto)}.{nameof(ParameterValues)}")]
        public IDictionary<string, TaskModuleParameterDto> ParameterValues { get; set; }
    }
}
