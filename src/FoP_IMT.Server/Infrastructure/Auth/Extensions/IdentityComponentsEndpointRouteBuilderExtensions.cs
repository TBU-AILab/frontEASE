using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FoP_IMT.Server.Infrastructure.Auth.Extensions
{
    public static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static void MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            endpoints.MapGet("/Account/Logout", async (
                HttpContext context,
                string returnUrl = "/Account/Login") =>
                {
                    await context.SignOutAsync(IdentityConstants.ApplicationScheme);
                    context.Response.Redirect(returnUrl, false);
                }).RequireAuthorization();
        }
    }
}
