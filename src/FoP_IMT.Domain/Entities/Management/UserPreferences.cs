using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Management.General;
using FoP_IMT.Domain.Entities.Management.Tokens;
using FoP_IMT.Domain.Entities.Shared.Users;

namespace FoP_IMT.Domain.Entities.Management
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
