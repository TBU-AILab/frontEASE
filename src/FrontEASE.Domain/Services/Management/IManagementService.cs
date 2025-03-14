using FrontEASE.Domain.Entities.Management;

namespace FrontEASE.Domain.Services.Management
{
    public interface IManagementService
    {
        Task<UserPreferences> Load(Guid id);
        Task<UserPreferences> Update(Guid id, UserPreferences preferences);
    }
}
