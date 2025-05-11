using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

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
        [RequiredValidation<TaskModuleParameterListOptionDto>]
        [CollectionNotEmptyValidation]
        public IList<TaskModuleParameterListOptionParamsDto> ParameterValues { get; set; }
    }
}
