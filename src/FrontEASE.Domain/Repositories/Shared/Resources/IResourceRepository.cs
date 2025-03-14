using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Domain.Repositories.Shared.Resources
{
    public interface IResourceRepository : IRepository
    {
        Task<Resource?> Load(LanguageCode language, string resourceCode);
        Task<IList<Resource>> LoadAll(LanguageCode language);
    }
}
