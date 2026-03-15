using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FrontEASE.Domain.Entities.Management.General.Columns
{
    public class UserPreferenceGeneralOptionTaskGridColumn : EntityBase
    {
        public UserPreferenceGeneralOptionTaskGridColumn()
        {
            GeneralOptions = null!;
        }

        #region Navigation

        public Guid GeneralOptionsID { get; set; }
        public UserPreferenceGeneralOptions GeneralOptions { get; set; }

        #endregion

        #region Data

        public TaskGridColumnsVisibility Column { get; set; }

        #endregion
    }
}