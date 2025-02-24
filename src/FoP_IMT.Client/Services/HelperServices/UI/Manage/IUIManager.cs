using Blazorise;
using FoP_IMT.Shared.Data.DTOs.Management;

namespace FoP_IMT.Client.Services.HelperServices.UI.Manage
{
    public interface IUIManager
    {
        Theme Theme { get; }

        bool CheckPreferencesInitialized();
        void InitPreferences(UserPreferencesDto userPrefs);
        void HandleUIPreferencesStateChange(UserPreferencesDto? userPrefs);
    }
}
