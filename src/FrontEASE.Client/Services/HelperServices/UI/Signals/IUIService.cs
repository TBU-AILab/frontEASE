namespace FrontEASE.Client.Services.HelperServices.UI.Signals
{
    public interface IUIService
    {
        #region UI Events

        event Func<Task>? RefreshRequested;

        Task CallRequestRefreshAsync();

        #endregion

        #region Preferences
        Task AssureResourcesInitialized(bool forceRefresh = false);

        #endregion
    }
}
