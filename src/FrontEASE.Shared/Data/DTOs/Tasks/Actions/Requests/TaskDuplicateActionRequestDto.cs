using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests
{
    /// <summary>
    /// Request DTO for the task duplicate action.
    /// </summary>
    public class TaskDuplicateActionRequestDto
    {
        public TaskDuplicateActionRequestDto()
        {
            Name = string.Empty;
            Copies = 1;
        }

        /// <summary>
        /// Name of the task duplicates.
        /// </summary>
        [Resource($"{nameof(TaskDuplicateActionRequestDto)}.{nameof(Name)}")]
        [RequiredValidation<TaskDuplicateActionRequestDto>]
        [StringLengthValidation(4, 128)]
        public string Name { get; set; }

        /// <summary>
        /// How many copies of the task should be created.
        /// </summary>
        [Resource($"{nameof(TaskDuplicateActionRequestDto)}.{nameof(Copies)}")]
        [RequiredValidation<TaskDuplicateActionRequestDto>]
        [RangeValidation(1, 32)]
        public int Copies { get; set; }
    }
}
