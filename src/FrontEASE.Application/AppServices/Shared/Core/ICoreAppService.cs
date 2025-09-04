using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Application.AppServices.Shared.Core
{
    public interface ICoreAppService
    {
        Task<IList<ModuleImportBulkActionResultDto>> ImportModules(GlobalPreferenceCoreModuleDto modulesContent, CancellationToken cancellationToken);
        Task DeleteModule(string shortName, CancellationToken cancellationToken);
        Task UpdateModels(CancellationToken cancellationToken);
    }
}
