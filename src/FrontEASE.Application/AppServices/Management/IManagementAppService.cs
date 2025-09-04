using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Application.AppServices.Management
{
    public interface IManagementAppService
    {
        Task<UserPreferencesDto> Load(CancellationToken cancellationToken);
        Task<GlobalPreferencesDto> LoadGlobal(CancellationToken cancellationToken);
        Task<UserPreferencesDto> Update(UserPreferencesDto preferences, CancellationToken cancellationToken);
        Task<GlobalPreferencesDto> UpdateGlobal(GlobalPreferencesDto globalPreferences, CancellationToken cancellationToken);
    }
}
