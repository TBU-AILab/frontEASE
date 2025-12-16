using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Client.Contexts
{
    public class ApplicationContext
    {
        public ApplicationContext()
        {
            LoggedInUser = new();
            UserPreferences = new();
        }

        public ApplicationUserDto LoggedInUser { get; set; }
        public UserPreferencesDto UserPreferences { get; set; }
    }
}
