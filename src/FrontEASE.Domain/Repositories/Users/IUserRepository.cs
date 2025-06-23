using FrontEASE.Domain.Entities.Shared.Users;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Users
{
    public interface IUserRepository : IRepository
    {
        Task<IList<ApplicationUser>> LoadAll(CancellationToken cancellationToken);
        Task<ApplicationUser?> Load(Guid id, CancellationToken cancellationToken);
        Task<IList<ApplicationUser>> LoadWhere(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken);
        Task<ApplicationUser> Insert(ApplicationUser user, CancellationToken cancellationToken);
        Task<ApplicationUser> Update(ApplicationUser user, CancellationToken cancellationToken);
        Task Delete(ApplicationUser user, CancellationToken cancellationToken);
    }
}
