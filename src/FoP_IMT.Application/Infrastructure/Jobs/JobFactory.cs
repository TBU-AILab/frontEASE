using FoP_IMT.Application.Infrastructure.Jobs.Tasks;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using Hangfire;

namespace FoP_IMT.Application.Infrastructure.Jobs
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
        }
    }
}
