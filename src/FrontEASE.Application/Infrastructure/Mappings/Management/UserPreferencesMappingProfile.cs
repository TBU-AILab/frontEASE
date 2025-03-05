using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Application.Infrastructure.Mappings.Management
{
    public class UserPreferencesMappingProfile : Profile
    {
        public UserPreferencesMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferences, UserPreferencesDto>()
                .ForMember(x => x.TokenOptions, cd => cd.MapFrom(map => map.TokenOptions))
                .ForMember(x => x.GeneralOptions, cd => cd.MapFrom(map => map.GeneralOptions))
                .ReverseMap();

            CreateMap<UserPreferences, UserPreferences>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.GeneralOptionsID, opt => opt.Ignore())

                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.GeneralOptions, cd => cd.MapFrom(map => map.GeneralOptions))
                .ForMember(x => x.TokenOptions, cd => cd.MapFrom(map => map.TokenOptions));
        }
    }
}
