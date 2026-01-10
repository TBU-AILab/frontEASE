using AutoMapper;
using FrontEASE.Domain.Services.Shared.Resources;
using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Application.AppServices.Shared.Resources
{
    public class ResourceAppService(
        IResourceService resourceService,
        IMapper mapper) : IResourceAppService
    {
        public async Task<IList<ResourceDto>> LoadAll(LanguageCode language, CancellationToken cancellationToken)
        {
            var resources = await resourceService.LoadAll(language, cancellationToken);
            return mapper.Map<IList<ResourceDto>>(resources);
        }
    }
}
