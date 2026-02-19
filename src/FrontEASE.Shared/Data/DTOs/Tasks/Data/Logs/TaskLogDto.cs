using FrontEASE.Shared.Infrastructure.Attributes;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Logs
{
    /// <summary>
    /// Singular task-related core log entry.
    /// </summary>
    public class TaskLogDto
    {
        public TaskLogDto()
        {
            Message = string.Empty;
        }

        #region Data

        /// <summary>
        /// The log message content
        /// </summary>
        [Resource($"{nameof(TaskLogDto)}.{nameof(Message)}")]
        public string Message { get; set; }

        #endregion
    }
}
