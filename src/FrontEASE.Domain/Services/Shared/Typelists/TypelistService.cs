﻿using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Domain.Services.Tasks.Core;
using FrontEASE.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FrontEASE.Domain.Services.Shared.Typelists
{
    public class TypelistService : ITypelistService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ITaskCoreService _taskCoreService;

        public TypelistService(IMapper mapper, IMemoryCache memoryCache, ITaskCoreService taskCoreService)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _taskCoreService = taskCoreService;
        }

        public async Task<IList<TaskModule>> LoadModuleTypes()
        {
            var values = (IList<TaskModule>?)_memoryCache.Get(CacheNameConstants.TypelistModuleTypes);
            if (values is null)
            {
                var types = await _taskCoreService.GetModuleTypes();
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
