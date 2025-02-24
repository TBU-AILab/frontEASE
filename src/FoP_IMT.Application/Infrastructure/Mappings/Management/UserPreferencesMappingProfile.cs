using AutoMapper;
using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Shared.Data.DTOs.Management;

namespace FoP_IMT.Application.Infrastructure.Mappings.Management
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
