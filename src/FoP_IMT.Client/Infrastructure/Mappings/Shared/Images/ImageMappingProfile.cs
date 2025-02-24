using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Shared.Images;

namespace FoP_IMT.Client.Infrastructure.Mappings.Shared.Images
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
