using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;

namespace FrontEASE.Domain.Repositories.Management
{
    public interface IManagementRepository : IRepository
    {
        Task<UserPreferenceTagOption?> LoadTag(string id, CancellationToken cancellationToken);
        Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken);
        Task<UserPreferences?> Load(Guid id, CancellationToken cancellationToken);
        Task<UserPreferences> Update(UserPreferences preferences, CancellationToken cancellationToken);
        Task Delete(UserPreferenceTagOption tag, CancellationToken cancellationToken);
    }
}
