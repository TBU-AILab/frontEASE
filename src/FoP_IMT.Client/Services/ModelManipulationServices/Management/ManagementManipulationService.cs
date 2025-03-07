﻿using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Management;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Management
{
    public class ManagementManipulationService : IManagementManipulationService
    {
        private readonly IMapper _mapper;

        public ManagementManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void SortTokenConnectorModels(UserPreferencesDto preferences)
        {
            foreach (var token in preferences.TokenOptions)
            {
                var connectorTypes = token.SelectedTokenConnectorTypes.Select(x => { return new UserPreferenceTokenOptionConnectorTypeDto() { ConnectorType = x }; });
                token.ConnectorTypes.Clear();
                
                foreach(var addedToken in connectorTypes)
                {
                    token.ConnectorTypes.Add(addedToken);
                }
            }
        }

        public void SetItemPriorities(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions.Select((value, i) => new { i, value }))
            { item.value.Priority = item.i; }
        }

        public void ReinitializeTokenModel(UserPreferenceTokenOptionDto token)
        {
            var cleanModel = new UserPreferenceTokenOptionDto();
            _mapper.Map(cleanModel, token);
        }
    }
}
