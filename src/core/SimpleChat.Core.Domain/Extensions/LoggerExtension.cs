using Microsoft.Extensions.Logging;

namespace SimpleChat.Core.Domain.Extensions;

public static class LoggerExtension
{
    public static void LogDebug(this ILogger logger, Func<string>? action)
    {
        if (!logger.IsEnabled(LogLevel.Debug) || action == null)
            return;
        logger.LogDebug(action());
    }

    public static void LogError(this ILogger logger, Func<string>? action)
    {
        if (!logger.IsEnabled(LogLevel.Error) || action == null)
            return;
        logger.LogError(action());
    }
        
    public static void LogInformation(this ILogger logger, Func<string>? action)
    {
        if (!logger.IsEnabled(LogLevel.Information) || action == null)
            return;
        logger.LogInformation(action());
    }
        
    public static void LogWarning(this ILogger logger, Func<string>? action)
    {
        if (!logger.IsEnabled(LogLevel.Debug) || action == null)
            return;
        logger.LogWarning(action());
    }
    
    public static void LogCritical(this ILogger logger, Func<string>? action)
    {
        if (!logger.IsEnabled(LogLevel.Critical) || action == null)
            return;
        logger.LogCritical(action());
    }    
}
