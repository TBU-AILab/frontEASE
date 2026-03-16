using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.General.Columns;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.General.Columns
{
    public class UserPreferenceGeneralOptionTaskGridColumnMappingProfile : Profile
    {
        public UserPreferenceGeneralOptionTaskGridColumnMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceGeneralOptionTaskGridColumnDto, UserPreferenceGeneralOptionTaskGridColumnDto>();
        }
    }
}
