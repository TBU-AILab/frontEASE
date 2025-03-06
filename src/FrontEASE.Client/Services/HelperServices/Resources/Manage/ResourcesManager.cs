using FrontEASE.Client.Infrastructure.Settings.AppSettings;
using FrontEASE.Client.Services.ApiServices.Shared.Resources;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Services.HelperServices.Resources.Manage
{
    public class ResourcesManager : IResourcesManager
    {
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceApiService _resourceApiService;

        private readonly AppSettings _appSettings;

        public ResourcesManager(IResourceHandler resourceHandler, IResourceApiService resourceApiService, AppSettings appSettings)
        {
            _resourceApiService = resourceApiService;
            _resourceHandler = resourceHandler;
            _appSettings = appSettings;
        }

        public async Task AssureResourcesInitialized()
        {
            if (!_resourceHandler.CheckResourcesInitialized())
            {
                _resourceHandler.InitLanguage(_appSettings.EnvironmentSettings!.LanguageCode!);
                var resources = await _resourceApiService.LoadResources(_resourceHandler.CurrentLanguage);
                _resourceHandler.InitResources(resources);
            }
        }
    }
}
