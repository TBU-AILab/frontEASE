namespace FoP_IMT.Shared.Data.Enums.Tasks
{
    /// <summary>
    /// Represents current state of optimisation task
    /// </summary>
    public enum TaskState
    {
        /// <summary>
        /// Task created
        /// </summary>
        CREATED,

        /// <summary>
        /// Created and initialized, not yet ran
        /// </summary>
        INIT,

        /// <summary>
        /// Task currently executed
        /// </summary>
        RUN,

        /// <summary>
        /// Task in progress, but paused
        /// </summary>
        PAUSED,

        /// <summary>
        /// Execution in progress, but halted
        /// </summary>
        STOP,

        /// <summary>
        /// Finished successfuly
        /// </summary>
        FINISH,

        /// <summary>
        /// Finished with error
        /// </summary>
        BREAK
    }
}
