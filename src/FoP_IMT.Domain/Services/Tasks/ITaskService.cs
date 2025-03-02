using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;
using FoP_IMT.Shared.Data.Enums.Tasks;

namespace FoP_IMT.Domain.Services.Tasks
{
    public interface ITaskService
    {
        Task<Entities.Tasks.Task> Load(Guid id);
        Task<IList<TaskModule>> RefreshOptions(Entities.Tasks.Task task);
        Task<IList<Entities.Tasks.Task>> LoadAll(Guid? userID);
        Task<IList<Entities.Tasks.Task>> LoadAllBase(Guid? userID);
        Task<Entities.Tasks.Task> Create(Entities.Tasks.Task task);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task);
        Task<Entities.Tasks.Task> Duplicate(Entities.Tasks.Task task);
        Task Delete(Entities.Tasks.Task task);
        Task ChangeState(Entities.Tasks.Task task, TaskState state);
    }
}
