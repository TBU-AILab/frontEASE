using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Application.AppServices.Management
{
    public interface IManagementAppService
    {
        Task<UserPreferencesDto> Load();
        Task<UserPreferencesDto> Update(UserPreferencesDto preferences);
    }
}
