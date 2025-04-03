using FrontEASE.Client.Infrastructure.Settings.AppSettings.Api;
using FrontEASE.Client.Infrastructure.Settings.AppSettings.Environment;
using FrontEASE.Client.Infrastructure.Settings.AppSettings.Integration;
using FrontEASE.Client.Infrastructure.Settings.AppSettings.Pages;

namespace FrontEASE.Client.Infrastructure.Settings.AppSettings
{
    public class AppSettings
    {
        public PageSettings? PageSettings { get; set; }
        public ApiSettings? ApiSettings { get; set; }
        public IntegrationSettings? IntegrationSettings { get; set; }
        public EnvironmentSettings? EnvironmentSettings { get; set; }
    }
}
