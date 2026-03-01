using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Domain.Infrastructure.Utils.Users
{
    public interface IUserHelper
    {
        UserRole UserRoleGuidToRole(Guid roleID);
    }
}
