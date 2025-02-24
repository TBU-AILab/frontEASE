using FoP_IMT.Domain.Infrastructure.Settings.App.Auth;
using FoP_IMT.Domain.Infrastructure.Settings.App.Environment;
using FoP_IMT.Domain.Infrastructure.Settings.App.Hangfire;
using FoP_IMT.Domain.Infrastructure.Settings.App.Integration;
using FoP_IMT.Domain.Infrastructure.Settings.App.Sentry;

namespace FoP_IMT.Domain.Infrastructure.Settings.App
{
    public class AppSettings
    {
        public EnvironmentSettings? EnvironmentSettings { get; set; }
        public AuthSettings? AuthSettings { get; set; }
        public IntegrationSettings? IntegrationSettings { get; set; }
        public HangfireSettings? HangfireSettings { get; set; }
        public SentrySettings? SentrySettings { get; set; }
    }
}