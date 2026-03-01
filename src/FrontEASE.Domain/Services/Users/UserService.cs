using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Data.Enums.Shared.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FrontEASE.Domain.Services.Users
{
    public class UserService(
        IUserRepository userRepository,
        IImageService imageService,
        IHttpContextAccessor contextAccessor,
        AppSettings appSettings) : IUserService
    {
        public async Task<Guid> LoadCurrentUserId(CancellationToken cancellationToken)
        {
            var user = await LoadCurrentUser(cancellationToken);
            return Guid.Parse(user!.Id);
        }

        public async Task<ApplicationUser?> LoadCurrentUser(CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var currentUser = await Load(userMail, cancellationToken);

            return currentUser;
        }

        public async Task<IList<ApplicationUser>> LoadAll(CancellationToken cancellationToken)
        {
            var users = await userRepository.LoadAll(cancellationToken);
            return users;
        }

        public async Task<ApplicationUser?> Load(Guid id, CancellationToken cancellationToken)
        {
            var user = await userRepository.Load(id, cancellationToken) ?? throw new NotFoundException();
            return user;
        }

        public async Task<ApplicationUser?> Load(string email, CancellationToken cancellationToken)
        {
            var user = (await userRepository.LoadWhere(u => 
                u.Email == email,
                cancellationToken))
                .FirstOrDefault() ?? throw new NotFoundException();

            return user;
        }

        public async Task<IList<ApplicationUser>> LoadDuplicities(ApplicationUser user, CancellationToken cancellationToken)
        {
            var users = await userRepository.LoadWhere(u =>
                u.Email == user.Email ||
                u.UserName == user.UserName,
                cancellationToken);

            return users;
        }

        public async Task<ApplicationUser> Create(ApplicationUser user, UserRole role, string password, CancellationToken cancellationToken)
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
                await imageService.SaveImage(newUser!.Image!, Guid.Parse(newUser.Id), cancellationToken);
            }
            else
            {
                if (string.IsNullOrEmpty(user?.Image?.ImageUrl))
                {
                    user!.Image = null;
                }
            }

            newUser = await userRepository.Insert(newUser, cancellationToken);
            return newUser;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user, UserRole role, CancellationToken cancellationToken)
        {
            user.UserRole = UserRoleToIdentityRole(role);
            user.NormalizedEmail = user.Email!.ToUpperInvariant();
            user.NormalizedUserName = user.UserName!.ToUpperInvariant();

            if (!string.IsNullOrEmpty(user?.Image?.ImageData))
            {
                user!.Image!.Type = ImageType.USER_PROFILE_PICTURE;
                await imageService.SaveImage(user!.Image!, Guid.Parse(user.Id), cancellationToken);
            }
            else
            {
                if (string.IsNullOrEmpty(user?.Image?.ImageUrl))
                {
                    user!.Image = null;
                }
            }

            var updatedUser = await userRepository.Update(user, cancellationToken);
            return updatedUser;
        }

        public async Task Delete(ApplicationUser user, CancellationToken cancellationToken)
        {
            await userRepository.Delete(user, cancellationToken);
            if (!string.IsNullOrEmpty(user?.Image?.ImageUrl))
            { imageService.DeleteImage(user?.Image!); }
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
                UserRole.USER => appSettings.AuthSettings!.Defaults!.Roles!.UserGuid!.Value,
                UserRole.ADMIN => appSettings.AuthSettings!.Defaults!.Roles!.AdminGuid!.Value,
                UserRole.OWNER => appSettings.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.Value,
                _ => Guid.Empty,
            };
        }
    }
}
