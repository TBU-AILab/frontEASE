using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Management.General.Columns;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FrontEASE.Domain.Entities.Management.General
{
    public class UserPreferenceGeneralOptions : EntityBase
    {
        public UserPreferenceGeneralOptions()
        {
            UserPreferences = null!;
            TaskGridColumns = [];
        }

        #region Navigation

        public UserPreferences UserPreferences { get; set; }
        public IList<UserPreferenceGeneralOptionTaskGridColumn> TaskGridColumns { get; set; }

        #endregion

        #region Data

        public ColorScheme ColorScheme { get; set; }
        public TokenVisibility TokenVisibility { get; set; }
        public MessageDisplayFormat UserMessageDisplayFormat { get; set; }
        public MessageDisplayFormat SystemMessageDisplayFormat { get; set; }

        #endregion
    }
}
