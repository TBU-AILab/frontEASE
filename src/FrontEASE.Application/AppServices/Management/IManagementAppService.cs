using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Tags;

namespace FrontEASE.Application.AppServices.Management
{
    public interface IManagementAppService
    {
        Task<UserPreferencesDto> Load(CancellationToken cancellationToken);
        Task<IList<UserPreferenceTagOptionDto>> LoadTags(CancellationToken cancellationToken);
        Task<GlobalPreferencesDto> LoadGlobal(CancellationToken cancellationToken);
        Task<UserPreferencesDto> Update(UserPreferencesDto preferences, CancellationToken cancellationToken);
        Task<GlobalPreferencesDto> UpdateGlobal(GlobalPreferencesDto globalPreferences, CancellationToken cancellationToken);
    }
}
