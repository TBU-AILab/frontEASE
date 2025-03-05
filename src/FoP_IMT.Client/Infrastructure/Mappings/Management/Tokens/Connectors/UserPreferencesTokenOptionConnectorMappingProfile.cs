using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FoP_IMT.Client.Infrastructure.Mappings.Management.Tokens.Connectors
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
