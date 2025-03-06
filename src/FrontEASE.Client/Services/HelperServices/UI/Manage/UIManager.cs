using Blazorise;
using FrontEASE.Client.Shared.Styling.Defaults;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FrontEASE.Client.Services.HelperServices.UI.Manage
{
    public class UIManager : IUIManager
    {
        public Theme Theme { get; private set; }
        private readonly IMemoryCache _memoryCache;

        public UIManager(IMemoryCache memoryCache)
        {
            Theme = new Theme();
            _memoryCache = memoryCache;
        }

        public bool CheckPreferencesInitialized()
        {
            var resourcesInited = _memoryCache.TryGetValue(CacheNameConstants.PreferencesDictionaryKey, out _);
            return resourcesInited;
        }

        public void InitPreferences(UserPreferencesDto userPrefs) => _memoryCache.Set(CacheNameConstants.PreferencesDictionaryKey, userPrefs);

        public void HandleUIPreferencesStateChange(UserPreferencesDto? userPrefs)
        {
            if (userPrefs is not null)
            {
                if (userPrefs?.GeneralOptions is not null)
                {
                    var themeColor = ThemeDefaults.GetThemeDefaults(userPrefs.GeneralOptions.ColorScheme);
                    ChangeTheme(themeColor);
                }
                InitPreferences(userPrefs!);
            }
        }

        public void ChangeTheme(Theme? theme)
        {
            if (theme is not null)
            {
                Theme.White = theme.White;
                Theme.LuminanceThreshold = theme.LuminanceThreshold;
                Theme.AlertOptions = theme.AlertOptions;
                Theme.BackgroundOptions = theme.BackgroundOptions;
                Theme.BadgeOptions = theme.BadgeOptions;

                Theme.ColorOptions = theme.ColorOptions;
                Theme.TextColorOptions = theme.TextColorOptions;
                Theme.DropdownOptions = theme.DropdownOptions;
                Theme.InputOptions = theme.InputOptions;
                Theme.DividerOptions = theme.DividerOptions;
                Theme.ModalOptions = theme.ModalOptions;
                Theme.Black = theme.Black;

                Theme.ThemeHasChanged();
            }
        }
    }
}
