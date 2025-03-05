using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FrontEASE.Client.Services.ApiServices.Shared.Typelists
{
    public interface ITypelistApiService
    {
        Task<IList<TaskModuleNoValidationDto>> LoadTaskModuleOptions();
    }
}
