namespace FrontEASE.Shared.Data.Enums.Tasks.Config
{
    /// <summary>
    /// Enum representing type classification of dynamic task module
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// Analysis
        /// </summary>
        ANALYSIS,

        /// <summary>
        /// Evaluator
        /// </summary>
        EVALUATOR,

        /// <summary>
        /// LLM Connector
        /// </summary>
        LLM_CONNECTOR,

        /// <summary>
        /// Solution type - image, text...
        /// </summary>
        SOLUTION,

        /// <summary>
        /// Stopping condition
        /// </summary>
        STOPPING_CONDITION,

        /// <summary>
        /// Test
        /// </summary>
        TEST,

        /// <summary>
        /// Type of task stat
        /// </summary>
        STATISTIC
    }
}
