using FoP_IMT.Shared.Data.DTOs.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Resources
{
    public interface IResourceApiService
    {
        Task<IList<ResourceDto>> LoadResources(LanguageCode language);
    }
}
