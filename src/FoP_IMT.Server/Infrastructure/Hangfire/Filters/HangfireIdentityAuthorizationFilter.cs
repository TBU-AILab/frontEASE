using FoP_IMT.Shared.Infrastructure.Constants.Auth;
using Hangfire.Dashboard;

namespace FoP_IMT.Server.Infrastructure.Hangfire.Filters
{
    public class HangfireIdentityAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HangfireIdentityAuthorizationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null || httpContext.User?.Identity?.IsAuthenticated != true)
            {
                return false;
            }

            var isAdmin = httpContext.User.IsInRole(UserRoleNames.SuperadminRoleName);
            return isAdmin;
        }
    }
}
