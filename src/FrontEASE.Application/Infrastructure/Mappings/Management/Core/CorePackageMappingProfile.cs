using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Packages;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.Core
{
    public class CorePackageMappingProfile : Profile
    {
        public CorePackageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<GlobalPreferenceCorePackageDto, GlobalPreferenceCorePackage>().ReverseMap();

            CreateMap<GlobalPreferenceCorePackage, CorePackageCoreDto>().ReverseMap();
        }
    }
}
