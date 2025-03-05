using Blazorise;
using FrontEASE.Client.Shared.Styling.Constants;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FrontEASE.Client.Shared.Styling.Defaults
{
    public static class ThemeDefaults
    {
        private static readonly IDictionary<ColorScheme, ColorConstants> SchemeConstants = new Dictionary<ColorScheme, ColorConstants>()
        {
            { ColorScheme.LIGHT, ThemeConstants.LightThemeConstants },
            { ColorScheme.DARK, ThemeConstants.DarkThemeConstants }
        };

        public static Theme GetThemeDefaults(ColorScheme scheme)
        {
            var selectedTheme = SchemeConstants[scheme];

            var theme = new Theme()
            {
                ColorOptions = new ThemeColorOptions()
                {
                    Primary = selectedTheme.Primary,
                    Secondary = selectedTheme.Secondary,
                    Danger = selectedTheme.Danger,
                    Success = selectedTheme.Success,
                    Info = selectedTheme.Info,
                    Warning = selectedTheme.Warning,
                    Dark = selectedTheme.Dark,
                    Light = selectedTheme.Light
                },
                TextColorOptions = new ThemeTextColorOptions()
                {
                    Primary = selectedTheme.Primary,
                    Secondary = selectedTheme.Secondary,
                    Danger = selectedTheme.Danger,
                    Success = selectedTheme.Success,
                    Info = selectedTheme.Info,
                    Warning = selectedTheme.Warning,
                    Dark = selectedTheme.Dark,
                    Light = selectedTheme.Light
                },
                BackgroundOptions = new ThemeBackgroundOptions()
                {
                    Primary = selectedTheme.Primary,
                    Secondary = selectedTheme.Secondary,
                    Danger = selectedTheme.Danger,
                    Success = selectedTheme.Success,
                    Info = selectedTheme.Info,
                    Warning = selectedTheme.Warning,
                    Dark = selectedTheme.Dark,
                    Light = selectedTheme.Light
                },
                DropdownOptions = new ThemeDropdownOptions()
                {
                    ToggleIconVisible = true,
                    GradientBlendPercentage = 0f
                },
                InputOptions = new ThemeInputOptions()
                {
                    Size = Size.Medium,
                    CheckColor = selectedTheme.Primary,
                    SliderColor = selectedTheme.Primary
                },
                DividerOptions = new ThemeDividerOptions()
                {
                    DividerType = DividerType.Solid
                },
                ModalOptions = new ThemeModalOptions()
                {
                    BorderRadius = $"{6} px"
                },
                Black = selectedTheme.Black,
                Enabled = true,
            };

            return theme;
        }
    }
}
