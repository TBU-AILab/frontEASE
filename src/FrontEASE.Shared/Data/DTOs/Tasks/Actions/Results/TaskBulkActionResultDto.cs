namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results
{
    /// <summary>
    /// Contains the result of a bulk action on task instance.
    /// </summary>
    public class TaskBulkActionResultDto
    {
        /// <summary>
        /// ID of the affected task.
        /// </summary>
        public Guid TaskID { get; set; }

        /// <summary>
        /// Success status of the action.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Exception message if the action failed.
        /// </summary>
        public string? ExceptionMessage { get; set; }
    }
}
