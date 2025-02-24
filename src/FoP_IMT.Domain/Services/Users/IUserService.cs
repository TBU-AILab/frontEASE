using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Shared.Data.Enums.Auth;

namespace FoP_IMT.Domain.Services.Users
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
