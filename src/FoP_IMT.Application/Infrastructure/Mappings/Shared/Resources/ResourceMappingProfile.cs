using AutoMapper;
using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Shared.Data.DTOs.Shared.Resources;

namespace FoP_IMT.Application.Infrastructure.Mappings.Shared.Resources
{
    public class ResourceMappingProfile : Profile
    {
        public ResourceMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Resource, ResourceDto>()
                .ReverseMap();
        }
    }
}
