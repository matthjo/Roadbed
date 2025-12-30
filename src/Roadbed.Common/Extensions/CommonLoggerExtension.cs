/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

using Microsoft.Extensions.Logging;

/// <summary>
/// Extensions for common ILogger operations.
/// </summary>
public static class CommonLoggerExtension
{
    #region Public Methods

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
            /*
            Visual Studio doesn't support ignoring a single line of code from code coverage.
            The "return" causes the 'if' statements closing bracket (}) not to be covered.
             */
        }

        return logger.BeginScope(new Dictionary<string, object> { { key, value } });
    }

    /// <summary>
    /// Logs a message with the specified severity level.
    /// </summary>
    /// <param name="logger">Represents a type used to perform logging.</param>
    /// <param name="level">Defines logging severity levels.</param>
    /// <param name="message">Message to log.</param>
    public static void LogWithCheck(this ILogger logger, LogLevel level, string message)
    {
        logger.LogWithCheck(level, message, Array.Empty<object>());
    }

    /// <summary>
    /// Logs a message with the specified severity level.
    /// </summary>
    /// <param name="logger">Represents a type used to perform logging.</param>
    /// <param name="level">Defines logging severity levels.</param>
    /// <param name="message">Message to log.</param>
    /// <param name="param">Message parameters.</param>
    public static void LogWithCheck(this ILogger logger, LogLevel level, string message, params object[] param)
    {
        if ((logger == null) ||
            string.IsNullOrEmpty(message))
        {
            return;
            /*
            Visual Studio doesn't support ignoring a single line of code from code coverage.
            The "return" causes the 'if' statements closing bracket (}) not to be covered.
             */
        }

        // Checks if the given logLevel is enabled.
        if (logger.IsEnabled(level))
        {
            logger.Log(level, message, param);
        }
    }

    #endregion Public Methods
}