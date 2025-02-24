namespace FoP_IMT.Client.Services.HelperServices.UI.Signals
{
    public interface IUIService
    {
        #region UI Events

        event Action RefreshRequested;
        void CallRequestRefresh();

        #endregion

        #region Preferences
        Task AssureResourcesInitialized();

        #endregion
    }
}
