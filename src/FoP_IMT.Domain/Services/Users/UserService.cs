using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Infrastructure.Exceptions.Types;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Repositories.Users;
using FoP_IMT.Domain.Services.Shared.Images;
using FoP_IMT.Shared.Data.Enums.Auth;
using FoP_IMT.Shared.Data.Enums.Shared.Images;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.Domain.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IImageService imageService, AppSettings appSettings)
        {
            _imageService = imageService;
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public async Task<IList<ApplicationUser>> LoadAll()
        {
            var users = await _userRepository.LoadAll();
            return users;
        }

        public async Task<ApplicationUser?> Load(Guid id)
        {
            var user = await _userRepository.Load(id) ?? throw new NotFoundException();
            return user;
        }

        public async Task<ApplicationUser?> Load(string email)
        {
            var user = (await _userRepository.LoadWhere(u =>
                u.Email == email)).FirstOrDefault() ?? throw new NotFoundException();

            return user;
        }

        public async Task<IList<ApplicationUser>> LoadDuplicities(ApplicationUser user)
        {
            var users = await _userRepository.LoadWhere(u =>
                u.Email == user.Email ||
                u.UserName == user.UserName);

            return users;
        }

        public async Task<ApplicationUser> Create(ApplicationUser user, UserRole role, string password)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var newUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = user.Email,
                NormalizedEmail = user.Email!.ToUpperInvariant(),
                UserName = user.UserName,
                NormalizedUserName = user.UserName!.ToUpperInvariant(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,

                UserRole = UserRoleToIdentityRole(role),
                Image = user.Image
            };
            newUser.PasswordHash = hasher.HashPassword(newUser, password);

            if (!string.IsNullOrEmpty(user?.Image?.ImageData))
            {
                user!.Image!.Type = ImageType.USER_PROFILE_PICTURE;
                await _imageService.SaveImage(newUser!.Image!, Guid.Parse(newUser.Id));
            }
            else
            {
                if (string.IsNullOrEmpty(user?.Image?.ImageUrl))
                {
                    user!.Image = null;
                }
            }

            newUser = await _userRepository.Insert(newUser);
            return newUser;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user, UserRole role)
        {
            user.UserRole = UserRoleToIdentityRole(role);
            user.NormalizedEmail = user.Email!.ToUpperInvariant();
            user.NormalizedUserName = user.UserName!.ToUpperInvariant();

            if (!string.IsNullOrEmpty(user?.Image?.ImageData))
            {
                user!.Image!.Type = ImageType.USER_PROFILE_PICTURE;
                await _imageService.SaveImage(user!.Image!, Guid.Parse(user.Id));
            }
            else
            {
                if (string.IsNullOrEmpty(user?.Image?.ImageUrl))
                {
                    user!.Image = null;
                }
            }

            var updatedUser = await _userRepository.Update(user);
            return updatedUser;
        }

        public async Task Delete(ApplicationUser user)
        {
            await _userRepository.Delete(user);
            if (!string.IsNullOrEmpty(user?.Image?.ImageUrl))
            { _imageService.DeleteImage(user?.Image!); }
        }

        private IdentityUserRole<string> UserRoleToIdentityRole(UserRole role)
        {
            var roleId = UserRoleToIdentityRoleGuid(role);
            var identityRole = new IdentityUserRole<string>()
            {
                UserId = null!,
                RoleId = roleId.ToString()
            };
            return identityRole;
        }

        private Guid UserRoleToIdentityRoleGuid(UserRole role)
        {
            return role switch
            {
                UserRole.USER => _appSettings.AuthSettings!.Defaults!.Roles!.UserGuid!.Value,
                UserRole.ADMIN => _appSettings.AuthSettings!.Defaults!.Roles!.AdminGuid!.Value,
                UserRole.OWNER => _appSettings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.Value,
                _ => Guid.Empty,
            };
        }
    }
}
