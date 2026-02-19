using FrontEASE.Domain.Entities.Base.Manual;

namespace FrontEASE.Domain.Entities.Tasks.Logs
{
    public class TaskLog : EntityBaseManualStamp
    {
        public TaskLog()
        {
            ID = Guid.NewGuid();
            Message = string.Empty;
            Task = null!;
        }

        #region Navigation

        public Guid TaskID { get; set; }
        public Task Task { get; set; }

        #endregion

        #region Data

        public string Message { get; set; }

        #endregion
    }
}
