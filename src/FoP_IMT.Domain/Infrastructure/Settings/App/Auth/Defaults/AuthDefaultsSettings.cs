using FoP_IMT.Domain.Infrastructure.Settings.App.Auth.Defaults.Roles;
using FoP_IMT.Domain.Infrastructure.Settings.App.Auth.Defaults.Superuser;

namespace FoP_IMT.Domain.Infrastructure.Settings.App.Auth.Defaults
{
    public class AuthDefaultsSettings
    {
        public SuperadminDefaults? Superadmin { get; set; }
        public RoleDefaults? Roles { get; set; }
    }
}
