using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
using FrontEASE.Shared.Data.DTOs.Management.Tags;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;

namespace FrontEASE.Client.Services.ModelManipulationServices.Management
{
    public class ManagementManipulationService(IMapper mapper) : IManagementManipulationService
    {
        public void SetItemPriorities(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions.Select((value, i) => new { i, value }))
            { item.value.Priority = item.i; }
        }

        public void SetItemConnectorTypes(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions)
            {
                if (item.SelectedTokenConnectorTypes?.Count > 0)
                {
                    item.ConnectorTypes.Clear();
                    item.ConnectorTypes.AddRange(item.SelectedTokenConnectorTypes.Select(x => new UserPreferenceTokenOptionConnectorTypeDto { ConnectorType = x }));
                }
            }
        }

        public void ReinitializeTagModel(UserPreferenceTagOptionDto tag)
        {
            var cleanModel = new UserPreferenceTagOptionDto();
            mapper.Map(cleanModel, tag);
        }

        public void ReinitializeTokenModel(UserPreferenceTokenOptionDto token)
        {
            var cleanModel = new UserPreferenceTokenOptionDto();
            mapper.Map(cleanModel, token);
        }

        public void ReinitializePackageModel(GlobalPreferenceCorePackageDto package)
        {
            var cleanModel = new GlobalPreferenceCorePackageDto();
            mapper.Map(cleanModel, package);
        }
    }
}