using Blazorise;
using FoP_IMT.Client.Shared.Styling.Defaults;
using FoP_IMT.Shared.Data.DTOs.Management;
using FoP_IMT.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FoP_IMT.Client.Services.HelperServices.UI.Manage
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

        public void InitPreferences(UserPreferencesDto userPrefs) =>
            _memoryCache.Set(CacheNameConstants.PreferencesDictionaryKey, userPrefs);

        public void HandleUIPreferencesStateChange(UserPreferencesDto? userPrefs)
        {
            if (userPrefs is not null)
            {
                if (userPrefs?.GeneralOptions is not null)
                {
                    var themeColor = ThemeDefaults.GetThemeDefaults(userPrefs.GeneralOptions.ColorScheme);

                    Theme.White = themeColor.White;
                    Theme.LuminanceThreshold = themeColor.LuminanceThreshold;
                    Theme.AlertOptions = themeColor.AlertOptions;
                    Theme.BackgroundOptions = themeColor.BackgroundOptions;
                    Theme.BadgeOptions = themeColor.BadgeOptions;

                    Theme.ColorOptions = themeColor.ColorOptions;
                    Theme.TextColorOptions = themeColor.TextColorOptions;
                    Theme.DropdownOptions = themeColor.DropdownOptions;
                    Theme.InputOptions = themeColor.InputOptions;
                    Theme.DividerOptions = themeColor.DividerOptions;
                    Theme.ModalOptions = themeColor.ModalOptions;
                    Theme.Black = themeColor.Black;

                    Theme.ThemeHasChanged();
                }
                InitPreferences(userPrefs!);
            }
        }
    }
}
