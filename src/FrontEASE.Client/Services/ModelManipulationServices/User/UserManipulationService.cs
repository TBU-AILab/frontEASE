using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Client.Services.ModelManipulationServices.User
{
    public class UserManipulationService : IUserManipulationService
    {
        private readonly IMapper _mapper;

        public UserManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

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
            _mapper.Map(cleanModel, user);
        }
    }
}
