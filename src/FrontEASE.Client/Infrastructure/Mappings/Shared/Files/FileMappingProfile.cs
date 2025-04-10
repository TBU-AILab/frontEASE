using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Files;

namespace FrontEASE.Client.Infrastructure.Mappings.Shared.Files
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<FileDto, FileDto>()
                .ReverseMap();
        }
    }
}
