using FrontEASE.Domain.Infrastructure.Settings.App.Hangfire.Jobs;
using FrontEASE.Domain.Infrastructure.Settings.App.Hangfire.Login;

namespace FrontEASE.Domain.Infrastructure.Settings.App.Hangfire
{
    public class HangfireSettings
    {
        public bool IsEnabled { get; set; }
        public HangfireJobs? Jobs { get; set; }
        public HangfireLogin? Login { get; set; }
    }
}
