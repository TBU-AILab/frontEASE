using FrontEASE.Domain.Entities.Management;

namespace FrontEASE.Domain.Repositories.Management
{
    public interface IManagementRepository : IRepository
    {
        Task<UserPreferences?> Load(Guid id, CancellationToken cancellationToken);
        Task<UserPreferences> Update(UserPreferences preferences, CancellationToken cancellationToken);
    }
}
