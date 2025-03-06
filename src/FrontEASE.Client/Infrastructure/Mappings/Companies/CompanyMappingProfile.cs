using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Client.Infrastructure.Mappings.Companies
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<CompanyDto, CompanyDto>()
                .ForMember(x => x.Image, cd => cd.MapFrom(map => map.Image))
                .ForMember(x => x.Address, cd => cd.MapFrom(map => map.Address))
                .ForMember(x => x.Users, cd => cd.MapFrom(map => map.Users))
                .ReverseMap();
        }
    }
}
