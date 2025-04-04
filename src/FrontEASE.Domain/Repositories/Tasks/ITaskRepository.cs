using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Messages;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Tasks
{
    public interface ITaskRepository : IRepository
    {
        Task<int> LoadTaskCount();
        Task<TaskMessage?> LoadLastMessage();
        Task<Entities.Tasks.Task?> Load(Guid id, TasksQuery query);
        Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids, TasksQuery query);
        Task<IList<Entities.Tasks.Task>> LoadInfo(Guid? userID = null, TaskFilterActionRequest? filter = null);
        Task<IList<Entities.Tasks.Task>> LoadInfoBase(Guid? userID = null, TaskFilterActionRequest? filter = null);
        Task<IList<Entities.Tasks.Task>> LoadAllWhere(Expression<Func<Entities.Tasks.Task, bool>>? predicate, TasksQuery query);
        Task<Entities.Tasks.Task> Insert(Entities.Tasks.Task task);
        Task<IList<Entities.Tasks.Task>> InsertRange(IList<Entities.Tasks.Task> tasks, bool saveChanges);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task);
        Task Delete(Entities.Tasks.Task task);
        Task DeleteRange(IList<Entities.Tasks.Task> task, bool hardDelete, bool saveChanges);
    }
}