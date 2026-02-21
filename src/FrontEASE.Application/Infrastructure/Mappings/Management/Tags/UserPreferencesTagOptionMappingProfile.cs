using AutoMapper;
using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Shared.Data.DTOs.Management.Tags;

namespace FrontEASE.Application.Infrastructure.Mappings.Management.Tags
{
    public class UserPreferencesTagOptionMappingProfile : Profile
    {
        public UserPreferencesTagOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTagOption, UserPreferenceTagOptionDto>()
                .ReverseMap();

            CreateMap<UserPreferenceTagOption, UserPreferenceTagOption>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferencesID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferences, opt => opt.Ignore());
        }
    }
}
