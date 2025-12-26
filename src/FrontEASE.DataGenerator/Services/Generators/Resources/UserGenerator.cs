using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Users.Defaults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(2)]
    public class UserGenerator(ApplicationDbContext dataContext, AppSettings settings) : IGenerator
    {
        public async Task Generate()
        {
            var roles = DefaultUserRolesConfiguration.GetDefaults(settings);
            var users = DefaultUsersConfiguration.GetDefaults(settings);

            var roleGuidsExpected = roles.Select(x => x.Id);
            var roleGuidsReal = dataContext.Roles.Select(x => x.Id);
            var rolesMissing = roleGuidsExpected.Except(roleGuidsReal);
            foreach (var role in rolesMissing)
            {
                await dataContext.Roles.AddAsync(roles.Single(x => x.Id == role));
            }

            var superadminRoleId = settings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.ToString()!;
            var superadminAlreadyExists = await dataContext.UserRoles.AnyAsync(ur => ur.RoleId == superadminRoleId);

            if (!superadminAlreadyExists)
            {
                var superadminDefaultUser = users.SingleOrDefault(u => u.Email == settings.AuthSettings!.Defaults!.Superadmin!.Email);
                if (superadminDefaultUser is not null)
                {
                    var existingSuperadmin = await dataContext.Users.FirstOrDefaultAsync(u => u.Email == superadminDefaultUser.Email);
                    if (existingSuperadmin is null)
                    {
                        await dataContext.Users.AddAsync(superadminDefaultUser);
                        existingSuperadmin = superadminDefaultUser;
                    }

                    if (!await dataContext.UserRoles.AnyAsync(ur => ur.UserId == existingSuperadmin.Id && ur.RoleId == superadminRoleId))
                    {
                        var superadminUserRole = new IdentityUserRole<string>()
                        {
                            RoleId = superadminRoleId,
                            UserId = existingSuperadmin.Id
                        };
                        await dataContext.UserRoles.AddAsync(superadminUserRole);
                    }
                }
            }

            foreach (var user in users)
            {
                if (user.Email != settings.AuthSettings!.Defaults!.Superadmin!.Email)
                {
                    bool userExists = await dataContext.Users.AnyAsync(u => u.Email == user.Email);
                    if (!userExists)
                    {
                        await dataContext.Users.AddAsync(user);
                    }
                }
            }

            await dataContext.SaveChangesAsync();
        }
    }
}