/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */
namespace Roadbed
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Extensions for common ILogger operations.
    /// </summary>
    public static class CommonLoggerExtensions
    {
        /// <summary>
        /// Appends a single key/value pair to the logging scope.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="key">Key for the dictionary entry.</param>
        /// <param name="value">Value for the dictionary entry.</param>
        /// <returns>Logger with the scope attached.</returns>
        /// <remarks>
        /// For more context, see https://andrewlock.net/creating-an-extension-method-for-attaching-key-value-pairs-to-scope-state-using-asp-net-core/.
        /// <code>
        /// using (this.Logger.BeginScope("transactionId", transaction.Id))
        /// {
        ///     this.Logger.LogInformation("Successful transaction");
        /// }
        /// </code>
        /// </remarks>
        public static IDisposable? BeginScope(this ILogger logger, string key, object value)
        {
            if ((logger == null) ||
                string.IsNullOrEmpty(key))
            {
                return default;
            }

            return logger.BeginScope(new Dictionary<string, object> { { key, value } });
        }

        /// <summary>
        /// Logs a message with the specified severity level.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="level">Defines logging severity levels.</param>
        /// <param name="message">Message to log.</param>
        public static void Log(this ILogger logger, LogLevel level, string message)
        {
            if ((logger == null) ||
                string.IsNullOrEmpty(message))
            {
                return;
            }

            // Checks if the given logLevel is enabled.
            if (logger.IsEnabled(level))
            {
                logger.Log(level, message);
            }
        }

        /// <summary>
        /// Logs a message with a severity level of <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="message">Message to log.</param>
        public static void LogDebug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a message with a severity level of <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="message">Message to log.</param>
        public static void LogError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Logs a message with a severity level of <see cref="LogLevel.Information"/>.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="message">Message to log.</param>
        public static void LogInformation(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, message);
        }

        /// <summary>
        /// Logs a message with a severity level of <see cref="LogLevel.Trace"/>.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="message">Message to log.</param>
        public static void LogTrace(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Trace, message);
        }

        /// <summary>
        /// Logs a message with a severity level of <see cref="LogLevel.Warning"/>.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="message">Message to log.</param>
        public static void LogWarning(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Warning, message);
        }
    }
}