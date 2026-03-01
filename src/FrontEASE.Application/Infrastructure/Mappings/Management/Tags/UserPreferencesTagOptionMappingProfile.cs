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
                .ForMember(x => x.TaskCount, opt => opt.MapFrom(x => x.Tasks.Count));

            CreateMap<UserPreferenceTagOptionDto, UserPreferenceTagOption>()
                .ForMember(x => x.Tasks, opt => opt.Ignore());

            CreateMap<UserPreferenceTagOption, UserPreferenceTagOption>()
                .ForMember(x => x.ID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferencesID, opt => opt.Ignore())
                .ForMember(x => x.UserPreferences, opt => opt.Ignore());
        }
    }
}
