namespace FrontEASE.Shared.Data.DTOs.Tasks.UI
{
    /// <summary>
    /// View metadata for evaluating additional information related to task view in its entirety
    /// </summary>
    public class TaskListViewMetadataDto
    {
        /// <summary>
        /// ID of the task currently being displayed in the mini overview mode
        /// </summary>
        public Guid? MiniOverviewTaskID { get; set; }
    }
}
