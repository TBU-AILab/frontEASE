using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Application.AppServices.Management
{
    public interface IManagementAppService
    {
        Task<UserPreferencesDto> Load();
        Task<GlobalPreferencesDto> LoadGlobal();
        Task<UserPreferencesDto> Update(UserPreferencesDto preferences);
        Task<GlobalPreferencesDto> UpdateGlobal(GlobalPreferencesDto globalPreferences);
    }
}
