using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;

namespace FoP_IMT.Client.Infrastructure.Mappings.Shared.Images
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<ApplicationUserDto, ApplicationUserDto>()
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image))
                .ForMember(x => x.Companies, cd => cd.MapFrom(map => map.Companies))
                .ForMember(x => x.UserPreferences, cd => cd.MapFrom(map => map.UserPreferences))
                .ReverseMap();
        }
    }
}
