using AutoMapper;
using FoP_IMT.Domain.Services.Shared.Resources;
using FoP_IMT.Shared.Data.DTOs.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Application.AppServices.Shared.Resources
{
    public class ResourceAppService : IResourceAppService
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourceAppService(
            IResourceService resourceService,
            IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
        }

        public async Task<IList<ResourceDto>> LoadAll(LanguageCode language)
        {
            var resources = await _resourceService.LoadAll(language);
            return _mapper.Map<IList<ResourceDto>>(resources);
        }
    }
}
