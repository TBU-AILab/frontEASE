using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Client.Services.ModelManipulationServices.User
{
    public interface IUserManipulationService
    {
        void ChangeUserRole(ApplicationUserDto user, UserRole role);
        void ConsolidateUserModel(ApplicationUserDto user);
        void ReinitializeModel(ApplicationUserDto user);
    }
}
