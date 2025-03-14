using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Shared.Data.Enums.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Domain.Services.Tasks.Core
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

        Task<IList<TaskInfoCoreDto>> GetTaskInfos();
        Task<IList<TaskFullCoreDto>> GetTasksFullData();
        Task<IList<TaskInfoCoreDto>> GetTaskStates();
        Task<IList<TaskDynamicInfoCoreDto>> GetTaskRunData(DateTime? dateFrom);

        Task<IList<TaskModuleCoreDto>> GetModuleTypes();
    }
}