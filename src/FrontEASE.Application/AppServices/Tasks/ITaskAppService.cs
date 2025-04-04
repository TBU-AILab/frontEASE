using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Application.AppServices.Tasks
{
    public interface ITaskAppService
    {
        Task<TaskDto> Load(Guid id);
        Task<TaskDto> Refresh(TaskDto task);
        Task<IList<TaskInfoDto>> LoadAll(TaskFilterActionRequestDto? filter);
        Task<IList<TaskStatusDto>> LoadAllStatuses();
        Task<TaskDto> Create();
        Task<IList<TaskDto>> Duplicate(Guid id, TaskDuplicateActionRequestDto request);
        Task<TaskDto> Update(TaskDto task);
        Task<IList<TaskBulkActionResultDto>> Delete(IList<Guid> ids);
        Task<IList<TaskBulkActionResultDto>> ChangeState(IList<Guid> ids, TaskState state);
    }
}
