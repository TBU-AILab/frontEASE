using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Repositories.Users;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FoP_IMT.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ApplicationUser>> LoadWhere(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var users = await _context.Users
                .Include(x => x.UserRole)
                .Where(predicate)
                .ToListAsync() ?? [];

            return users;
        }

        public async Task<IList<ApplicationUser>> LoadAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .ToListAsync() ?? [];

            return users;
        }

        public async Task<ApplicationUser?> Load(Guid id)
        {
            var user = await _context.Users
                .Include(x => x.UserRole)
                .Include(x => x.Image)
                .SingleOrDefaultAsync(x => x.Id == id.ToString());

            return user;
        }

        public async Task<ApplicationUser> Insert(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(ApplicationUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);

    }
}
