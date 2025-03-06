using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Images;

namespace FrontEASE.Application.Infrastructure.Mappings.Shared.Images
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
