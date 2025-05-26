namespace FrontEASE.Domain.Infrastructure.Settings.App.Hangfire.Jobs
{
    public class HangfireJobs
    {
        public HangfireJobConfig? UpdateTaskDetailsJob { get; set; }
        public HangfireJobConfig? InitialTaskSyncJob { get; set; }
    }
}
