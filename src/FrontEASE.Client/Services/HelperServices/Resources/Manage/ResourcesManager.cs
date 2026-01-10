using FrontEASE.Client.Infrastructure.Settings.AppSettings;
using FrontEASE.Client.Services.ApiServices.Shared.Resources;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Services.HelperServices.Resources.Manage
{
    public class ResourcesManager(IResourceHandler resourceHandler, IResourceApiService resourceApiService, AppSettings appSettings) : IResourcesManager
    {
        public async Task AssureResourcesInitialized()
        {
            if (!resourceHandler.CheckResourcesInitialized())
            {
                resourceHandler.InitLanguage(appSettings.EnvironmentSettings!.LanguageCode!);
                var resources = await resourceApiService.LoadResources(resourceHandler.CurrentLanguage);
                resourceHandler.InitResources(resources);
            }
        }
    }
}
