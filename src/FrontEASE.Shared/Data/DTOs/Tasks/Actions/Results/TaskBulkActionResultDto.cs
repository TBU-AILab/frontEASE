namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results
{
    /// <summary>
    /// Contains the result of a bulk action on task instance.
    /// </summary>
    public class TaskBulkActionResultDto : BulkActionResultDto
    {
        /// <summary>
        /// ID of the affected task.
        /// </summary>
        public Guid TaskID { get; set; }
    }
}
