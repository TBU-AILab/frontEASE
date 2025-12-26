using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FrontEASE.Domain.Services.Shared.Typelists
{
    public class TypelistService(IMapper mapper, IMemoryCache memoryCache, ICoreConnector coreService) : ITypelistService
    {
        public async Task<IList<TaskModule>> LoadModuleTypes(bool withCacheRefresh, CancellationToken cancellationToken)
        {
            var values = (IList<TaskModule>?)null;
            if (!withCacheRefresh)
            {
                values = (IList<TaskModule>?)memoryCache.Get(CacheNameConstants.TypelistModuleTypes);
            }

            if (values is null)
            {
                var types = await coreService.GetModuleTypes(cancellationToken);
                values = mapper.Map<IList<TaskModule>>(types);

                if (values?.Count > 0)
                {
                    memoryCache.Set(CacheNameConstants.TypelistModuleTypes, values);
                }
            }

            return values ?? [];
        }
    }
}
