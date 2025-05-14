using Hangfire.Server;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public interface IJob
    {
        string JobName { get; init; }

        Task Execute(PerformContext context);
    }
}
