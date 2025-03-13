using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FrontEASE.Client.Services.ModelManipulationServices.Management
{
    public class ManagementManipulationService : IManagementManipulationService
    {
        private readonly IMapper _mapper;

        public ManagementManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

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
                    foreach (var newType in item.SelectedTokenConnectorTypes)
                    {
                        item.ConnectorTypes.Add(new UserPreferenceTokenOptionConnectorTypeDto { ConnectorType = newType });
                    }
                }
            }
        }

        public void ReinitializeTokenModel(UserPreferenceTokenOptionDto token)
        {
            var cleanModel = new UserPreferenceTokenOptionDto();
            _mapper.Map(cleanModel, token);
        }
    }
}