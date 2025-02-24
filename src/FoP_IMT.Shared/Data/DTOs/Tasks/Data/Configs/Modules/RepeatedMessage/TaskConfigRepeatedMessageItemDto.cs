using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage
{
    /// <summary>
    /// Structure representing one repeated message option
    /// </summary>
    public class TaskConfigRepeatedMessageItemDto
    {
        public TaskConfigRepeatedMessageItemDto()
        {
            Content = string.Empty;
        }

        #region Data

        /// <summary>
        /// Message content - text
        /// </summary>       
        [Resource($"{nameof(TaskConfigRepeatedMessageItemDto)}.{nameof(Content)}")]
        [RequiredValidation<TaskConfigRepeatedMessageItemDto>]
        [StringLengthValidation(4, 2048)]
        public string Content { get; set; }

        /// <summary>
        /// Weight for this message
        /// </summary>
        [Resource($"{nameof(TaskConfigRepeatedMessageItemDto)}.{nameof(Weight)}")]
        [RequiredValidation<TaskConfigRepeatedMessageItemDto>]
        [RangeValidation(0, 1)]
        public float Weight { get; set; }

        #endregion
    }
}
