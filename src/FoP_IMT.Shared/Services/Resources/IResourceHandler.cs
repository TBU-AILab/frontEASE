using FoP_IMT.Shared.Data.DTOs.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Shared.Services.Resources
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
