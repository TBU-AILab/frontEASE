namespace FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage
{
    /// <summary>
    /// Type of repeated message
    /// </summary>
    public enum RepeatedMessageType
    {
        /// <summary>
        /// Single message (first if more are defined).
        /// </summary>
        SINGLE,

        /// <summary>
        /// Message randomly selected.
        /// </summary>
        RANDOM,

        /// <summary>
        /// Message selected with weighted randomness.
        /// </summary>
        RANDOM_WEIGHTED,

        /// <summary>
        /// Message send in a circular pattern.
        /// </summary>
        CIRCULAR
    }
}
