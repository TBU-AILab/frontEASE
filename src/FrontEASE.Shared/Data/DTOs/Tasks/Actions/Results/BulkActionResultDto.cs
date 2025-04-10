namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results
{
    /// <summary>
    /// Contains the result of a bulk action.
    /// </summary>
    public class BulkActionResultDto
    {
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
