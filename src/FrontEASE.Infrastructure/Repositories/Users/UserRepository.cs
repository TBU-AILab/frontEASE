using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ApplicationUser>> LoadWhere(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .Where(predicate)
                .ToListAsync(cancellationToken) ?? [];

            return users;
        }

        public async Task<IList<ApplicationUser>> LoadAll(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .ToListAsync(cancellationToken) ?? [];

            return users;
        }

        public async Task<ApplicationUser?> Load(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .SingleOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken);

            return user;
        }

        public async Task<ApplicationUser> Insert(ApplicationUser user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task Delete(ApplicationUser user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await _context.Database.BeginTransactionAsync(cancellationToken);

    }
}
