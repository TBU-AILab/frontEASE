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
                { ShortcutKeys.ESCAPE, KeyboardConstants.EscapeKeyName }
            };

            _dictionaryToEnum = _dictionaryToText.ToDictionary(x => x.Value, x => x.Key);
        }

        public string? GetTextForm(ShortcutKeys key)
        {
            return _dictionaryToText.TryGetValue(key, out string? value) ? value : null;
        }

        public ShortcutKeys? GetEnumForm(string key)
        {
            return _dictionaryToEnum.TryGetValue(key, out ShortcutKeys value) ? value : null;
        }
    }
}
