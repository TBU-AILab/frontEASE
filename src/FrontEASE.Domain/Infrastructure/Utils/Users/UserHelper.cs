using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Domain.Infrastructure.Utils.Users
{
    public class UserHelper(AppSettings appSettings) : IUserHelper
    {
        public UserRole UserRoleGuidToRole(Guid roleID)
        {
            if (appSettings!.AuthSettings!.Defaults!.Roles!.UserGuid!.Value == roleID) { return UserRole.USER; }
            else if (appSettings!.AuthSettings!.Defaults!.Roles!.AdminGuid!.Value == roleID) { return UserRole.ADMIN; }
            else if (appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.Value == roleID) { return UserRole.OWNER; }

            return UserRole.USER;
        }
    }
}
