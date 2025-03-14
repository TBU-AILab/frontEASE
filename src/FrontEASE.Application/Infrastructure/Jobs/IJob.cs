using Hangfire.Server;

namespace FrontEASE.Application.Infrastructure.Jobs
{
    public interface IJob
    {
        Task Execute(PerformContext context);
    }
}
