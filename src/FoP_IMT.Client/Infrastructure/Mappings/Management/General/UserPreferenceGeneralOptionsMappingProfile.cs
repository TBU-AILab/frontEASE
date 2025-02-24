using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Management.General;

namespace FoP_IMT.Client.Infrastructure.Mappings.Management.General
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
