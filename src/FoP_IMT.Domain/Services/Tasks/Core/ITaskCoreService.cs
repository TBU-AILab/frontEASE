using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FoP_IMT.DataContracts.Models.Core.Tasks.Info;
using FoP_IMT.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoP_IMT.Domain.Services.Tasks.Core
{
    public interface ITaskCoreService
    {
        Task HandleTaskCreate(Entities.Tasks.Task task);
        Task HandleTaskInit(Entities.Tasks.Task task);
        Task HandleTaskDuplicate(Entities.Tasks.Task task, Guid origTaskID);
        Task HandleTaskDelete(Entities.Tasks.Task task);
        Task RefreshTaskOptions(Entities.Tasks.Task task);
        Task ChangeTaskState(Entities.Tasks.Task task, TaskState state);

        Task<FileStreamResult> DownloadTaskFull(Guid taskID);
        Task<FileStreamResult> DownloadTaskSolution(Guid taskID, Guid messageID);

        Task<IDictionary<Guid, TaskInfoCoreDto>> GetTaskStates();
        Task<IDictionary<Guid, TaskDynamicInfoCoreDto>> GetTaskRunData(DateTime? dateFrom);

        Task<IList<TaskModuleCoreDto>> GetModuleTypes();
    }
}
