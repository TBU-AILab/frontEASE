using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Data.Enums.Tasks;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data
{
    /// <summary>
    /// DTO for user-created optimisation task
    /// </summary>
    public class TaskDto
    {
        public TaskDto()
        {
            Config = new();
            Author = new();

            Messages = [];
            Solutions = [];
            Members = [];
            MemberGroups = [];
        }

        #region Navigation

        /// <summary>
        /// Task identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Task configuration setup.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(Config)}")]
        public TaskConfigDto Config { get; set; }

        /// <summary>
        /// Linked model - task author
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(Author)}")]
        public ApplicationUserDto Author { get; set; }

        /// <summary>
        /// Linked model - task members
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(Members)}")]
        public IList<ApplicationUserDto> Members { get; set; }

        /// <summary>
        /// Linked model - task members
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(MemberGroups)}")]
        public IList<CompanyDto> MemberGroups { get; set; }

        /// <summary>
        /// List of messages generated within this task.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(Messages)}")]
        public IList<TaskMessageDto> Messages { get; set; }

        /// <summary>
        /// List of found solutions - solution history.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(Solutions)}")]
        public IList<TaskSolutionDto> Solutions { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Last task update datetime.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(DateUpdated)}")]
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Task creation datetime.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(DateCreated)}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Current task state.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(State)}")]
        [EnumValidation(typeof(TaskState))]
        public TaskState State { get; set; }

        /// <summary>
        /// Current task executed iteration.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(CurrentIteration)}")]
        public int CurrentIteration { get; set; }

        /// <summary>
        /// Number of valid task iterations.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(IterationsValid)}")]
        public int IterationsValid { get; set; }

        /// <summary>
        /// Number of consecutive failed iterations.
        /// </summary>
        [Resource($"{nameof(TaskDto)}.{nameof(IterationsInvalidConsecutive)}")]
        public int IterationsInvalidConsecutive { get; set; }

        #endregion
    }
}
