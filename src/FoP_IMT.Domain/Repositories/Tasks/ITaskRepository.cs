using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using System.Linq.Expressions;

namespace FoP_IMT.Domain.Repositories.Tasks
{
    public interface ITaskRepository : IRepository
    {
        Task<int> LoadTaskCount();
        Task<TaskMessage?> LoadLastMessage();
        Task<Entities.Tasks.Task?> Load(Guid id, TasksQuery query);
        Task<IList<Entities.Tasks.Task>> LoadInfo(Guid? userID = null);
        Task<IList<Entities.Tasks.Task>> LoadInfoBase(Guid? userID = null);
        Task<IList<Entities.Tasks.Task>> LoadAllWhere(Expression<Func<Entities.Tasks.Task, bool>> predicate, TasksQuery query);
        Task<Entities.Tasks.Task> Insert(Entities.Tasks.Task task);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task);
        Task Delete(Entities.Tasks.Task task);
        Task HardDeleteRange(IList<Entities.Tasks.Task> task, bool performSave);
    }
}
