namespace FrontEASE.Shared.Infrastructure.Constants.Auth
{
    public static class UserRoleNames
    {
        public const string UserRoleName = "User";
        public static readonly string UserRoleNameNormalized = UserRoleName.ToUpper();

        public const string AdminRoleName = "Admin";
        public static readonly string AdminRoleNameNormalized = AdminRoleName.ToUpper();

        public const string SuperadminRoleName = "Superadmin";
        public static readonly string SuperadminRoleNameNormalized = SuperadminRoleName.ToUpper();
    }
}
