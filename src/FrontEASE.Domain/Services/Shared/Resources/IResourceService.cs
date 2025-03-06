using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Domain.Services.Shared.Resources
{
    public interface IResourceService
    {
        Task<Resource> Load(LanguageCode language, string resourceCode);
        Task<IList<Resource>> LoadAll(LanguageCode language);
    }
}
