using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Domain.Services.Users
{
    public interface IUserService
    {
        Task<IList<ApplicationUser>> LoadDuplicities(ApplicationUser user);
        Task<IList<ApplicationUser>> LoadAll();
        Task<ApplicationUser?> Load(Guid id);
        Task<ApplicationUser?> Load(string email);
        Task<ApplicationUser> Create(ApplicationUser user, UserRole role, string password);
        Task<ApplicationUser> Update(ApplicationUser user, UserRole role);
        Task Delete(ApplicationUser user);
    }
}
