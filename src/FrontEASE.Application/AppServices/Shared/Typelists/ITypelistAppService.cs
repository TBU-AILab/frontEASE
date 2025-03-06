using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FrontEASE.Application.AppServices.Shared.Typelists
{
    public interface ITypelistAppService
    {
        Task<IList<TaskModuleNoValidationDto>> LoadModuleTypes();
    }
}
