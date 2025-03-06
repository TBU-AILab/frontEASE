using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Shared.Data.DTOs.Shared.Resources;

namespace FrontEASE.Application.Infrastructure.Mappings.Shared.Resources
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
