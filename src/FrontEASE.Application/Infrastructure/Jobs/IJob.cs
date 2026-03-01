using Hangfire;
using Hangfire.Server;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public interface IJob
    {
        string JobName { get; init; }

        [AutomaticRetry(Attempts = 0)]
        Task Execute(PerformContext context);
    }
}
