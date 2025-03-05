using FrontEASE.Domain.Entities.Base.Manual;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Shared;

namespace FrontEASE.Domain.Entities.Tasks.Solutions
{
    public class TaskSolution : EntityBaseManualStamp
    {
        public TaskSolution()
        {
            ID = Guid.NewGuid();
            Task = null!;
            TaskMessage = null!;

            Metadata = [];
        }

        #region Navigation

        public Guid TaskID { get; set; }
        public Task Task { get; set; }

        public Guid TaskMessageID { get; set; }
        public TaskMessage TaskMessage { get; set; }

        #endregion

        #region Data

        public float? Fitness { get; set; }
        public string? Feedback { get; set; }
        public IList<TaskKeyValueItem> Metadata { get; set; }

        #endregion
    }
}
