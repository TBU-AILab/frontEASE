using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Client.Services.ModelManipulationServices.User
{
    public class UserManipulationService(IMapper mapper) : IUserManipulationService
    {
        public void ChangeUserRole(ApplicationUserDto user, UserRole role) => user.Role = role;

        public void ConsolidateUserModel(ApplicationUserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Image?.ImageData) && string.IsNullOrWhiteSpace(user.Image?.ImageUrl))
            { user.Image = null; }
            if (string.IsNullOrWhiteSpace(user.Password))
            { user.Password = null; }
        }

        public void ReinitializeModel(ApplicationUserDto user)
        {
            var cleanModel = new ApplicationUserDto()
            {
                Image = new ImageDto()
            };
            mapper.Map(cleanModel, user);
        }
    }
}
