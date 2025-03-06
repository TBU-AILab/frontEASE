using FrontEASE.Client.Infrastructure.Settings.AppSettings.Api;
using FrontEASE.Client.Infrastructure.Settings.AppSettings.Environment;
using FrontEASE.Client.Infrastructure.Settings.AppSettings.Integration;

namespace FrontEASE.Client.Infrastructure.Settings.AppSettings
{
    public class AppSettings
    {
        public ApiSettings? ApiSettings { get; set; }
        public IntegrationSettings? IntegrationSettings { get; set; }
        public EnvironmentSettings? EnvironmentSettings { get; set; }
    }
}
