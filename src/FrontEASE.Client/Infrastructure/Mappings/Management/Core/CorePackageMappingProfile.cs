using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.Core
{
    public class CorePackageMappingProfile : Profile
    {
        public CorePackageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferenceCorePackageDto, GlobalPreferenceCorePackageDto>();
        }
    }
}
