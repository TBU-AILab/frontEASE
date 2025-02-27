namespace FoP_IMT.Domain.Infrastructure.Settings.App.Hangfire.Jobs
{
    public class HangfireJobs
    {
        public HangfireJobConfig? UpdateTaskStatusesJob { get; set; }
        public HangfireJobConfig? UpdateTaskDetailsJob { get; set; }
        public HangfireJobConfig? InitialTaskSyncJob { get; set; }
    }
}
