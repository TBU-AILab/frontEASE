namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params
{
    /// <summary>
    /// List value option item for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterListOptionParamsNoValidationDto
    {
        public TaskModuleParameterListOptionParamsNoValidationDto()
        {
            ParameterItems = [];
        }

        /// <summary>
        /// List of list option parameters.
        /// </summary>
        public IList<TaskModuleParameterNoValidationDto> ParameterItems { get; set; }
    }
}
