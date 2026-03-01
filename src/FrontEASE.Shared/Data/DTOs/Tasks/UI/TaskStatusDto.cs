using FrontEASE.Shared.Data.DTOs.Tasks.Data.Logs;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.UI
{
    /// <summary>
    /// Minified DTO for task state check transportation
    /// </summary>
    public class TaskStatusDto
    {
        public TaskStatusDto()
        {
            this.Logs = [];
        }

        #region Navigation

        /// <summary>
        /// Task identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Task status logs
        /// </summary>
        public IList<TaskLogDto> Logs { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Current task state.
        /// </summary>
        [EnumValidation(typeof(TaskState))]
        public TaskState State { get; set; }

        #endregion
    }
}
