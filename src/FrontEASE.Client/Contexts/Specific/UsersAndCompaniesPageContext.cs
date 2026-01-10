using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Client.Contexts.Specific
{
    public class UsersAndCompaniesPageContext : ApplicationContext
    {
        public UsersAndCompaniesPageContext(ApplicationContext appContext)
        {
            LoggedInUser = appContext.LoggedInUser;
            UserPreferences = appContext.UserPreferences;

            Companies = [];
            Users = [];
        }

        public IList<CompanyDto> Companies { get; set; }
        public IList<ApplicationUserDto> Users { get; set; }
    }
}
