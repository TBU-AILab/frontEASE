using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Users.Defaults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(2)]
    public class UserGenerator : IGenerator
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly AppSettings _settings;

        public UserGenerator(ApplicationDbContext dataContext, AppSettings settings)
        {
            _dataContext = dataContext;
            _settings = settings;
        }

        public async Task Generate()
        {
            var roles = DefaultUserRolesConfiguration.GetDefaults(_settings).ToList();
            var users = DefaultUsersConfiguration.GetDefaults(_settings).ToList();

            var roleGuidsExpected = roles.Select(x => x.Id);
            var roleGuidsReal = _dataContext.Roles.Select(x => x.Id);
            var rolesMissing = roleGuidsExpected.Except(roleGuidsReal);
            foreach (var role in rolesMissing)
            {
                await _dataContext.Roles.AddAsync(roles.Single(x => x.Id == role));
            }

            var superadminRoleId = _settings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.ToString()!;
            bool superadminAlreadyExists = await _dataContext.UserRoles.AnyAsync(ur => ur.RoleId == superadminRoleId);

            if (!superadminAlreadyExists)
            {
                var superadminDefaultUser = users.SingleOrDefault(u => u.Email == _settings.AuthSettings!.Defaults!.Superadmin!.Email);
                if (superadminDefaultUser is not null)
                {
                    var existingSuperadmin = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == superadminDefaultUser.Email);
                    if (existingSuperadmin is null)
                    {
                        await _dataContext.Users.AddAsync(superadminDefaultUser);
                        existingSuperadmin = superadminDefaultUser;
                    }

                    if (!await _dataContext.UserRoles.AnyAsync(ur => ur.UserId == existingSuperadmin.Id && ur.RoleId == superadminRoleId))
                    {
                        var superadminUserRole = new IdentityUserRole<string>()
                        {
                            RoleId = superadminRoleId,
                            UserId = existingSuperadmin.Id
                        };
                        await _dataContext.UserRoles.AddAsync(superadminUserRole);
                    }
                }
            }

            foreach (var user in users)
            {
                if (user.Email != _settings.AuthSettings!.Defaults!.Superadmin!.Email)
                {
                    bool userExists = await _dataContext.Users.AnyAsync(u => u.Email == user.Email);
                    if (!userExists)
                    {
                        await _dataContext.Users.AddAsync(user);
                    }
                }
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}