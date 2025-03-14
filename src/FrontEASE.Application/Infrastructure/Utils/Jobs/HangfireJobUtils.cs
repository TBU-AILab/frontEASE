using Hangfire;
using Hangfire.Storage;

namespace FrontEASE.Application.Infrastructure.Utils.Jobs
{
    public static class HangfireJobUtils
    {
        public static DateTime? GetLastExecutionTime(string jobName)
        {
            var recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
            var job = recurringJobs.FirstOrDefault(j => j.Id == jobName);

            if (job is not null && job.LastExecution.HasValue)
            {
                return job.LastExecution.Value;
            }
            return null;
        }
    }
}
