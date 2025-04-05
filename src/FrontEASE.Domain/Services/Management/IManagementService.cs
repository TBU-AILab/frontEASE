using FrontEASE.Domain.Entities.Management;

namespace FrontEASE.Domain.Services.Management
{
    public interface IManagementService
    {
        Task<UserPreferences> Load(Guid id);
        Task<GlobalPreferences> LoadGlobal();
        Task<UserPreferences> Update(Guid id, UserPreferences preferences);
        Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences);
    }
}
