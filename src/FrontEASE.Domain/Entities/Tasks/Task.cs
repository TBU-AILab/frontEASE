using FrontEASE.Domain.Entities.Base.Manual;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Domain.Entities.Tasks
{
    public class Task : EntityTrackedFullManualStamp
    {
        public Task()
        {
            ID = Guid.NewGuid();
            Config = new();

            Messages = [];
            Solutions = [];
            Members = [];
            MemberGroups = [];
        }

        #region Navigation

        public Guid ConfigID { get; set; }
        public TaskConfig Config { get; set; }

        public Guid AuthorID { get; set; }
        public IList<ApplicationUser> Members { get; set; }
        public IList<Company> MemberGroups { get; set; }

        public IList<TaskMessage> Messages { get; set; }
        public IList<TaskSolution> Solutions { get; set; }

        #endregion

        #region Data

        public TaskState State { get; set; }
        public int CurrentIteration { get; set; }
        public int IterationsValid { get; set; }
        public int IterationsInvalidConsecutive { get; set; }

        #endregion
    }
}
