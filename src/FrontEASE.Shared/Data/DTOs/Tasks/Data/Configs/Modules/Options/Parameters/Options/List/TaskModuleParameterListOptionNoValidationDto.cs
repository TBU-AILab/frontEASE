using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListOptionNoValidationDto
    {
        public TaskModuleParameterListOptionNoValidationDto()
        {
            ParameterValues = [];
        }

        /// <summary>
        /// Nested pararmeters.
        /// </summary>
        public IList<TaskModuleParameterListOptionParamsNoValidationDto> ParameterValues { get; set; }
    }
}
