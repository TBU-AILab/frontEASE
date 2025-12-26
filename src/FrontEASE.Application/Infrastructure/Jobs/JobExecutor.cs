using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.DependencyInjection;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public class JobExecutor(IServiceProvider services)
    {
        [JobDisplayName("{0}")]
        public async Task Execute(Type type, PerformContext context)
        {
            var job = services.GetRequiredService(type) as IJob;
            var jobName = job!.GetType().Name;

            try
            {
                await job.Execute(context);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{jobName} - Error: {ex}");
            }
        }
    }
}
