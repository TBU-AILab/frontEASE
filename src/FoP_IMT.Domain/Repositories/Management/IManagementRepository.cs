using FoP_IMT.Domain.Entities.Management;

namespace FoP_IMT.Domain.Repositories.Management
{
    public interface IManagementRepository : IRepository
    {
        Task<UserPreferences?> Load(Guid id);
        Task<UserPreferences> Update(UserPreferences preferences);
    }
}
