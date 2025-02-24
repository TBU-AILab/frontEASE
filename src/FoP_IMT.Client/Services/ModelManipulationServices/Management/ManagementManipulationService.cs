using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Management;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Management
{
    public class ManagementManipulationService : IManagementManipulationService
    {
        private readonly IMapper _mapper;

        public ManagementManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void SetItemPriorities(UserPreferencesDto preferences)
        {
            foreach (var item in preferences.TokenOptions.Select((value, i) => new { i, value }))
            { item.value.Priority = item.i; }
        }

        public void ReinitializeTokenModel(UserPreferenceTokenOptionDto token)
        {
            var cleanModel = new UserPreferenceTokenOptionDto();
            _mapper.Map(cleanModel, token);
        }
    }
}
