using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests
{
    /// <summary>
    /// Dto bearing information used for task filtration.
    /// </summary>
    public class TaskFilterActionRequestDto
    {
        public TaskFilterActionRequestDto()
        {
            State = [];
        }

        /// <summary>
        /// Task name.
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(Name)}")]
        [StringLengthValidation(0, 256)]
        public string? Name { get; set; }

        /// <summary>
        /// Content of messages.
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(MessagesContent)}")]
        [StringLengthValidation(0, 1024)]
        public string? MessagesContent { get; set; }

        /// <summary>
        /// Task states.
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(State)}")]
        public IReadOnlyList<TaskState> State { get; set; }

        /// <summary>
        /// Date of creation - from
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(DateCreatedFrom)}")]
        [DateNotInFutureValidation]
        [DateRangeValidation(from: "2025-01-01")]
        public DateTime? DateCreatedFrom { get; set; }

        /// <summary>
        /// Date of creation - to
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(DateCreatedTo)}")]
        [DateNotInFutureValidation]
        [DateRangeValidation(from: "2025-01-01")]
        public DateTime? DateCreatedTo { get; set; }

        /// <summary>
        /// Date of last update - from
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(DateUpdatedFrom)}")]
        [DateNotInFutureValidation]
        [DateRangeValidation(from: "2025-01-01")]
        public DateTime? DateUpdatedFrom { get; set; }

        /// <summary>
        /// Date of last update - to
        /// </summary>
        [Resource($"{nameof(TaskFilterActionRequestDto)}.{nameof(DateUpdatedTo)}")]
        [DateNotInFutureValidation]
        [DateRangeValidation(from: "2025-01-01")]
        public DateTime? DateUpdatedTo { get; set; }
    }
}
