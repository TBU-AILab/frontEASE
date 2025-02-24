using AutoMapper;
using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Shared.Data.DTOs.Shared.Images;

namespace FoP_IMT.Application.Infrastructure.Mappings.Shared.Images
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Image, ImageDto>()
                .ReverseMap();

            CreateMap<Image, Image>()
                .ForMember(x => x.ID, opt => opt.Ignore());
        }
    }
}
