﻿using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FrontEASE.Infrastructure.Repositories.Tasks
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


        public async Task<IList<Domain.Entities.Tasks.Task>> LoadAllWhere(Expression<Func<Domain.Entities.Tasks.Task, bool>>? predicate, TasksQuery query)
        {
            var tasksQuery = ComposeQuery(query);
            if (predicate is not null)
            {
                tasksQuery = tasksQuery.Where(predicate);
            }
            return await tasksQuery.ToListAsync() ?? [];
        }

        public async Task<Domain.Entities.Tasks.Task?> Load(Guid id, TasksQuery query)
        {
            var taskQuery = ComposeQuery(query);
            return await taskQuery.SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> Load(IList<Guid> ids, TasksQuery query)
        {
            var taskQuery = ComposeQuery(query);
            return await taskQuery.Where(x => ids.Contains(x.ID)).ToListAsync();
        }


        public async Task<int> LoadTaskCount() => await _context.Tasks.CountAsync();

        public async Task<TaskMessage?> LoadLastMessage()
        {
            var lastMessage = await _context.TaskMessages
                .OrderByDescending(x => x.DateCreated)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return lastMessage;
        }

        private IQueryable<Domain.Entities.Tasks.Task> GetFilterQuery(IQueryable<Domain.Entities.Tasks.Task> tasksQuery, TaskFilterActionRequest filter)
        {
            var utcDateCreatedFrom = filter.DateCreatedFrom.HasValue ? DateTime.SpecifyKind(filter.DateCreatedFrom.Value.Date, DateTimeKind.Utc) : (DateTime?)null;
            var utcDateCreatedTo = filter.DateCreatedTo.HasValue ? DateTime.SpecifyKind(filter.DateCreatedTo.Value.Date.AddDays(1), DateTimeKind.Utc) : (DateTime?)null;
            var utcDateUpdatedFrom = filter.DateUpdatedFrom.HasValue ? DateTime.SpecifyKind(filter.DateUpdatedFrom.Value.Date, DateTimeKind.Utc) : (DateTime?)null;
            var utcDateUpdatedTo = filter.DateUpdatedTo.HasValue ? DateTime.SpecifyKind(filter.DateUpdatedTo.Value.Date.AddDays(1), DateTimeKind.Utc) : (DateTime?)null;

            if (string.IsNullOrWhiteSpace(filter.MessagesContent))
            {
                tasksQuery = tasksQuery.Include(x => x.Messages);
            }

            tasksQuery = tasksQuery
                .Where(x =>
                    (string.IsNullOrWhiteSpace(filter.Name) || EF.Functions.ILike(x.Config.Name, $"%{filter.Name}%")) &&
                    (string.IsNullOrWhiteSpace(filter.MessagesContent) || x.Messages.Any(e => EF.Functions.ILike(e.Content, $"%{filter.Name}%"))) &&
                    (filter.State == null || !filter.State.Any() || filter.State.Contains(x.State)) &&
                    (utcDateCreatedFrom == null || x.DateCreated >= utcDateCreatedFrom.Value) &&
                    (utcDateCreatedTo == null || x.DateCreated < utcDateCreatedTo.Value) &&
                    (utcDateUpdatedFrom == null || (x.DateUpdated != null && x.DateUpdated >= utcDateUpdatedFrom.Value)) &&
                    (utcDateUpdatedTo == null || (x.DateUpdated != null && x.DateUpdated < utcDateUpdatedTo.Value))
                );

            return tasksQuery;
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> LoadInfo(Guid? userID = null, TaskFilterActionRequest? filter = null)
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

            tasksQuery = filter is null ? tasksQuery : GetFilterQuery(tasksQuery, filter);

            var tasks = userID is null ?
                await tasksQuery.ToListAsync() :
                await tasksQuery
                    .Where(x =>
                        x.Members.Any(x => x.Id == userID!.ToString()) ||
                        x.MemberGroups.Any(x => x.Users.Any(x => x.Id == userID!.ToString())))
                    .ToListAsync();

            return tasks;
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> LoadInfoBase(Guid? userID = null, TaskFilterActionRequest? filter = null)
        {
            var tasksQuery = _context.Tasks
                .AsSplitQuery()
                .AsNoTracking()
                .Include(x => x.Members)
                .Include(x => x.MemberGroups)
                    .ThenInclude(x => x.Users)
                .Where(x => !x.IsDeleted);

            tasksQuery = filter is null ? tasksQuery : GetFilterQuery(tasksQuery, filter);

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

        public async Task<IList<Domain.Entities.Tasks.Task>> InsertRange(IList<Domain.Entities.Tasks.Task> tasks, bool saveChanges)
        {
            await _context.Tasks.AddRangeAsync(tasks);
            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }
            return tasks;
        }

        public async Task<Domain.Entities.Tasks.Task> Update(Domain.Entities.Tasks.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IList<Domain.Entities.Tasks.Task>> UpdateRange(IList<Domain.Entities.Tasks.Task> tasks)
        {
            _context.Tasks.UpdateRange(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

        public async Task Delete(Domain.Entities.Tasks.Task task)
        {
            task.IsDeleted = true;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IList<Domain.Entities.Tasks.Task> tasks, bool hardDelete, bool saveChanges)
        {
            if (hardDelete)
            {
                _context.Tasks.RemoveRange(tasks);
            }
            else
            {
                foreach (var task in tasks)
                {
                    task.IsDeleted = true;
                    _context.Tasks.Update(task);
                }
            }

            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);

    }
}