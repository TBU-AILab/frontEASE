using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Client.Services.ApiServices.Tasks
{
    public interface ITaskApiService
    {
        #region Load
        Task<TaskDto?> LoadTask(Guid taskID);
        Task<IList<TaskInfoDto>> LoadTaskInfos();
        Task<IList<TaskStatusDto>> LoadTaskStatuses();
        #endregion

        #region Insert
        Task<TaskDto?> InsertTask();
        Task<IList<TaskDto>> CloneTask(Guid taskID, TaskDuplicateActionRequestDto request);
        #endregion

        #region Update
        Task<TaskDto?> UpdateTask(Guid taskID, TaskDto updateTaskDto);
        Task<IList<TaskBulkActionResultDto>> ChangeTaskStates(IList<Guid> taskIDs, TaskState state);
        #endregion

        #region Delete
        Task<IList<TaskBulkActionResultDto>> DeleteTasks(IList<Guid> taskIDs);
        #endregion

        #region Manipulate
        Task<TaskDto?> RefreshTaskOptions(Guid taskID, TaskDto refreshTaskDto);
        #endregion
    }
}
