using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Users
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<IList<ApplicationUser>> LoadWhere(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken)
        {
            var users = await context.Users
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .Where(predicate)
                .ToListAsync(cancellationToken) ?? [];

            return users;
        }

        public async Task<IList<ApplicationUser>> LoadAll(CancellationToken cancellationToken)
        {
            var users = await context.Users
                .AsNoTracking()
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .ToListAsync(cancellationToken) ?? [];

            return users;
        }

        public async Task<ApplicationUser?> Load(Guid id, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .SingleOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken);

            return user;
        }

        public async Task<ApplicationUser> Insert(ApplicationUser user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user, CancellationToken cancellationToken)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task Delete(ApplicationUser user, CancellationToken cancellationToken)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await context.Database.BeginTransactionAsync(cancellationToken);

    }
}
