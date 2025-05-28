using FrontEASE.DataContracts.Models.Core.Packages;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Domain.Services.Core.Connector
{
    public interface ICoreConnector
    {
        Task HandleTaskCreate(Entities.Tasks.Task task);
        Task HandleTaskInit(Entities.Tasks.Task task);
        Task HandleTaskDuplicate(IList<Entities.Tasks.Task> tasks, Guid origTaskID, string baseName, int copies);
        Task<bool> HandleTaskDelete(IList<Entities.Tasks.Task> tasks);
        Task RefreshTaskOptions(Entities.Tasks.Task task);
        Task ChangeTaskState(IList<Entities.Tasks.Task> tasks, TaskState state);

        Task<FileStreamResult> DownloadTaskFull(Guid taskID);
        Task<FileStreamResult> DownloadTaskSolution(Guid taskID, Guid messageID);
        Task ImportModule(Entities.Shared.Files.File moduleFile);
        Task<bool> DeleteModule(string shortName);
        Task<bool> UpdateModels();

        Task<IList<TaskInfoCoreDto>> GetTaskInfos();
        Task<IList<TaskFullCoreDto>> GetTasksFullData();
        Task<IList<TaskInfoCoreDto>> GetTaskStates(IList<Guid>? taskIDs);
        Task<IList<TaskDynamicInfoCoreDto>> GetTaskRunData(IList<Guid>? taskIDs, DateTime? dateFrom);

        Task<IList<TaskModuleCoreDto>> GetModuleTypes();

        Task<IList<CorePackageCoreDto>> GetPackages();
        Task<bool> DeletePackages(IList<GlobalPreferenceCorePackage> packages);
        Task<bool> AddPackages(IList<GlobalPreferenceCorePackage> packages);
    }
}