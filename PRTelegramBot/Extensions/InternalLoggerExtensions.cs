using Microsoft.Extensions.Logging;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Internal расширения для логирования в стиле ILogger
    /// </summary>
    internal static class InternalLoggerExtensions
    {
        internal static void LogErrorInternal(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, new EventId(), message, null, (state, ex) => state);
        }

        internal static void LogErrorInternal(this ILogger logger, Exception ex, string message = "")
        {
            logger.Log(LogLevel.Error, new EventId(), ex, ex, (state, exception) =>
                string.IsNullOrEmpty(message) ? exception?.ToString() ?? "" : message);
        }

        internal static void LogErrorInternal(this ILogger logger, EventId eventId, string message)
        {
            logger.Log(LogLevel.Error, eventId, message, null, (state, ex) => state);
        }

        internal static void LogErrorInternal(this ILogger logger, EventId eventId, Exception ex, string message = "")
        {
            logger.Log(LogLevel.Error, eventId, ex, ex, (state, exception) =>
                string.IsNullOrEmpty(message) ? exception?.ToString() ?? "" : message);
        }

        // ========================= LogWarning =========================
        internal static void LogWarningInternal(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Warning, new EventId(), message, null, (state, ex) => state);
        }

        internal static void LogWarningInternal(this ILogger logger, Exception ex, string message = "")
        {
            logger.Log(LogLevel.Warning, new EventId(), ex, ex, (state, exception) =>
                string.IsNullOrEmpty(message) ? exception?.ToString() ?? "" : message);
        }

        // ========================= LogInformation =========================
        internal static void LogInformationInternal(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, new EventId(), message, null, (state, ex) => state);
        }

        internal static void LogInformationInternal(this ILogger logger, Exception ex, string message = "")
        {
            logger.Log(LogLevel.Information, new EventId(), ex, ex, (state, exception) =>
                string.IsNullOrEmpty(message) ? exception?.ToString() ?? "" : message);
        }

        // ========================= LogDebug =========================
        internal static void LogDebugInternal(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, new EventId(), message, null, (state, ex) => state);
        }

        // ========================= LogCritical =========================
        internal static void LogCriticalInternal(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Critical, new EventId(), message, null, (state, ex) => state);
        }

        internal static void LogCriticalInternal(this ILogger logger, Exception ex, string message = "")
        {
            logger.Log(LogLevel.Critical, new EventId(), ex, ex, (state, exception) =>
                string.IsNullOrEmpty(message) ? exception?.ToString() ?? "" : message);
        }
    }
}
