namespace FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific
{
    public class CoreControllerConstants : ControllerConstants
    {
        new public const string BaseUrl = $"{ControllerConstants.BaseUrl}/core";

        public const string Module = "module";
        public const string Models = "models";

        public const string NameParam = "{name}";
    }
}
