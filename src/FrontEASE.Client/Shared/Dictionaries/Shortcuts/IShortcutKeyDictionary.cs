using FrontEASE.Shared.Data.Enums.Shared.UI;

namespace FrontEASE.Client.Shared.Dictionaries.Shortcuts
{
    public interface IShortcutKeyDictionary
    {
        string? GetTextForm(ShortcutKeys? key);
        ShortcutKeys? GetEnumForm(string? key);
    }
}
