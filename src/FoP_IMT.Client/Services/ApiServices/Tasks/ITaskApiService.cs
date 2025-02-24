using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;
using FoP_IMT.Shared.Data.Enums.Tasks;

namespace FoP_IMT.Client.Services.ApiServices.Tasks
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
