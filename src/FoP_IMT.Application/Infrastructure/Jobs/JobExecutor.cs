using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.DependencyInjection;

namespace FoP_IMT.Application.Infrastructure.Jobs
{
    public class JobExecutor
    {
        private readonly IServiceProvider _services;

        public JobExecutor(IServiceProvider services) => _services = services;

        [JobDisplayName("{0}")]
        public async Task Execute(Type type, PerformContext context)
        {
            var job = _services.GetRequiredService(type) as IJob;
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
