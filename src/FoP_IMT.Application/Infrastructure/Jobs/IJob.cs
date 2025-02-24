using Hangfire.Server;

namespace FoP_IMT.Application.Infrastructure.Jobs
{
    public interface IJob
    {
        Task Execute(PerformContext context);
    }
}
