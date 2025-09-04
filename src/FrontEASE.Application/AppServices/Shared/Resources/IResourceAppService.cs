using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Application.AppServices.Shared.Resources
{
    public interface IResourceAppService
    {
        Task<IList<ResourceDto>> LoadAll(LanguageCode language, CancellationToken cancellationToken);
    }
}
