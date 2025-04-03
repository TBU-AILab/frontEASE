using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options;
using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Domain.Services.Tasks
{
    public interface ITaskService
    {
        Task<Entities.Tasks.Task> Load(Guid id);
        Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids);
        Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task);
        Task<IList<Entities.Tasks.Task>> LoadAll(Guid? userID);
        Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid? userID);
        Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task);
        Task<Entities.Tasks.Task> Duplicate(Entities.Tasks.Task task, string taskName);
        Task Delete(Entities.Tasks.Task task);
        Task ChangeState(Entities.Tasks.Task task, TaskState state);
    }
}