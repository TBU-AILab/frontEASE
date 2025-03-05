using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.General;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.General
{
    public class UserPreferenceGeneralOptionsMappingProfile : Profile
    {
        public UserPreferenceGeneralOptionsMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceGeneralOptionsDto, UserPreferenceGeneralOptionsDto>();
        }
    }
}
