using FrontEASE.Client.Shared.Constants;
using FrontEASE.Shared.Data.Enums.Shared.UI;

namespace FrontEASE.Client.Shared.Dictionaries.Shortcuts
{
    public class ShortcutKeyDictionary : IShortcutKeyDictionary
    {
        private readonly IDictionary<ShortcutKeys, string> _dictionaryToText;
        private readonly IDictionary<string, ShortcutKeys> _dictionaryToEnum;

        public ShortcutKeyDictionary()
        {
            _dictionaryToText = new Dictionary<ShortcutKeys, string>()
            {
                { ShortcutKeys.ENTER, KeyboardConstants.EnterKeyName },
                { ShortcutKeys.ESCAPE, KeyboardConstants.EscapeKeyName },
            };

            _dictionaryToEnum = _dictionaryToText.ToDictionary(x => x.Value, x => x.Key);
        }

        public string? GetTextForm(ShortcutKeys? key) =>
            key is null ? null : _dictionaryToText.TryGetValue(key.Value, out string? value) ? value : null;

        public ShortcutKeys? GetEnumForm(string? key) =>
            key is null ? null : _dictionaryToEnum.TryGetValue(key, out ShortcutKeys value) ? value : null;
    }
}
