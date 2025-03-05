using FrontEASE.Domain.Entities.Base.Manual;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Shared.Data.Enums.Tasks.Messages;

namespace FrontEASE.Domain.Entities.Tasks.Messages
{
    public class TaskMessage : EntityTrackedBaseManualStamp
    {
        public TaskMessage()
        {
            ID = Guid.NewGuid();
            Content = string.Empty;

            Task = null!;
        }

        #region Navigation

        public Guid TaskID { get; set; }
        public Task Task { get; set; }

        public TaskSolution? TaskSolution { get; set; }

        #endregion

        #region Data

        public int? Tokens { get; set; }
        public string Content { get; set; }
        public string? Model { get; set; }
        public MessageRole Role { get; set; }

        #endregion
    }
}
