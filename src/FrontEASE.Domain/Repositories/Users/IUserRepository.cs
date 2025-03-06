using FrontEASE.Domain.Entities.Shared.Users;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Users
{
    public interface IUserRepository : IRepository
    {
        Task<IList<ApplicationUser>> LoadAll();
        Task<ApplicationUser?> Load(Guid id);
        Task<IList<ApplicationUser>> LoadWhere(Expression<Func<ApplicationUser, bool>> predicate);
        Task<ApplicationUser> Insert(ApplicationUser user);
        Task<ApplicationUser> Update(ApplicationUser user);
        Task Delete(ApplicationUser user);
    }
}
