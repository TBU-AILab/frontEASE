using AutoMapper;
using FoP_IMT.Domain.Entities.Management.Tokens.Connectors;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FoP_IMT.Application.Infrastructure.Mappings.Management.Tokens.Connectors
{
    public class UserPreferencesTokenOptionConnectorMappingProfile : Profile
    {
        public UserPreferencesTokenOptionConnectorMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTokenOptionConnectorType, UserPreferenceTokenOptionConnectorTypeDto>()
                .ReverseMap();

            CreateMap<UserPreferenceTokenOptionConnectorType, UserPreferenceTokenOptionConnectorType>()
                .ForMember(x => x.ID, opt => opt.Ignore())

                .ForMember(x => x.TokenOption, opt => opt.Ignore());
        }
    }
}
