using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Client.Services.ApiServices.Shared.Resources
{
    public interface IResourceApiService
    {
        Task<IList<ResourceDto>> LoadResources(LanguageCode language);
    }
}
