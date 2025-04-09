using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Application.Infrastructure.Mappings.Management
{
    public class GlobalPreferencesMappingProfile : Profile
    {
        public GlobalPreferencesMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferences, GlobalPreferencesDto>()
                .ForMember(x => x.CorePackages, cd => cd.MapFrom(map => map.CorePackages))
                .ReverseMap();

            CreateMap<GlobalPreferences, GlobalPreferences>()
                .ForMember(x => x.CorePackages, cd => cd.MapFrom(map => map.CorePackages));
        }
    }
}
