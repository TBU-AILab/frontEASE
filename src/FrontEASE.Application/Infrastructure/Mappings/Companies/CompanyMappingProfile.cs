using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Application.Infrastructure.Mappings.Companies
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(x => x.Address, cd => cd.MapFrom(map => map.Address))
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image ?? new Image()))
                .ForMember(x => x.Users, cd => cd.MapFrom(map => map.Users))
                .ReverseMap();

            CreateMap<Company, Company>()
                .ForMember(x => x.Address, cd => cd.MapFrom(map => map.Address))
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image))
                .ForMember(x => x.Users, cd => cd.MapFrom(map => map.Users))

                .ForMember(x => x.DateUpdated, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())

                .ForMember(x => x.AddressID, opt => opt.Ignore())
                .ForMember(x => x.ImageID, opt => opt.Ignore())
                .ForMember(x => x.ID, opt => opt.Ignore());
        }
    }
}
