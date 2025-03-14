using FrontEASE.Application.Infrastructure.Jobs.Tasks;
using FrontEASE.Domain.Infrastructure.Settings.App;
using Hangfire;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public static class JobFactory
    {
        public static void ScheduleJobs(AppSettings appSettings, IServiceProvider services)
        {
            var settings = appSettings.HangfireSettings!;
            const string never = "0 0 31 2 *";

            var jobExecutor = new JobExecutor(services);

            /* Tasks */
            RecurringJob.AddOrUpdate(nameof(UpdateTaskStatusesJob), () => jobExecutor.Execute(typeof(UpdateTaskStatusesJob), null!), settings.Jobs?.UpdateTaskStatusesJob?.Cron ?? never);
            RecurringJob.AddOrUpdate(nameof(UpdateTaskDetailsJob), () => jobExecutor.Execute(typeof(UpdateTaskDetailsJob), null!), settings.Jobs?.UpdateTaskDetailsJob?.Cron ?? never);
            RecurringJob.AddOrUpdate(nameof(InitialTaskSyncJob), () => jobExecutor.Execute(typeof(InitialTaskSyncJob), null!), settings.Jobs?.InitialTaskSyncJob?.Cron ?? never);

            BackgroundJob.Enqueue<InitialTaskSyncJob>(job => job.Execute(null!));
        }
    }
}
