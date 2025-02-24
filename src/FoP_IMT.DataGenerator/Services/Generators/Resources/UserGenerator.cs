using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Infrastructure.Data;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Users.Defaults;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.DataGenerator.Services.Generators.Resources
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

            foreach(var role in rolesMissing)
            {
                await _dataContext.Roles.AddAsync(roles.Single(x => x.Id == role));
            }

            await _dataContext.Users.AddRangeAsync(users);

            var superadminMail = _settings.AuthSettings!.Defaults!.Superadmin!.Email!.ToString();
            var superadmin = users.SingleOrDefault(x => x.Email == superadminMail);
            if (superadmin is not null)
            {
                var superadminRole = new IdentityUserRole<string>()
                {
                    RoleId = _settings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.ToString()!,
                    UserId = superadmin.Id
                };
                await _dataContext.UserRoles.AddAsync(superadminRole);
            }
            await _dataContext.SaveChangesAsync();
        }
    }
}
