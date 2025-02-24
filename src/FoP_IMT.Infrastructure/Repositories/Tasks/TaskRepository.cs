using FoP_IMT.Domain.DataQueries.Tasks;
using FoP_IMT.Domain.Entities.Tasks.Messages;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FoP_IMT.Infrastructure.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Domain.Entities.Tasks.Task> ComposeQuery(TasksQuery query)
        {
            var tasksQuery = _context.Tasks.AsQueryable();

            if (query.LoadSolutions) { tasksQuery = tasksQuery.Include(x => x.Solutions); }
            if (query.LoadMessages) { tasksQuery = tasksQuery.Include(x => x.Messages); }
            if (query.IncludeMembers) { tasksQuery = tasksQuery.Include(x => x.Members); }
            if (query.IncludeGroups) { tasksQuery = tasksQuery.Include(x => x.MemberGroups); }

            if (query.LoadConfig)
            {
                if (query.LoadConfigModules)
                {
                    tasksQuery = tasksQuery
                        .Include(x => x.Config)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Parameters)
                                .ThenInclude(x => x.Value)
                                    .ThenInclude(x => x!.EnumValue)
                                        .ThenInclude(x => x!.ModuleValue)
                                            .ThenInclude(x => x!.Parameters)
                                                .ThenInclude(x => x!.Value)
                                                    .ThenInclude(x => x!.EnumValue);
                }
                else
                {
                    tasksQuery = tasksQuery.Include(x => x.Config);
                }

                if (query.LoadConfigRepeatedMessage) { tasksQuery = tasksQuery.Include(x => x.Config).ThenInclude(x => x.RepeatedMessage).ThenInclude(x => x.RepeatedMessageItems); }

                tasksQuery = tasksQuery.Include(x => x.Config);
            }

            if (query.WithNoTracking) { tasksQuery = tasksQuery.AsNoTracking(); }
            tasksQuery = tasksQuery.AsSplitQuery();

            return tasksQuery;
        }


        public async Task<IList<Domain.Entities.Tasks.Task>> LoadAllWhere(Expression<Func<Domain.Entities.Tasks.Task, bool>> predicate, TasksQuery query)
        {
            var tasksQuery = ComposeQuery(query);
            return await tasksQuery.Where(predicate).ToListAsync() ?? [];
        }

        public async Task<Domain.Entities.Tasks.Task?> Load(Guid id, TasksQuery query)
        {
            var taskQuery = ComposeQuery(query);
            return await taskQuery.SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<TaskMessage?> LoadLastMessage()
        {
            var lastMessage = await _context.TaskMessages
                .OrderByDescending(x => x.DateCreated)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return lastMessage;
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> LoadInfo(Guid? userID = null)
        {
            var tasksQuery = _context.Tasks
                .AsSplitQuery()
                .AsNoTracking()
                .Include(x => x.Config)
                    .ThenInclude(x => x.Modules)
                .Include(x => x.Members)
                .Include(x => x.MemberGroups)
                    .ThenInclude(x => x.Users)
                .Where(x => !x.IsDeleted);

            var tasks = userID is null ?
                await tasksQuery.ToListAsync() :
                await tasksQuery
                    .Where(x =>
                        x.Members.Any(x => x.Id == userID!.ToString()) ||
                        x.MemberGroups.Any(x => x.Users.Any(x => x.Id == userID!.ToString())))
                    .ToListAsync();

            return tasks;
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> LoadInfoBase(Guid? userID = null)
        {
            var tasksQuery = _context.Tasks
                .AsSplitQuery()
                .AsNoTracking()
                .Include(x => x.Members)
                .Include(x => x.MemberGroups)
                    .ThenInclude(x => x.Users)
                .Where(x => !x.IsDeleted);

            var tasks = userID is null ?
                await tasksQuery.ToListAsync() :
                await tasksQuery
                    .Where(x =>
                        x.Members.Any(x => x.Id == userID!.ToString()) ||
                        x.MemberGroups.Any(x => x.Users.Any(x => x.Id == userID!.ToString())))
                    .ToListAsync();

            return tasks;
        }

        public async Task<Domain.Entities.Tasks.Task> Insert(Domain.Entities.Tasks.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Domain.Entities.Tasks.Task> Update(Domain.Entities.Tasks.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task Delete(Domain.Entities.Tasks.Task task)
        {
            task.IsDeleted = true;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);

    }
}
