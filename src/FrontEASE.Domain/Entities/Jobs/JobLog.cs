using FrontEASE.Domain.Entities.Base.Tracked;

namespace FrontEASE.Domain.Entities.Jobs
{
    public class JobLog : EntityBase
    {
        public JobLog()
        {
            this.JobName = string.Empty;
        }

        public string JobName { get; set; }
        public bool Success { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
