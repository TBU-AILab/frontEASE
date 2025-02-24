using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Entities.Shared.Images;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.Domain.Entities.Shared.Users
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Companies = [];
            Tasks = [];

            UserPreferences = new UserPreferences();
        }

        #region Navigation

        public Guid? ImageID { get; set; }
        public Image? Image { get; set; }

        public Guid UserPreferencesID { get; set; }
        public UserPreferences UserPreferences { get; set; }

        public IList<Company> Companies { get; set; }
        public IList<Tasks.Task> Tasks { get; set; }

        #endregion

        #region Data

        public IdentityUserRole<string>? UserRole { get; set; }

        #endregion
    }
}
