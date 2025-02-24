using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FoP_IMT.Application.AppServices.Shared.Typelists
{
    public interface ITypelistAppService
    {
        Task<IList<TaskModuleNoValidationDto>> LoadModuleTypes();
    }
}
