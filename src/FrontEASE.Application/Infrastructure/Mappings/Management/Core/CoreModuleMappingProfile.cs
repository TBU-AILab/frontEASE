using AutoMapper;
using FrontEASE.Domain.Entities.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.Core
{
    public class CoreModuleMappingProfile : Profile
    {
        public CoreModuleMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferenceCoreModuleDto, GlobalPreferenceCoreModule>()
                .ForMember(x => x.Files, cd => cd.MapFrom(map => map.Files))
                .ReverseMap();

            CreateMap<GlobalPreferenceCoreModule, GlobalPreferenceCoreModule>()
                .ForMember(x => x.Files, cd => cd.MapFrom(map => map.Files));
        }
    }
}
