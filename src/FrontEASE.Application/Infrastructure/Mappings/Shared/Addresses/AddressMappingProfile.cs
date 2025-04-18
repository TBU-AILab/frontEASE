﻿using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Addresses;
using FrontEASE.Shared.Data.DTOs.Shared.Addresses;

namespace FrontEASE.Application.Infrastructure.Mappings.Shared.Addresses
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Address, AddressDto>()
                .ReverseMap();

            CreateMap<Address, Address>()
                .ForMember(x => x.ID, opt => opt.Ignore())

                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ForMember(x => x.DateUpdated, opt => opt.Ignore());
        }
    }
}
