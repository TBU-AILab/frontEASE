using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Client.Contexts
{
    public class ApplicationContext
    {
        public ApplicationContext()
        {
            LoggedInUser = new();
        }

        public ApplicationUserDto LoggedInUser { get; set; }
    }
}
