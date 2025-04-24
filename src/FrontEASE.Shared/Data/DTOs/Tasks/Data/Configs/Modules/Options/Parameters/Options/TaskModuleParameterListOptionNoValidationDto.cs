namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterListOptionNoValidationDto
    {
        public TaskModuleParameterListOptionNoValidationDto()
        {
            ParameterValues = new List<IList<TaskModuleParameterNoValidationDto>>();
        }

        /// <summary>
        /// Nested pararmeters.
        /// </summary>
        public IList<IList<TaskModuleParameterNoValidationDto>> ParameterValues { get; set; }
    }
}
