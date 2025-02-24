using FoP_IMT.Shared.Data.DTOs.Shared.Users;
using FoP_IMT.Shared.Data.Enums.Auth;

namespace FoP_IMT.Client.Services.ModelManipulationServices.User
{
    public interface IUserManipulationService
    {
        void ChangeUserRole(ApplicationUserDto user, UserRole role);
        void ConsolidateUserModel(ApplicationUserDto user);
        void ReinitializeModel(ApplicationUserDto user);
    }
}
