using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Images;

namespace FrontEASE.Client.Infrastructure.Mappings.Shared.Images
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<ImageDto, ImageDto>()
                .ReverseMap();
        }
    }
}
