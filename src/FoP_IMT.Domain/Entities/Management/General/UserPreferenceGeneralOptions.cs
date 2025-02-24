using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FoP_IMT.Domain.Entities.Management.General
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

        #endregion
    }
}
