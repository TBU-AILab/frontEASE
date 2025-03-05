using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Client.Infrastructure.Mappings.Management
{
    public class UserPreferencesMappingProfile : Profile
    {
        public UserPreferencesMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferencesDto, UserPreferencesDto>()
                .ForMember(x => x.TokenOptions, cd => cd.MapFrom(map => map.TokenOptions))
                .ForMember(x => x.GeneralOptions, cd => cd.MapFrom(map => map.GeneralOptions));
        }
    }
}
