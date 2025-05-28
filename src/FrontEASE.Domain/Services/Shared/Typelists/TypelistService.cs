using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FrontEASE.Domain.Services.Shared.Typelists
{
    public class TypelistService : ITypelistService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICoreConnector _coreService;

        public TypelistService(IMapper mapper, IMemoryCache memoryCache, ICoreConnector coreService)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _coreService = coreService;
        }

        public async Task<IList<TaskModule>> LoadModuleTypes(bool withCacheRefresh)
        {
            var values = (IList<TaskModule>?)null;
            if (!withCacheRefresh)
            {
                values = (IList<TaskModule>?)_memoryCache.Get(CacheNameConstants.TypelistModuleTypes);
            }

            if (values is null)
            {
                var types = await _coreService.GetModuleTypes();
                values = _mapper.Map<IList<TaskModule>>(types);

                if (values?.Count > 0)
                {
                    _memoryCache.Set(CacheNameConstants.TypelistModuleTypes, values);
                }
            }

            return values ?? [];
        }
    }
}
