using FoP_IMT.Client.Infrastructure.Settings.AppSettings.Environment;
using FoP_IMT.Client.Infrastructure.Settings.AppSettings.Integration;

namespace FoP_IMT.Client.Infrastructure.Settings.AppSettings
{
    public class AppSettings
    {
        public EnvironmentSettings? EnvironmentSettings { get; set; }
        public IntegrationSettings? IntegrationSettings { get; set; }
    }
}
