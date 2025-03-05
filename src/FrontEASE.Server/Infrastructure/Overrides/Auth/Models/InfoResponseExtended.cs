using FrontEASE.Domain.Entities.Shared.Users;

namespace FrontEASE.Server.Infrastructure.Overrides.Auth.Models
{
    public sealed class InfoResponseExtended
    {
        public InfoResponseExtended()
        {
            Roles = [];
        }

        /// <summary>
        /// The username associated with the authenticated user.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// The email address associated with the authenticated user.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// User Roles
        /// </summary>
        public required IList<string> Roles { get; init; }
    }
}
