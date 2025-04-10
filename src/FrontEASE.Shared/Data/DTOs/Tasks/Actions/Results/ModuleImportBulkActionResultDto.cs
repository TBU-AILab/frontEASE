namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results
{
    /// <summary>
    /// Contains the result of a bulk action on module import.
    /// </summary>
    public class ModuleImportBulkActionResultDto : BulkActionResultDto
    {
        public ModuleImportBulkActionResultDto()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Filename of the affected module.
        /// </summary>
        public string Name { get; set; }
    }
}
