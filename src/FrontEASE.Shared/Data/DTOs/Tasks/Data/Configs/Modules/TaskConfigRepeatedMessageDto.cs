using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules
{
    /// <summary>
    /// Repeated message wrapper data structure
    /// </summary>
    public class TaskConfigRepeatedMessageDto
    {
        public TaskConfigRepeatedMessageDto()
        {
            RepeatedMessageItems = [];
        }

        #region Navigation

        /// <summary>
        /// List of attached repeated message items
        /// </summary>
        [Resource($"{nameof(TaskConfigRepeatedMessageDto)}.{nameof(RepeatedMessageItems)}")]
        [RequiredValidation<TaskConfigRepeatedMessageDto>]
        public IList<TaskConfigRepeatedMessageItemDto> RepeatedMessageItems { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Selected type for repeated message
        /// </summary>
        [Resource($"{nameof(TaskConfigRepeatedMessageDto)}.{nameof(RepeatedMessageType)}")]
        [EnumValidation(typeof(RepeatedMessageType))]
        public RepeatedMessageType RepeatedMessageType { get; set; }

        #endregion
    }
}
