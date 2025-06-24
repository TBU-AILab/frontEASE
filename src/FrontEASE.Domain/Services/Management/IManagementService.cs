using FrontEASE.Domain.Entities.Management;

namespace FrontEASE.Domain.Services.Management
{
    public interface IManagementService
    {
        Task<UserPreferences> Load(Guid id, CancellationToken cancellationToken);
        Task<GlobalPreferences> LoadGlobal(CancellationToken cancellationToken);
        Task<UserPreferences> Update(Guid id, UserPreferences preferences, CancellationToken cancellationToken);
        Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences, CancellationToken cancellationToken);
    }
}
