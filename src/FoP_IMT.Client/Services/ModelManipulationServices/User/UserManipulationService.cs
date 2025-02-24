using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Shared.Images;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;
using FoP_IMT.Shared.Data.Enums.Auth;

namespace FoP_IMT.Client.Services.ModelManipulationServices.User
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
