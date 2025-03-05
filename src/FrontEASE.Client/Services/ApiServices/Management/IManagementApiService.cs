using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Client.Services.ApiServices.Management
{
    public interface IManagementApiService
    {
        Task<UserPreferencesDto?> LoadPreferences();
        Task<UserPreferencesDto?> UpdatePreferences(UserPreferencesDto editedPreferencesDto);
    }
}
