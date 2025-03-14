using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;

namespace FrontEASE.Client.Infrastructure.Mappings.Management.Tokens
{
    public class UserPreferenceTokenOptionMappingProfile : Profile
    {
        public UserPreferenceTokenOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<UserPreferenceTokenOptionDto, UserPreferenceTokenOptionDto>()
                .ForMember(x => x.ConnectorTypes, cd => cd.MapFrom(map => map.ConnectorTypes));
        }
    }
}
