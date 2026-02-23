using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Management
{
    public class ManagementRepository(ApplicationDbContext context) : IManagementRepository
    {
        public async Task<UserPreferenceTagOption?> LoadTag(string tag, CancellationToken cancellationToken)
        {
            var tagEntity = await context.UserPreferenceTagOptions
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.User)
                .Include(x => x.Tasks)
                .SingleOrDefaultAsync(x => EF.Functions.ILike(x.Tag, $"{tag}"), cancellationToken);

            return tagEntity;
        }

        public async Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken)
        {
            var tags = await context.UserPreferenceTagOptions
                .Include(x => x.Tasks)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return tags;
        }

        public async Task<UserPreferences?> Load(Guid id, CancellationToken cancellationToken)
        {
            var query = await context.Users
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.TokenOptions)
                        .ThenInclude(x => x.ConnectorTypes)
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.TagOptions)
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

        public async Task Delete(UserPreferenceTagOption tag, CancellationToken cancellationToken)
        {
            context.UserPreferenceTagOptions.Remove(tag);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await context.Database.BeginTransactionAsync(cancellationToken);
    }
}
