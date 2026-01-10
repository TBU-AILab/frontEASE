using FrontEASE.Shared.Infrastructure.Constants.UI.Specific.Tasks;

namespace FrontEASE.Shared.Data.DTOs.Tasks.UI
{
    /// <summary>
    /// View metadata for evaluating additional information related to task
    /// </summary>
    public class TaskViewMetadataDto
    {
        /// <summary>
        /// Flag indicating whether task initialization is in progress
        /// </summary>
        public bool InitializationInProgres { get; set; }

        /// <summary>
        /// Flag indicating whether task reload is in progress
        /// </summary>
        public bool ReloadInProgress { get; set; }

        /// <summary>
        /// ID of the currently selected message
        /// </summary>
        public Guid? SelectedMessageID { get; set; }

        /// <summary>
        /// Flag indicating whether the task is in a state that requires a reload of options
        /// </summary>
        /// <param name="fieldChanged"></param>
        /// <returns></returns>
        public static bool OptionsReloadNecessary(string fieldChanged) =>
            TaskMetadataConstants.ReloadTriggerFields.Any(x => x.Equals(fieldChanged, StringComparison.InvariantCultureIgnoreCase));
    }
}
