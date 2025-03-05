namespace FrontEASE.Domain.Services.Shared.Logging
{
    public interface ILoggingService
    {
        void Error(object message, object? data = null);
        void Error(object message, Exception exception, object? data = null);
        void Warn(object message);
        void Warn(object message, object? data = null);
        void Info(object message);
    }
}
