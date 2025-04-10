using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Client.Services.ApiServices.Shared.Core
{
    public interface ICoreApiService
    {
        Task<IList<ModuleImportBulkActionResultDto>> ImportTaskModules(GlobalPreferenceCoreModuleDto modules);
    }
}
