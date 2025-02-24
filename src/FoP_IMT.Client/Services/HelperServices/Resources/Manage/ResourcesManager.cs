using FoP_IMT.Client.Infrastructure.Settings.AppSettings;
using FoP_IMT.Client.Services.ApiServices.Shared.Resources;
using FoP_IMT.Shared.Services.Resources;

namespace FoP_IMT.Client.Services.HelperServices.Resources.Manage
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
