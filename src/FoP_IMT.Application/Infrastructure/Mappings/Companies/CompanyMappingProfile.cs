using AutoMapper;
using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Shared.Data.DTOs.Companies;

namespace FoP_IMT.Application.Infrastructure.Mappings.Companies
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
