using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Domain.Repositories.Jobs;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public class JobBase
    {
        protected readonly IJobLogRepository _jobLogRepository;

        public JobBase(IJobLogRepository jobLogRepository)
        {
            _jobLogRepository = jobLogRepository;
        }

        protected async Task<JobLog> UpdateJobLog(JobLog log, DateTime? dateEnd, bool success)
        {
            log.Success = success;
            log.DateEnd = dateEnd;

            await _jobLogRepository.SaveChangesAsync();
            return log;
        }

        protected async Task<JobLog> InsertJobLog(string jobName, DateTime dateStart, DateTime? dateEnd, bool success)
        {
            var log = new JobLog()
            {
                DateStart = dateStart,
                DateEnd = dateEnd,
                Success = success,
                JobName = jobName,
            };

            return await _jobLogRepository.Insert(log);
        }
    }
}
