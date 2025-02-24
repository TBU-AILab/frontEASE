using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;

namespace FoP_IMT.Client.Infrastructure.Mappings.Management.Tokens
{
    public class UserPreferenceTokenOptionMappingProfile : Profile
    {
        public UserPreferenceTokenOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTokenOptionDto, UserPreferenceTokenOptionDto>();
        }
    }
}
