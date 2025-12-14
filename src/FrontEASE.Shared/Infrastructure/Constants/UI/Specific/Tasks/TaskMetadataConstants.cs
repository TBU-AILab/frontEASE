namespace FrontEASE.Shared.Infrastructure.Constants.UI.Specific.Tasks
{
    public static class TaskMetadataConstants
    {
        public const string InitMessageTemplate = "init_msg_template";
        public const string Token = "token";
        public const string Url = "url";

        public const string ShortName = "ShortName";
        public const string StringValue = "StringValue";

        public static readonly IList<string> ReloadTriggerFields = 
        [
            ShortName,
            StringValue
        ];
    }
}
