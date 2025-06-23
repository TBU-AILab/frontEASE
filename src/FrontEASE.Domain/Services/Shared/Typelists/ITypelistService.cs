using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;

namespace FrontEASE.Domain.Services.Shared.Typelists
{
    public interface ITypelistService
    {
        Task<IList<TaskModule>> LoadModuleTypes(bool withCacheRefresh, CancellationToken cancellationToken);
    }
}
