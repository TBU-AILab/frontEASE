using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Client.Services.ApiServices.Shared.Core
{
    public interface ICoreApiService
    {
        Task<IList<ModuleImportBulkActionResultDto>> ImportTaskCoreModules(GlobalPreferenceCoreModuleDto modules);
        Task<bool> DeleteTaskCoreModule(string moduleName);
        Task<string?> GetTaskCoreModuleContent(string moduleName);
        Task<bool> UpdateTaskCoreModule(string moduleName, string content);
        Task<bool> UpdateCoreModels();
        Task<string?> GetAvailableModels();
        Task<bool> SaveAvailableModels(string modelsJson);
    }
}
