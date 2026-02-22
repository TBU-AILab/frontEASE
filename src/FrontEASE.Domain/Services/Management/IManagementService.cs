using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;

namespace FrontEASE.Domain.Services.Management
{
    public interface IManagementService
    {
        Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken);
        Task<UserPreferences> Load(Guid id, CancellationToken cancellationToken);
        Task<GlobalPreferences> LoadGlobal(CancellationToken cancellationToken);
        Task<UserPreferences> Update(Guid id, UserPreferences preferences, CancellationToken cancellationToken);
        Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences, CancellationToken cancellationToken);
    }
}
