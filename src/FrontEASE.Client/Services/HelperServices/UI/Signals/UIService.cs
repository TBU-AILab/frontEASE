using FrontEASE.Client.Services.ApiServices.Management;
using FrontEASE.Client.Services.HelperServices.UI.Manage;

namespace FrontEASE.Client.Services.HelperServices.UI.Signals
{
    public class UIService(IUIManager uiManager, IManagementApiService managementApiService) : IUIService
    {

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
            if (!uiManager.CheckPreferencesInitialized() || forceRefresh)
            {
                var preferences = await managementApiService.LoadPreferences();
                if (preferences is not null)
                {
                    uiManager.HandleUIPreferencesStateChange(preferences);
                    uiManager.InitPreferences(preferences);
                    await CallRequestRefreshAsync();
                }
            }
        }

        #endregion
    }
}
