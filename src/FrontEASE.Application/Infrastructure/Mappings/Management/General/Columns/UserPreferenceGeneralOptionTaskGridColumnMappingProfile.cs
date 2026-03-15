using AutoMapper;
using FrontEASE.Domain.Entities.Management.General.Columns;
using FrontEASE.Shared.Data.DTOs.Management.General.Columns;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.General.Columns
{
    public class UserPreferenceGeneralOptionTaskGridColumnMappingProfile : Profile
    {
        public UserPreferenceGeneralOptionTaskGridColumnMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceGeneralOptionTaskGridColumn, UserPreferenceGeneralOptionTaskGridColumnDto>()
                .ReverseMap();

            CreateMap<UserPreferenceGeneralOptionTaskGridColumn, UserPreferenceGeneralOptionTaskGridColumn>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.GeneralOptionsID, opt => opt.Ignore())
                .ForMember(x => x.GeneralOptions, opt => opt.Ignore());
        }
    }
}