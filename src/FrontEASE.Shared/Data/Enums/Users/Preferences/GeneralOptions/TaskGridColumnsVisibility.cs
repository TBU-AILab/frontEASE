namespace FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions
{
    /// <summary>
    /// Toggles the visibility for the columns in the task grid. This allows users to customize their view by showing or hiding specific columns based on their preferences and needs.
    /// </summary>
    public enum TaskGridColumnsVisibility
    {
        /// <summary>
        /// Column with task name
        /// </summary>
        NAME,

        /// <summary>
        /// Column with task tags
        /// </summary>
        TAGS,

        /// <summary>
        /// Column with task created date
        /// </summary>
        DATE_CREATED,

        /// <summary>
        /// Column with task updated date
        /// </summary>
        DATE_UPDATED,

        /// <summary>
        /// Colum nwith task author (the user who created the task)
        /// </summary>
        AUTHOR,

        /// <summary>
        /// Column with task state (e.g., open, in progress, completed)
        /// </summary>
        STATE,

        /// <summary>
        /// Column with selected connector type
        /// </summary>
        CONNECTOR,

        /// <summary>
        /// Column with selected solution type
        /// </summary>
        SOLUTION
    }
}
