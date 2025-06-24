using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Domain.Services.Tasks
{
    public interface ITaskService
    {
        Task<Entities.Tasks.Task> Load(Guid id, CancellationToken cancellationToken);
        Task<Entities.Tasks.Task> LoadSimple(Guid id, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids, CancellationToken cancellationToken);
        Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> LoadAll(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken);
        Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> Duplicate(Entities.Tasks.Task task, string taskName, int copies, CancellationToken cancellationToken);
        Task Delete(IList<Entities.Tasks.Task> tasks, CancellationToken cancellationToken);
        Task ChangeState(IList<Entities.Tasks.Task> task, TaskState state, CancellationToken cancellationToken);
    }
}