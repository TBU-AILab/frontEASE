using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Typelists
{
    public interface ITypelistApiService
    {
        Task<IList<TaskModuleNoValidationDto>> LoadTaskModuleOptions();
    }
}
