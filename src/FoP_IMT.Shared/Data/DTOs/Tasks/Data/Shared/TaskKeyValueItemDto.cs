using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Shared
{
    /// <summary>
    /// Item for validatable key-value additional task data
    /// </summary>
    public class TaskKeyValueItemDto
    {
        public TaskKeyValueItemDto()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        /// <summary>
        /// Key part
        /// </summary>
        [Resource($"{nameof(TaskKeyValueItemDto)}.{nameof(Key)}")]
        [RequiredValidation<TaskKeyValueItemDto>]
        [StringLengthValidation(2, 128)]
        public string Key { get; set; }

        /// <summary>
        /// Value part
        /// </summary>
        [Resource($"{nameof(TaskKeyValueItemDto)}.{nameof(Value)}")]
        [RequiredValidation<TaskKeyValueItemDto>]
        [StringLengthValidation(4, 2048)]
        public string Value { get; set; }
    }
}
