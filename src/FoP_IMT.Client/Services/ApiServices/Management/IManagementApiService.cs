using FoP_IMT.Shared.Data.DTOs.Management;

namespace FoP_IMT.Client.Services.ApiServices.Management
{
    public interface IManagementApiService
    {
        Task<UserPreferencesDto?> LoadPreferences();
        Task<UserPreferencesDto?> UpdatePreferences(UserPreferencesDto editedPreferencesDto);
    }
}
