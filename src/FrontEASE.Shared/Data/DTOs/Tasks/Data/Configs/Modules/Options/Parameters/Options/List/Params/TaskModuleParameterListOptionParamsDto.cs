using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params
{
    /// <summary>
    /// List value option item for dynamic module parameters
    /// </summary>
    public class TaskModuleParameterListOptionParamsDto
    {
        public TaskModuleParameterListOptionParamsDto()
        {
            ParameterItems = [];
        }

        /// <summary>
        /// List of list option parameters.
        /// </summary>
        [RequiredValidation<TaskModuleParameterListOptionParamsDto>]
        [CollectionNotEmptyValidation]
        public IList<TaskModuleParameterDto> ParameterItems { get; set; }
    }
}
