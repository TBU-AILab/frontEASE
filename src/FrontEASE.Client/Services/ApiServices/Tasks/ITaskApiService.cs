using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Client.Services.ApiServices.Tasks
{
    public interface ITaskApiService
    {
        Task<TaskDto?> LoadTask(Guid taskID);
        Task<TaskDto?> InsertTask();
        Task<TaskDto?> CloneTask(Guid taskID);
        Task<TaskDto?> UpdateTask(TaskDto updateTaskDto);
        Task<TaskDto?> RefreshTaskOptions(TaskDto refreshTaskDto);
        Task<IList<TaskInfoDto>> LoadTaskInfos();
        Task<IList<TaskStatusDto>> LoadTaskStatuses();
        Task<bool> DeleteTask(Guid taskID);
        Task<bool> ChangeTaskState(TaskInfoDto task, TaskState state);
    }
}
