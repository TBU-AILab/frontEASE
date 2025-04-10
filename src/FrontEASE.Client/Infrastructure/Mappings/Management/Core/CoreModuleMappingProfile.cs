using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.Core
{
    public class CoreModuleMappingProfile : Profile
    {
        public CoreModuleMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferenceCoreModuleDto, GlobalPreferenceCoreModuleDto>()
                .ForMember(x => x.Files, cd => cd.MapFrom(map => map.Files));
        }
    }
}
