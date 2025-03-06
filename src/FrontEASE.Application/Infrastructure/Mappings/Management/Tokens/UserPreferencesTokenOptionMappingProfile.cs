using AutoMapper;
using FrontEASE.Domain.Entities.Management.Tokens;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.Tokens
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
