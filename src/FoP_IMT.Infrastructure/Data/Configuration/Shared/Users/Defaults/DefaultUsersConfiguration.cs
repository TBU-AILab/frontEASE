using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Users.Defaults
{
    public class DefaultUsersConfiguration
    {
        public static IList<ApplicationUser> GetDefaults(AppSettings settings)
        {
            var users = new List<ApplicationUser>
            {
                InitSuperadmin(settings)
            };
            return users;
        }

        private static ApplicationUser InitSuperadmin(AppSettings settings)
        {
            var saData = settings!.AuthSettings!.Defaults!.Superadmin!;

            var superAdmin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = saData.Email!,
                EmailConfirmed = true,
                UserName = saData.UserName!,
                NormalizedUserName = saData.UserName!.ToUpperInvariant(),
                NormalizedEmail = saData.Email!.ToUpperInvariant()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            superAdmin.PasswordHash = hasher.HashPassword(superAdmin, saData.Password!);

            return superAdmin;
        }
    }
}