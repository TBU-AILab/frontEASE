﻿using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Domain.Repositories.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Domain.Services.Shared.Resources
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IList<Resource>> LoadAll(LanguageCode language, CancellationToken cancellationToken)
        {
            var resources = await _resourceRepository.LoadAll(language, cancellationToken);
            return resources ?? [];
        }

        public async Task<Resource> Load(LanguageCode language, string resourceCode, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.Load(language, resourceCode, cancellationToken);
            return resource ?? new Resource() { ResourceCode = resourceCode, Value = "N/A" };
        }
    }
}
