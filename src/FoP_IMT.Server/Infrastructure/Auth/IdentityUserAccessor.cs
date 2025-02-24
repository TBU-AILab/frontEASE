using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Shared.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.Server.Infrastructure.Auth
{
    public class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            try
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user is null)
                {
                    redirectManager.RedirectToWithStatus("Account/Login", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
                }

                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null!;
        }
    }
}
