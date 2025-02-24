using FoP_IMT.Shared.Data.DTOs.Management;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Management
{
    public interface IManagementManipulationService
    {
        void SetItemPriorities(UserPreferencesDto preferences);
        void ReinitializeTokenModel(UserPreferenceTokenOptionDto token);
    }
}
