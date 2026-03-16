using AutoMapper;
using FrontEASE.Domain.Entities.Management.General;
using FrontEASE.Shared.Data.DTOs.Management.General;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.General
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
                .ForMember(x => x.TaskGridColumns, cd => cd.MapFrom(map => map.TaskGridColumns))
                .ReverseMap();

            CreateMap<UserPreferenceGeneralOptions, UserPreferenceGeneralOptions>()
                .ForMember(x => x.TaskGridColumns, cd => cd.MapFrom(map => map.TaskGridColumns))

                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferences, opt => opt.Ignore());
        }
    }
}
