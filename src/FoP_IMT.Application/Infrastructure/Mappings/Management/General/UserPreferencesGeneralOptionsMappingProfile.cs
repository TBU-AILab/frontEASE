using AutoMapper;
using FoP_IMT.Domain.Entities.Management.General;
using FoP_IMT.Shared.Data.DTOs.Management.General;

namespace FoP_IMT.Application.Infrastructure.Mappings.Management.General
{
    public class UserPreferencesGeneralOptionsMappingProfile : Profile
    {
        public UserPreferencesGeneralOptionsMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceGeneralOptions, UserPreferenceGeneralOptionsDto>()
                .ReverseMap();

            CreateMap<UserPreferenceGeneralOptions, UserPreferenceGeneralOptions>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferences, opt => opt.Ignore());
        }
    }
}
