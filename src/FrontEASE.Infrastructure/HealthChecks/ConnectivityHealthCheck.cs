using FrontEASE.Infrastructure.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace FrontEASE.Infrastructure.HealthChecks
{
    public class ConnectivityHealthCheck : IHealthCheck
    {
        private readonly ApplicationDbContext _dataContext;

        public ConnectivityHealthCheck(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;
            var message = string.Empty;

            try
            {
                var isDbAvailable = await _dataContext.Database.CanConnectAsync(cancellationToken);
                if (!isDbAvailable)
                {
                    throw new Exception("Database is not available.");
                }
            }
            catch (Exception ex)
            {
                isHealthy = false;
                message = ex.Message;
            }

            return isHealthy ?
                HealthCheckResult.Healthy(HttpStatusCode.OK.ToString()) :
                new HealthCheckResult(context.Registration.FailureStatus, message);
        }
    }
}
