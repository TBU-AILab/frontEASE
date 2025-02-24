using FoP_IMT.Shared.Data.DTOs.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Application.AppServices.Shared.Resources
{
    public interface IResourceAppService
    {
        Task<IList<ResourceDto>> LoadAll(LanguageCode language);
    }
}
