using FrontEASE.Domain.DataQueries.Management;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Management
{
    public interface IManagementRepository : IRepository
    {
        Task<UserPreferenceTagOption?> InsertTag(UserPreferenceTagOption tag, CancellationToken cancellationToken);
        Task<UserPreferenceTagOption?> LoadTag(Guid id, CancellationToken cancellationToken);
        Task<UserPreferenceTagOption?> LoadTagWhere(Expression<Func<UserPreferenceTagOption, bool>> predicate, CancellationToken cancellationToken);
        Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken);
        Task<IList<UserPreferenceTagOption>> LoadTagsWhere(Expression<Func<UserPreferenceTagOption, bool>> predicate, CancellationToken cancellationToken);
        Task<UserPreferences?> Load(Guid id, UserPreferencesQuery query, CancellationToken cancellationToken);
        Task<UserPreferences> Update(UserPreferences preferences, CancellationToken cancellationToken);
        Task Delete(UserPreferenceTagOption tag, CancellationToken cancellationToken);
    }
}
