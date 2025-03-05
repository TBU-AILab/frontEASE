using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Shared.Images;
using Microsoft.AspNetCore.Identity;

namespace FrontEASE.Domain.Entities.Shared.Users
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
