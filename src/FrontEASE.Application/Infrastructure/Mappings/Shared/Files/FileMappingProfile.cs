using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Files;

namespace FrontEASE.Application.Infrastructure.Mappings.Shared.Files
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Domain.Entities.Shared.Files.File, FileDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Shared.Files.File, Domain.Entities.Shared.Files.File>();
        }
    }
}
