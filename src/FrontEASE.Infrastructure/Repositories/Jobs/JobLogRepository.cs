using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Jobs
{
    public class JobLogRepository : IJobLogRepository
    {
        private readonly ApplicationDbContext _context;

        public JobLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobLog?> LoadLastSuccessful(string jobName)
        {
            var query = _context.JobExecutions
                .Where(x => x.JobName == jobName && x.Success)
                .OrderByDescending(x => x.DateStart);

            var lastJob = await query.FirstOrDefaultAsync();
            return lastJob;
        }

        public async Task<JobLog> Insert(JobLog jobLog)
        {
            await _context.JobExecutions.AddAsync(jobLog);
            await _context.SaveChangesAsync();

            return jobLog;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
