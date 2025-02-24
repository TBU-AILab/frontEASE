using FoP_IMT.Domain.Infrastructure.Settings.App.Hangfire.Jobs;

namespace FoP_IMT.Domain.Infrastructure.Settings.App.Hangfire
{
    public class HangfireSettings
    {
        public bool IsEnabled { get; set; }
        public HangfireJobs? Jobs { get; set; }
    }
}
