namespace FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific
{
    public class TasksControllerConstants : ControllerConstants
    {
        new public const string BaseUrl = $"{ControllerConstants.BaseUrl}/tasks";

        public const string StateParam = "{state}";
        public const string Clone = "clone";
    }
}
