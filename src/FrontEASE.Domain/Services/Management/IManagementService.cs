using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;

namespace FrontEASE.Domain.Services.Management
{
    public interface IManagementService
    {
        Task DeleteTag(UserPreferenceTagOption tag, CancellationToken cancellationToken);
        Task<UserPreferenceTagOption?> LoadTag(Guid id, bool supressException, CancellationToken cancellationToken);
        Task<UserPreferenceTagOption?> LoadTag(string tag, bool supressException, CancellationToken cancellationToken);
        Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken);
        Task<UserPreferences> LoadBase(Guid id, CancellationToken cancellationToken);
        Task<UserPreferences> LoadFull(Guid id, CancellationToken cancellationToken);
        Task<GlobalPreferences> LoadGlobal(CancellationToken cancellationToken);
        Task<UserPreferenceTagOption?> InsertTag(UserPreferenceTagOption tag, CancellationToken cancellationToken);
        Task<UserPreferences> Update(Guid id, UserPreferences preferences, CancellationToken cancellationToken);
        Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences, CancellationToken cancellationToken);
    }
}
