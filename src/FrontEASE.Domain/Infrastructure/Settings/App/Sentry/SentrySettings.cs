namespace FrontEASE.Domain.Infrastructure.Settings.App.Sentry
{
    public class SentrySettings
    {
        public bool IsEnabled { get; set; }
        public string? DSN { get; set; }
        public string? CronMonitorAPI { get; set; }
        public string? Release { get; set; }
        public string? Environment { get; set; }
    }
}
