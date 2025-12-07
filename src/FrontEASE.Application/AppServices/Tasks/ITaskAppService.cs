using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Application.AppServices.Tasks
{
    public interface ITaskAppService
    {
        Task<TaskDto> Load(Guid id, CancellationToken cancellationToken);
        Task<TaskDto> LoadSimple(Guid id, CancellationToken cancellationToken);
        Task<TaskDto> Refresh(TaskDto task, CancellationToken cancellationToken);
        Task<IList<TaskInfoDto>> LoadAll(TaskFilterActionRequestDto? filter, CancellationToken cancellationToken);
        Task<IList<TaskStatusDto>> LoadAllStatuses(CancellationToken cancellationToken);
        Task<TaskDto> Create(CancellationToken cancellationToken);
        Task<IList<TaskDto>> Duplicate(Guid id, TaskDuplicateActionRequestDto request, CancellationToken cancellationToken);
        Task<TaskDto> Update(TaskDto task, CancellationToken cancellationToken);
        Task<TaskDto> Share(TaskDto task, CancellationToken cancellationToken);
        Task Delete(IList<Guid> ids, CancellationToken cancellationToken);
        Task ChangeState(IList<Guid> ids, TaskState state, CancellationToken cancellationToken);
    }
}
