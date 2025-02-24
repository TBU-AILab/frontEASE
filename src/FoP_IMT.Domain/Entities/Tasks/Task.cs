using FoP_IMT.Domain.Entities.Base.Manual;
using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Entities.Tasks.Configs;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using FoP_IMT.Domain.Entities.Tasks.Solutions;
using FoP_IMT.Shared.Data.Enums.Tasks;

namespace FoP_IMT.Domain.Entities.Tasks
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
