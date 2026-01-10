using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Management
{
    public class ManagementRepository(ApplicationDbContext context) : IManagementRepository
    {
        public async Task<UserPreferences?> Load(Guid id, CancellationToken cancellationToken)
        {
            var query = await context.Users
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
            context.UserPreferences.Update(preferences);
            await context.SaveChangesAsync(cancellationToken);
            return preferences;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await context.Database.BeginTransactionAsync(cancellationToken);
    }
}
