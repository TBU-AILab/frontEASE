using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Application.Infrastructure.Mappings.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image))
                .ForMember(x => x.UserPreferences, cd => cd.MapFrom(map => map.UserPreferences))

                .ForMember(x => x.Companies, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ApplicationUser, ApplicationUser>()
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image ?? new Image()))
                .ForMember(x => x.Companies, cd => cd.MapFrom(map => map.Companies))
                .ForMember(x => x.UserPreferences, cd => cd.MapFrom(map => map.UserPreferences))

                .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
                .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
                .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
                .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
                .ForMember(x => x.LockoutEnd, opt => opt.Ignore())
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())

                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ImageID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferencesID, opt => opt.Ignore())
                .ForMember(x => x.UserRole, opt => opt.Ignore());
        }
    }
}
