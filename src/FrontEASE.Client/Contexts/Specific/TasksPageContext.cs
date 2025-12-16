namespace FrontEASE.Client.Contexts.Specific
{
    public class TasksPageContext : ApplicationContext
    {
        public TasksPageContext(ApplicationContext appContext)
        {
            LoggedInUser = appContext.LoggedInUser;
            UserPreferences = appContext.UserPreferences;
        }
    }
}
