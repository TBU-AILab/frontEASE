using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoP_IMT.DataGenerator.Services.Erasers.Resources
{
    [Order(2)]
    public class UserEraser : IEraser
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly AppSettings _settings;

        public UserEraser(ApplicationDbContext dataContext, AppSettings settings)
        {
            _dataContext = dataContext;
            _settings = settings;
        }

        public async Task Erase()
        {
            var adminRoleId = _settings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.ToString();
            var admins = await _dataContext.Users.Where(x => x.UserRole!.RoleId == adminRoleId).ToListAsync();

            var defaultAdmin = admins.SingleOrDefault(x => string.Equals(x.Email, _settings.AuthSettings.Defaults.Superadmin!.Email, StringComparison.InvariantCultureIgnoreCase));

            if (defaultAdmin is not null)
            {
                _dataContext.Users.Remove(defaultAdmin);
            }
            await _dataContext.SaveChangesAsync();
        }
    }
}
