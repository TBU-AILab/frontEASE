using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Tasks
{
    public interface ITaskRepository : IRepository
    {
        Task<int> LoadTaskCount(CancellationToken cancellationToken);
        Task<Entities.Tasks.Task?> Load(Guid id, TasksQuery query, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> Load(IList<Guid> ids, TasksQuery query, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> LoadInfo(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> LoadInfoBase(Guid? userID, TaskFilterActionRequest? filter, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> LoadAllWhere(Expression<Func<Entities.Tasks.Task, bool>>? predicate, TasksQuery query, CancellationToken cancellationToken);
        Task<IList<TaskMessage>> LoadAllMessagesWhere(Expression<Func<TaskMessage, bool>>? predicate, CancellationToken cancellationToken);
        Task<Entities.Tasks.Task> Insert(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> InsertRange(IList<Entities.Tasks.Task> tasks, bool saveChanges, CancellationToken cancellationToken);
        Task<Entities.Tasks.Task> Update(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task<IList<Entities.Tasks.Task>> UpdateRange(IList<Entities.Tasks.Task> task, CancellationToken cancellationToken);
        Task Delete(Entities.Tasks.Task task, CancellationToken cancellationToken);
        Task DeleteRange(IList<TaskSolution> solutions, bool saveChanges, CancellationToken cancellationToken);
        Task DeleteRange(IList<TaskMessage> messages, bool saveChanges, CancellationToken cancellationToken);
        Task DeleteRange(IList<Entities.Tasks.Task> task, bool hardDelete, bool saveChanges, CancellationToken cancellationToken);
    }
}