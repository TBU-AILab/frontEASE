namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterListOptionNoValidationDto
    {
        public TaskModuleParameterListOptionNoValidationDto()
        {
            ParameterValues = new Dictionary<string, TaskModuleParameterNoValidationDto>();
        }

        /// <summary>
        /// Nested pararmeters.
        /// </summary>
        public IDictionary<string, TaskModuleParameterNoValidationDto> ParameterValues { get; set; }
    }
}
