using FrontEASE.Domain.DataQueries.Management;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Management
{
    public class ManagementRepository(ApplicationDbContext context) : IManagementRepository
    {
        private IQueryable<UserPreferences> ComposeQuery(UserPreferencesQuery query)
        {
            var preferencesQuery = context.UserPreferences.AsQueryable();

            if (query.LoadTokenOptions)
            {
                preferencesQuery = query.LoadConnectorTypes
                    ? preferencesQuery.Include(x => x.TokenOptions).ThenInclude(x => x.ConnectorTypes)
                    : preferencesQuery.Include(x => x.TokenOptions);
            }
            if (query.LoadTagOptions) { preferencesQuery = preferencesQuery.Include(x => x.TagOptions); }
            if (query.LoadGeneralOptions) { preferencesQuery = preferencesQuery.Include(x => x.GeneralOptions); }

            if (query.WithNoTracking) { preferencesQuery = preferencesQuery.AsNoTracking(); }
            if (query.AsSplitQuery) { preferencesQuery = preferencesQuery.AsSplitQuery(); }

            return preferencesQuery;
        }

        public async Task<UserPreferenceTagOption?> LoadTagWhere(Expression<Func<UserPreferenceTagOption, bool>> predicate, CancellationToken cancellationToken)
        {
            var tag = await context.UserPreferenceTagOptions
                .Where(predicate)
                .SingleOrDefaultAsync(cancellationToken);

            return tag;
        }

        public async Task<IList<UserPreferenceTagOption>> LoadTagsWhere(Expression<Func<UserPreferenceTagOption, bool>> predicate, CancellationToken cancellationToken)
        {
            var tags = await context.UserPreferenceTagOptions
                .Where(predicate)
                .ToListAsync(cancellationToken) ?? [];

            return tags;
        }

        public async Task<UserPreferenceTagOption?> LoadTag(Guid id, CancellationToken cancellationToken)
        {
            var tagEntity = await context.UserPreferenceTagOptions
                .Include(x => x.UserPreferences)
                    .ThenInclude(x => x.User)
                .Include(x => x.Tasks.Where(x => !x.IsDeleted))
                .SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

            return tagEntity;
        }

        public async Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken)
        {
            var tags = await context.UserPreferenceTagOptions
                .Include(x => x.Tasks.Where(x => !x.IsDeleted))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return tags;
        }

        public async Task<UserPreferences?> Load(Guid id, UserPreferencesQuery query, CancellationToken cancellationToken)
        {
            var preferencesQuery = ComposeQuery(query);
            var preferences = await preferencesQuery.SingleOrDefaultAsync(x => x.User.Id == id.ToString(), cancellationToken);
            return preferences;
        }

        public async Task<UserPreferenceTagOption?> InsertTag(UserPreferenceTagOption tag, CancellationToken cancellationToken)
        {
            await context.UserPreferenceTagOptions.AddAsync(tag, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return tag;
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
