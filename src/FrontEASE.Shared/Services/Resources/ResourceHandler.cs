using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using FrontEASE.Shared.Infrastructure.Caching.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace FrontEASE.Shared.Services.Resources
{
    public class ResourceHandler(IMemoryCache memoryCache) : IResourceHandler
    {
        public LanguageCode CurrentLanguage { get; set; }

        public bool CheckResourcesInitialized()
        {
            var resourcesInited = memoryCache.TryGetValue(CacheNameConstants.ResourcesDictionaryKey, out _);
            return resourcesInited;
        }

        public string GetResource(string key)
        {
            var dict = (IDictionary<string, string>?)memoryCache.Get(CacheNameConstants.ResourcesDictionaryKey)!;

            var value = string.Empty;
            dict?.TryGetValue(key, out value);

            return !string.IsNullOrEmpty(value) ? value : key;
        }

        public string GetValidationResource(string key, IEnumerable<string>? arguments)
        {
            var message = GetResource(key);
            if (arguments is not null && arguments.Any())
            {
                message = string.Format(CultureInfo.InvariantCulture, message, [.. arguments]);
            }
            return message;
        }

        public void InitLanguage(string languageCode) => 
            CurrentLanguage = string.IsNullOrWhiteSpace(languageCode) ? LanguageCode.EN : Enum.Parse<LanguageCode>(languageCode);

        public void InitResources(IList<ResourceDto> resources)
        {
            var dict = (IDictionary<string, string>?)memoryCache.Get(CacheNameConstants.ResourcesDictionaryKey);
            if (dict is null)
            {
                var resourcesDict = resources.ToDictionary(item => item.ResourceCode, item => item.Value);
                memoryCache.Set(CacheNameConstants.ResourcesDictionaryKey, resourcesDict);
            }
        }
    }
}
