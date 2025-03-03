using AutoMapper;
using FoP_IMT.Domain.Entities.Management.Tokens;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;

namespace FoP_IMT.Application.Infrastructure.Mappings.Management.Tokens
{
    public class UserPreferencesTokenOptionMappingProfile : Profile
    {
        public UserPreferencesTokenOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTokenOption, UserPreferenceTokenOptionDto>()
                .ForMember(x => x.ConnectorTypes, cd => cd.MapFrom(map => map.ConnectorTypes))
                .ReverseMap();

            CreateMap<UserPreferenceTokenOption, UserPreferenceTokenOption>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferencesID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferences, opt => opt.Ignore())

                .ForMember(x => x.DateCreated, opt => opt.Ignore())

                .ForMember(x => x.ConnectorTypes, cd => cd.MapFrom(map => map.ConnectorTypes));
        }
    }
}
