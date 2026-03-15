using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.General;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
using FrontEASE.Shared.Data.DTOs.Management.Tags;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;

namespace FrontEASE.Client.Services.ModelManipulationServices.Management
{
    public interface IManagementManipulationService
    {
        void SetItemConnectorTypes(UserPreferencesDto preferences);
        void SetItemPriorities(UserPreferencesDto preferences);
        void ReinitializeTokenModel(UserPreferenceTokenOptionDto token);
        void ReinitializeTagModel(UserPreferenceTagOptionDto tag);
        void ReinitializePackageModel(GlobalPreferenceCorePackageDto package);
        void SyncTaskGridColumn(UserPreferenceGeneralOptionsDto preferences, TaskGridColumnsVisibility column, bool isChecked);
        void EnsureTaskGridColumnsModel(UserPreferenceGeneralOptionsDto preferences);
    }
}
