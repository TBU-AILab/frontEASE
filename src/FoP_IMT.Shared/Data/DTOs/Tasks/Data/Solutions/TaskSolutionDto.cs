using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Shared;
using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Solutions
{
    /// <summary>
    /// DTO for task solution
    /// </summary>
    public class TaskSolutionDto
    {
        public TaskSolutionDto()
        {
            Metadata = [];
        }

        #region Navigation

        /// <summary>
        /// Solution identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Bound message identifier
        /// </summary>
        public Guid TaskMessageID { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Solution evaluation - fitness value
        /// </summary>
        [Resource($"{nameof(TaskSolutionDto)}.{nameof(Fitness)}")]
        public float? Fitness { get; set; }

        /// <summary>
        /// Textual feedback assigned to this solution
        /// </summary>
        [Resource($"{nameof(TaskSolutionDto)}.{nameof(Feedback)}")]
        public string? Feedback { get; set; }

        /// <summary>
        /// Additional information in the form of metadata
        /// </summary>
        [Resource($"{nameof(TaskSolutionDto)}.{nameof(Metadata)}")]
        public IList<TaskKeyValueItemDto> Metadata { get; set; }

        #endregion
    }
}
