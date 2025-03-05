using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;

namespace FrontEASE.Client.Services.ModelManipulationServices.Management
{
    public interface IManagementManipulationService
    {
        void SetItemPriorities(UserPreferencesDto preferences);
        void SortTokenConnectorModels(UserPreferencesDto preferences);
        void ReinitializeTokenModel(UserPreferenceTokenOptionDto token);
    }
}
