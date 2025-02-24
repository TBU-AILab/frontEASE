using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Infrastructure.Utils.Extensions;

namespace FoP_IMT.Domain.Services.Shared.Logging.Sentry
{
    public class SentryLoggingService : ISentryLoggingService
    {

        private readonly AppSettings _appSettings;

        public SentryLoggingService(AppSettings appSettings) => _appSettings = appSettings;

        private bool IsEnabled => _appSettings.SentrySettings!.IsEnabled;

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
