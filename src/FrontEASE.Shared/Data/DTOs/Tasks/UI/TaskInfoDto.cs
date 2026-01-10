using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Attributes;

namespace FrontEASE.Shared.Data.DTOs.Tasks.UI
{
    /// <summary>
    /// DTO for simplified task information (multiple overview)
    /// </summary>
    public class TaskInfoDto
    {
        public TaskInfoDto()
        {
            Name = string.Empty;
            ConnectorType = string.Empty;
            SolutionType = string.Empty;

            Author = null!;
        }

        #region Navigation

        /// <summary>
        /// Task identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Task author
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(Author)}")]
        public ApplicationUserDto Author { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Task name
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(Name)}")]
        public string Name { get; set; }

        /// <summary>
        /// Last task update datetime.
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(DateUpdated)}")]
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Task creation datetime.
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(DateCreated)}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Current task state.
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(State)}")]
        public TaskState State { get; set; }

        /// <summary>
        /// Gets the large language model currently set for this task
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(ConnectorType)}")]
        public string ConnectorType { get; set; }

        /// <summary>
        /// Expected solution format
        /// </summary>
        [Resource($"{nameof(TaskInfoDto)}.{nameof(SolutionType)}")]
        public string SolutionType { get; set; }

        #endregion
    }
}
