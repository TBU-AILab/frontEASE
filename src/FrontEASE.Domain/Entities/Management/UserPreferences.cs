using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Management.General;
using FrontEASE.Domain.Entities.Management.Tokens;
using FrontEASE.Domain.Entities.Shared.Users;

namespace FrontEASE.Domain.Entities.Management
{
    public class UserPreferences : EntityBase
    {
        public UserPreferences()
        {
            User = null!;
            TokenOptions = [];

            GeneralOptions = new();
        }

        #region Navigation

        public Guid GeneralOptionsID { get; set; }
        public UserPreferenceGeneralOptions GeneralOptions { get; set; }

        public ApplicationUser User { get; set; }

        public IList<UserPreferenceTokenOption> TokenOptions { get; set; }

        #endregion
    }
}
