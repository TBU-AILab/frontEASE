using FrontEASE.Client.Shared.Constants;
using FrontEASE.Shared.Data.Enums.Shared.UI;

namespace FrontEASE.Client.Shared.Dictionaries.Shortcuts
{
    public static class ShortcutKeyDictionary
    {
        private static readonly IDictionary<ShortcutKeys, string> _dictionaryToText = new Dictionary<ShortcutKeys, string>()
        {
            { ShortcutKeys.ENTER, KeyboardConstants.EnterKeyName },
            { ShortcutKeys.ESCAPE, KeyboardConstants.EscapeKeyName },
        };

        private static readonly IDictionary<string, ShortcutKeys> _dictionaryToEnum = new Dictionary<string, ShortcutKeys>(StringComparer.Ordinal)
        {
            { KeyboardConstants.EnterKeyName, ShortcutKeys.ENTER },
            { KeyboardConstants.EscapeKeyName, ShortcutKeys.ESCAPE },
        };

        public static string? GetTextForm(ShortcutKeys? key) =>
            key is null ? null : _dictionaryToText.TryGetValue(key.Value, out string? value) ? value : null;

        public static ShortcutKeys? GetEnumForm(string? key) =>
            key is null ? null : _dictionaryToEnum.TryGetValue(key, out ShortcutKeys value) ? value : null;
    }
}
