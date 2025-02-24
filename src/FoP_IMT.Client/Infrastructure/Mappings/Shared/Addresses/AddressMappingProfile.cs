using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Shared.Addresses;

namespace FoP_IMT.Client.Infrastructure.Mappings.Shared.Addresses
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<AddressDto, AddressDto>()
                .ReverseMap();
        }
    }
}
