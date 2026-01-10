namespace FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific
{
    public class TasksControllerConstants : ControllerConstants
    {
        new public const string BaseUrl = $"{ControllerConstants.BaseUrl}/tasks";

        public const string StateParam = "{state}";

        public const string Clone = "clone";
        public const string ChangeState = "change-state";
        public const string Share = "share";
        public const string SimpleMode = "simple";
    }
}
