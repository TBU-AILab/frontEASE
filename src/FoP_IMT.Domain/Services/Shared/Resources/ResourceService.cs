using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Domain.Repositories.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Domain.Services.Shared.Resources
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IList<Resource>> LoadAll(LanguageCode language)
        {
            var resources = await _resourceRepository.LoadAll(language);
            return resources ?? [];
        }

        public async Task<Resource> Load(LanguageCode language, string resourceCode)
        {
            var resource = await _resourceRepository.Load(language, resourceCode);
            return resource ?? new Resource() { ResourceCode = resourceCode, Value = "N/A" };
        }
    }
}
