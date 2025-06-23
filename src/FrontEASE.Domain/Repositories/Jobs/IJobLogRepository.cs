using FrontEASE.Domain.Entities.Jobs;

namespace FrontEASE.Domain.Repositories.Jobs
{
    public interface IJobLogRepository : IRepository
    {
        Task<JobLog?> LoadLastSuccessful(string jobName, CancellationToken cancellationToken);
        Task<JobLog> Insert(JobLog jobLog, CancellationToken cancellationToken);
    }
}
