using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.Tags;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.Tags
{
    public class UserPreferenceTagOptionMappingProfile : Profile
    {
        public UserPreferenceTagOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTagOptionDto, UserPreferenceTagOptionDto>();
        }
    }
}
