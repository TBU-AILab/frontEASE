using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Application.AppServices.Tasks
{
    public interface ITaskAppService
    {
        Task<TaskDto> Load(Guid id);
        Task<TaskDto> Refresh(TaskDto task);
        Task<IList<TaskInfoDto>> LoadAll();
        Task<IList<TaskStatusDto>> LoadAllStatuses();
        Task<TaskDto> Create();
        Task<TaskDto> Duplicate(Guid id);
        Task<TaskDto> Update(TaskDto task);
        Task Delete(Guid id);
        Task ChangeState(Guid id, TaskState state);
    }
}
