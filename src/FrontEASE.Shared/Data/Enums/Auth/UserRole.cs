namespace FrontEASE.Shared.Data.Enums.Auth
{
    /// <summary>
    /// Enumeration for dividing user roles and their privileges within the application.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// USER - Can view and manipulate his own tasks.
        /// </summary>
        USER,

        /// <summary>
        /// ADMIN - Can view and manipulate all tasks (even tasks from other users).
        /// </summary>
        ADMIN,

        /// <summary>
        /// OWNER - Can view and manipulate all tasks (even tasks from other users) + is able to manage other user and admin accounts.
        /// </summary>
        OWNER
    }
}
