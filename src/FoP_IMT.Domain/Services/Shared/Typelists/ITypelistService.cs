using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;

namespace FoP_IMT.Domain.Services.Shared.Typelists
{
    public interface ITypelistService
    {
        Task<IList<TaskModule>> LoadModuleTypes();
    }
}
