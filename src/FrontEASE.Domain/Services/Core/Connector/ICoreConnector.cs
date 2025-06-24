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
        Task HandleTaskCreate(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task HandleTaskInit(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task HandleTaskDuplicate(IList<Entities.Tasks.Task> tasks, Guid origTaskID, string baseName, int copies, CancellationToken cancellationToken);
        Task<bool> HandleTaskDelete(IList<Entities.Tasks.Task> tasks, CancellationToken cancellationToken);
        Task RefreshTaskOptions(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task ChangeTaskState(IList<Entities.Tasks.Task> tasks, TaskState state, CancellationToken cancellationToken);

        Task<FileStreamResult> DownloadTaskFull(Guid taskID, CancellationToken cancellationToken);
        Task<FileStreamResult> DownloadTaskSolution(Guid taskID, Guid messageID, CancellationToken cancellationToken);
        Task ImportModule(Entities.Shared.Files.File moduleFile, CancellationToken cancellationToken);
        Task<bool> DeleteModule(string shortName, CancellationToken cancellationToken);
        Task<bool> UpdateModels(CancellationToken cancellationToken);

        Task<IList<TaskInfoCoreDto>> GetTaskInfos(CancellationToken cancellationToken);
        Task<IList<TaskFullCoreDto>> GetTasksFullData(CancellationToken cancellationToken);
        Task<IList<TaskInfoCoreDto>> GetTaskStates(IList<Guid>? taskIDs, CancellationToken cancellationToken);
        Task<IList<TaskDynamicInfoCoreDto>> GetTaskRunData(IList<Guid>? taskIDs, DateTime? dateFrom, CancellationToken cancellationToken);

        Task<IList<TaskModuleCoreDto>> GetModuleTypes(CancellationToken cancellationToken);

        Task<IList<CorePackageCoreDto>> GetPackages(CancellationToken cancellationToken);
        Task<bool> DeletePackages(IList<GlobalPreferenceCorePackage> packages, CancellationToken cancellationToken);
        Task<bool> AddPackages(IList<GlobalPreferenceCorePackage> packages, CancellationToken cancellationToken);
    }
}