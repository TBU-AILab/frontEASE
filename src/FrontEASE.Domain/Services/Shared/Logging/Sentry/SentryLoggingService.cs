using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;

namespace FrontEASE.Domain.Services.Shared.Logging.Sentry
{
    public class SentryLoggingService(AppSettings appSettings) : ISentryLoggingService
    {
        private bool IsEnabled => appSettings.SentrySettings!.IsEnabled;

        public void Error(object message, object? data = null)
        {
            if (!IsEnabled)
            { return; }

            if (message is Exception e)
            {
                SentrySdk.CaptureException(e, x => x.SetExtra("data", data));
            }
            else
            {
                SentrySdk.CaptureException(new Exception(message.ToString()), x =>
                {
                    x.SetExtra("message", message);
                    x.SetExtra("data", data);
                });
            }
        }

        public void Error(object message, Exception exception, object? data = null)
        {
            if (!IsEnabled)
            { return; }

            SentrySdk.CaptureException(exception, x =>
            {
                x.SetExtra("message", message);
                x.SetExtra("data", data);
            });
        }

        public void Warn(object message)
        {
            if (!IsEnabled)
            { return; }

            SentrySdk.CaptureMessage(message.ToString() ?? string.Empty, SentryLevel.Warning);
        }

        public void Warn(object message, object? data = null)
        {
            if (!IsEnabled)
            { return; }

            SentrySdk.CaptureMessage((message, data).Serialize(), SentryLevel.Warning);
        }

        public void Info(object message)
        {
            if (!IsEnabled)
            { return; }

            SentrySdk.CaptureMessage(message.ToString() ?? string.Empty);
        }
    }
}
