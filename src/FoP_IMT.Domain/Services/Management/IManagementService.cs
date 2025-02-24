using FoP_IMT.Domain.Entities.Management;

namespace FoP_IMT.Domain.Services.Management
{
    public interface IManagementService
    {
        Task<UserPreferences> Load(Guid id);
        Task<UserPreferences> Update(Guid id, UserPreferences preferences);
    }
}
