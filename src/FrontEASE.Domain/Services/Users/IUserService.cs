using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;

namespace FrontEASE.Domain.Services.Users
{
    public interface IUserService
    {
        Task<IList<ApplicationUser>> LoadDuplicities(ApplicationUser user, CancellationToken cancellationToken);
        Task<IList<ApplicationUser>> LoadAll(CancellationToken cancellationToken);
        Task<ApplicationUser?> Load(Guid id, CancellationToken cancellationToken);
        Task<ApplicationUser?> Load(string email, CancellationToken cancellationToken);
        Task<ApplicationUser> Create(ApplicationUser user, UserRole role, string password, CancellationToken cancellationToken);
        Task<ApplicationUser> Update(ApplicationUser user, UserRole role, CancellationToken cancellationToken);
        Task Delete(ApplicationUser user, CancellationToken cancellationToken);
    }
}
