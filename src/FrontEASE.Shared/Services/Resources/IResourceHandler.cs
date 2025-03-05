using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Shared.Services.Resources
{
    public interface IResourceHandler
    {
        public LanguageCode CurrentLanguage { get; set; }

        bool CheckResourcesInitialized();
        string GetResource(string key);
        string GetValidationResource(string message, IEnumerable<string>? arguments);

        void InitLanguage(string languageCode);
        void InitResources(IList<ResourceDto> resources);
    }
}
