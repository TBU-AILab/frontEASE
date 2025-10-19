using FrontEASE.Domain.Infrastructure.Settings.App.Auth;
using FrontEASE.Domain.Infrastructure.Settings.App.Environment;
using FrontEASE.Domain.Infrastructure.Settings.App.Hangfire;
using FrontEASE.Domain.Infrastructure.Settings.App.Integration;
using FrontEASE.Domain.Infrastructure.Settings.App.Licenses;
using FrontEASE.Domain.Infrastructure.Settings.App.Sentry;

namespace FrontEASE.Domain.Infrastructure.Settings.App
{
    public class AppSettings
    {
        public EnvironmentSettings? EnvironmentSettings { get; set; }
        public LicenseSettings? LicenseSettings { get; set; }
        public AuthSettings? AuthSettings { get; set; }
        public IntegrationSettings? IntegrationSettings { get; set; }
        public HangfireSettings? HangfireSettings { get; set; }
        public SentrySettings? SentrySettings { get; set; }
    }
}