using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FrontEASE.Domain.Entities.Management.General
{
    public class UserPreferenceGeneralOptions : EntityBase
    {
        public UserPreferenceGeneralOptions()
        {
            UserPreferences = null!;
        }

        #region Navigation

        public UserPreferences UserPreferences { get; set; }

        #endregion

        #region Data

        public ColorScheme ColorScheme { get; set; }
        public TokenVisibility TokenVisibility { get; set; }

        #endregion
    }
}
