using FrontEASE.Domain.Infrastructure.Settings.App.Auth.Defaults.Roles;
using FrontEASE.Domain.Infrastructure.Settings.App.Auth.Defaults.Superuser;

namespace FrontEASE.Domain.Infrastructure.Settings.App.Auth.Defaults
{
    public class AuthDefaultsSettings
    {
        public SuperadminDefaults? Superadmin { get; set; }
        public RoleDefaults? Roles { get; set; }
    }
}
