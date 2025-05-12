using FrontEASE.Domain.Entities.Jobs;

namespace FrontEASE.Domain.Repositories.Jobs
{
    public interface IJobLogRepository : IRepository
    {
        Task<JobLog?> LoadLastSuccessful(string jobName);

        Task<JobLog> Insert(JobLog jobLog);
    }
}
