using AutoMapper;
using FrontEASE.Domain.Entities.Management.Tokens.Connectors;
using FrontEASE.Shared.Data.DTOs.Management.Tokens.Connectors;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.Tokens.Connectors
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