using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Client.Services.ApiServices.Tasks
{
    public interface ITaskApiService
    {
        #region Load
        Task<TaskDto?> LoadTask(Guid taskID, bool simple);
        Task<IList<TaskInfoDto>> LoadTaskInfos(TaskFilterActionRequestDto? filter);
        Task<IList<TaskStatusDto>> LoadTaskStatuses();
        #endregion

        #region Insert
        Task<TaskDto?> InsertTask();
        Task<IList<TaskDto>> CloneTask(Guid taskID, TaskDuplicateActionRequestDto request);
        #endregion

        #region Update
        Task<TaskDto?> UpdateTask(Guid taskID, TaskDto updateTaskDto);
        Task<(IList<ApplicationUserDto> Members, IList<CompanyDto> MemberGroups)?> ShareTask(Guid taskID, TaskDto updateTaskDto);
        Task<bool> ChangeTaskStates(IList<Guid> taskIDs, TaskState state);
        #endregion

        #region Delete
        Task<bool> DeleteTasks(IList<Guid> taskIDs);
        #endregion

        #region Manipulate
        Task<TaskDto?> RefreshTaskOptions(Guid taskID, TaskDto refreshTaskDto);
        #endregion
    }
}
