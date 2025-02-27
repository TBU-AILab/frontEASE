using FoP_IMT.Shared.Data.Enums.Tasks.Messages;
using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Messages
{
    /// <summary>
    /// Singular task message in performed iterative process.
    /// </summary>
    public class TaskMessageDto
    {
        public TaskMessageDto()
        {
            Content = string.Empty;
        }

        #region Navigation

        /// <summary>
        /// Message identifier
        /// </summary>
        public Guid ID { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Date of message creation
        /// </summary>
        [Resource($"{nameof(TaskMessageDto)}.{nameof(DateCreated)}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Tokens spent for message
        /// </summary>
        [Resource($"{nameof(TaskMessageDto)}.{nameof(Tokens)}")]
        public int? Tokens { get; set; }

        /// <summary>
        /// Message text content
        /// </summary>
        [Resource($"{nameof(TaskMessageDto)}.{nameof(Content)}")]
        public string Content { get; set; }

        /// <summary>
        /// Language model selected for this message
        /// </summary>
        [Resource($"{nameof(TaskMessageDto)}.{nameof(Model)}")]
        public string? Model { get; set; }

        /// <summary>
        /// Classification of message within the conversation
        /// </summary>
        [Resource($"{nameof(TaskMessageDto)}.{nameof(Role)}")]
        public MessageRole Role { get; set; }

        #endregion
    }
}
