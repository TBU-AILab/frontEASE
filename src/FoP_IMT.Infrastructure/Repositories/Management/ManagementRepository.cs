using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Repositories.Management;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoP_IMT.Infrastructure.Repositories.Management
{
    public class ManagementRepository : IManagementRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPreferences?> Load(Guid id)
        {
            var query = await _context.Users
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.TokenOptions)
                        .ThenInclude(x => x.ConnectorTypes)
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.GeneralOptions)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.Id == id.ToString());

            return query?.UserPreferences;
        }

        public async Task<UserPreferences> Update(UserPreferences preferences)
        {
            _context.UserPreferences.Update(preferences);
            await _context.SaveChangesAsync();
            return preferences;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
