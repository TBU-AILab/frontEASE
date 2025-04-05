using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management;

namespace FrontEASE.Client.Infrastructure.Mappings.Management
{
    public class GlobalPreferencesMappingProfile : Profile
    {
        public GlobalPreferencesMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferencesDto, GlobalPreferencesDto>()
                .ForMember(x => x.CorePackages, cd => cd.MapFrom(map => map.CorePackages));
        }
    }
}
