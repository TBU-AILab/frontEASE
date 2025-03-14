using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Shared.Addresses;

namespace FrontEASE.Client.Infrastructure.Mappings.Shared.Addresses
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
