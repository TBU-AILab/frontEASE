using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Infrastructure.Constants.Auth;
using Microsoft.AspNetCore.Identity;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Users.Defaults
{
    public class DefaultUserRolesConfiguration
    {
        public static IList<IdentityRole> GetDefaults(AppSettings settings)
        {
            var userRoles = new List<IdentityRole>();

            userRoles.AddRange(InitBaseRole(settings));
            userRoles.AddRange(InitElevatedRoles(settings));
            return userRoles;
        }

        private static IList<IdentityRole> InitBaseRole(AppSettings settings)
        {
            return
            [
                /* User */
                new IdentityRole()
                {
                    Name = UserRoleNames.UserRoleName,
                    NormalizedName = UserRoleNames.UserRoleNameNormalized,
                    Id = settings!.AuthSettings!.Defaults!.Roles!.UserGuid!.Value.ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
            ];
        }

        private static IList<IdentityRole> InitElevatedRoles(AppSettings settings)
        {
            return
            [
                /* Admin */ 
                new IdentityRole()
                {
                    Name = UserRoleNames.AdminRoleName,
                    NormalizedName = UserRoleNames.AdminRoleNameNormalized,
                    Id = settings!.AuthSettings!.Defaults!.Roles!.AdminGuid!.Value.ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },

                /* Superadmin */
                new IdentityRole()
                {
                    Name = UserRoleNames.SuperadminRoleName,
                    NormalizedName = UserRoleNames.SuperadminRoleNameNormalized,
                    Id = settings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.Value.ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            ];
        }
    }
}
