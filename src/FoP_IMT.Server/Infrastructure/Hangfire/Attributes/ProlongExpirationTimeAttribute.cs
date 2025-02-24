using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace FoP_IMT.Server.Infrastructure.Hangfire.Attributes
{
    public class ProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        public void OnStateApplied(ApplyStateContext filterContext, IWriteOnlyTransaction transaction) => filterContext.JobExpirationTimeout = TimeSpan.FromDays(3);
        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) => context.JobExpirationTimeout = TimeSpan.FromDays(3);
    }
}
