using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Management
{
    public class ManagementRepository : IManagementRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPreferences?> Load(Guid id, CancellationToken cancellationToken)
        {
            var query = await _context.Users
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.TokenOptions)
                        .ThenInclude(x => x.ConnectorTypes)
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.GeneralOptions)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken);

            return query?.UserPreferences;
        }

        public async Task<UserPreferences> Update(UserPreferences preferences, CancellationToken cancellationToken)
        {
            _context.UserPreferences.Update(preferences);
            await _context.SaveChangesAsync(cancellationToken);
            return preferences;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
