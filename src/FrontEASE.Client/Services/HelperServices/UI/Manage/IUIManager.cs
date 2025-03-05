using Blazorise;
using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Client.Services.HelperServices.UI.Manage
{
    public interface IUIManager
    {
        Theme Theme { get; }

        void ChangeTheme(Theme? theme);
        bool CheckPreferencesInitialized();
        void InitPreferences(UserPreferencesDto userPrefs);
        void HandleUIPreferencesStateChange(UserPreferencesDto? userPrefs);
    }
}
