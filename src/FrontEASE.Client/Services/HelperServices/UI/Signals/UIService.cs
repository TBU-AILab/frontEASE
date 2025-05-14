using FrontEASE.Client.Services.ApiServices.Management;
using FrontEASE.Client.Services.HelperServices.UI.Manage;

namespace FrontEASE.Client.Services.HelperServices.UI.Signals
{
    public class UIService : IUIService
    {
        private readonly IUIManager _uiManager;
        private readonly IManagementApiService _managementApiService;

        public UIService(IUIManager uiManager, IManagementApiService managementApiService)
        {
            _uiManager = uiManager;
            _managementApiService = managementApiService;
        }

        #region UI Events

        public event Func<Task>? RefreshRequested;

        public async Task CallRequestRefreshAsync()
        {
            if (RefreshRequested != null)
            {
                var invocationList = RefreshRequested.GetInvocationList();
                var tasks = invocationList
                    .Cast<Func<Task>>()
                    .Select(handler => handler.Invoke());
                await Task.WhenAll(tasks);
            }
        }

        #endregion

        #region Preferences

        public async Task AssureResourcesInitialized(bool forceRefresh = false)
        {
            if (!_uiManager.CheckPreferencesInitialized() || forceRefresh)
            {
                var preferences = await _managementApiService.LoadPreferences();
                if (preferences is not null)
                {
                    _uiManager.HandleUIPreferencesStateChange(preferences);
                    _uiManager.InitPreferences(preferences);
                    await CallRequestRefreshAsync();
                }
            }
        }

        #endregion
    }
}
