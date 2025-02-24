using FoP_IMT.Shared.Data.DTOs.Management;

namespace FoP_IMT.Application.AppServices.Management
{
    public interface IManagementAppService
    {
        Task<UserPreferencesDto> Load();
        Task<UserPreferencesDto> Update(UserPreferencesDto preferences);
    }
}
