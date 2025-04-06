using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Client.Services.ApiServices.Management
{
    public interface IManagementApiService
    {
        Task<UserPreferencesDto?> LoadPreferences();
        Task<GlobalPreferencesDto?> LoadGlobalPreferences();
        Task<UserPreferencesDto?> UpdatePreferences(UserPreferencesDto editedPreferencesDto);
        Task<GlobalPreferencesDto?> UpdateGlobalPreferences(GlobalPreferencesDto editedGlobalPreferencesDto);
    }
}
