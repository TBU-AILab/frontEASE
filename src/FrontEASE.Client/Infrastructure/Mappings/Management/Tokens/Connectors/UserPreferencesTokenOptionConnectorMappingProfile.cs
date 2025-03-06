using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.Tokens.Connectors
{
    public class UserPreferencesTokenOptionConnectorMappingProfile : Profile
    {
        public UserPreferencesTokenOptionConnectorMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTokenOptionConnectorTypeDto, UserPreferenceTokenOptionConnectorTypeDto>();
        }
    }
}