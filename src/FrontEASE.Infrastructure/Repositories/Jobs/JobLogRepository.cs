using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Jobs
{
    public class JobLogRepository(ApplicationDbContext context) : IJobLogRepository
    {
        public async Task<JobLog?> LoadLastSuccessful(string jobName, CancellationToken cancellationToken)
        {
            var query = context.JobExecutions
                .Where(x => x.JobName == jobName && x.Success)
                .OrderByDescending(x => x.DateStart);

            var lastJob = await query.FirstOrDefaultAsync(cancellationToken);
            return lastJob;
        }

        public async Task<JobLog> Insert(JobLog jobLog, CancellationToken cancellationToken)
        {
            await context.JobExecutions.AddAsync(jobLog, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return jobLog;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => await context.Database.BeginTransactionAsync(cancellationToken);
    }
}
