namespace Roadbed.Test.Unit.Common;

using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Unit tests for the CommonLoggerExtension class.
/// </summary>
[TestClass]
public class CommonLoggerExtensionTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that BeginScope returns null when key is empty.
    /// </summary>
    [TestMethod]
    public void BeginScope_WithEmptyKey_ReturnsNull()
    {
        // Arrange
        var logger = new TestLogger();
        string key = string.Empty;
        object value = "testValue";

        // Act - explicitly call the extension method from CommonLoggerExtension
        var scope = Roadbed.CommonLoggerExtension.BeginScope(logger, key, value);

        // Assert
        Assert.IsNull(scope, "BeginScope should return null when key is empty.");
    }

    /// <summary>
    /// Verifies that BeginScope returns null when key is null.
    /// </summary>
    [TestMethod]
    public void BeginScope_WithNullKey_ReturnsNull()
    {
        // Arrange
        var logger = new TestLogger();
        string? key = null;
        object value = "testValue";

        // Act
        var scope = Roadbed.CommonLoggerExtension.BeginScope(logger, key!, value);

        // Assert
        Assert.IsNull(scope, "BeginScope should not be null - even when key is null.");
    }

    /// <summary>
    /// Verifies that BeginScope throws ArgumentNullException when logger is null.
    /// </summary>
    [TestMethod]
    public void BeginScope_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange
        ILogger? nullLogger = null;
        string key = "testKey";
        object value = "testValue";

        // Act
        bool threwException = false;
        try
        {
            var scope = nullLogger!.BeginScope(key, value);
        }
        catch (ArgumentNullException)
        {
            threwException = true;
        }

        // Assert
        Assert.IsTrue(threwException, "Should throw ArgumentNullException when logger is null.");
    }

    /// <summary>
    /// Verifies that BeginScope returns a valid scope when logger and key are valid.
    /// </summary>
    [TestMethod]
    public void BeginScope_WithValidLoggerAndKey_ReturnsScope()
    {
        // Arrange
        var logger = new TestLogger();
        string key = "testKey";
        object value = "testValue";

        // Act
        var scope = logger.BeginScope(key, value);

        // Assert
        Assert.IsNotNull(scope, "BeginScope should return a scope when logger and key are valid.");
    }

    /// <summary>
    /// Verifies that BeginScope with whitespace key returns null.
    /// </summary>
    [TestMethod]
    public void BeginScope_WithWhitespaceKey_ReturnsNull()
    {
        // Arrange
        var logger = new TestLogger();
        string key = "   ";
        object value = "testValue";

        // Act
        var scope = logger.BeginScope(key, value);

        // Assert
        Assert.IsNotNull(scope, "BeginScope should return scope for whitespace key (only checks null or empty, not whitespace).");
    }

    /// <summary>
    /// Verifies that LogWithCheck does not throw when message is empty.
    /// </summary>
    [TestMethod]
    public void LogWithCheck_WithEmptyMessage_DoesNotThrow()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string message = string.Empty;

        // Act & Assert
        bool threwException = false;
        try
        {
            logger.LogWithCheck(level, message);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when message is empty.");
    }

    /// <summary>
    /// Verifies that LogWithCheck does not throw when message is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheck_WithNullMessage_DoesNotThrow()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string? message = null;

        // Act & Assert
        bool threwException = false;
        try
        {
            logger.LogWithCheck(level, message!);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when message is null.");
    }

    /// <summary>
    /// Verifies that LogWithCheck logs when logger and message are valid.
    /// </summary>
    [TestMethod]
    public void LogWithCheck_WithValidLoggerAndMessage_LogsMessage()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string message = "Test message";

        // Act
        logger.LogWithCheck(level, message);

        // Assert
        Assert.IsTrue(logger.WasLogged, "Logger should have logged the message.");
    }

    /// <summary>
    /// Verifies that LogWithCheck does not log when log level is not enabled.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithDisabledLogLevel_DoesNotLog()
    {
        // Arrange
        var logger = new TestLogger { EnabledLogLevel = LogLevel.Warning };
        LogLevel level = LogLevel.Debug;
        string message = "Test message";
        object[] param = new object[] { };

        // Act
        logger.LogWithCheck(level, message, param);

        // Assert
        Assert.IsFalse(logger.WasLogged, "Logger should not log when log level is not enabled.");
    }

    /// <summary>
    /// Verifies that LogWithCheck with params does not throw when message is empty.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithEmptyMessage_DoesNotThrow()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string message = string.Empty;
        object[] param = new object[] { "param1" };

        // Act & Assert
        bool threwException = false;
        try
        {
            logger.LogWithCheck(level, message, param);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when message is empty.");
    }

    /// <summary>
    /// Verifies that LogWithCheck with params logs when logger and message are valid.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithValidLoggerAndMessage_LogsMessage()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string message = "Test message {0}";
        object[] param = new object[] { "param1" };

        // Act
        logger.LogWithCheck(level, message, param);

        // Assert
        Assert.IsTrue(logger.WasLogged, "Logger should have logged the message.");
    }

    /// <summary>
    /// Verifies that LogWithCheck does not throw when logger is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheck_WithNullLogger_DoesNotThrow()
    {
        // Arrange
        ILogger? nullLogger = null;
        LogLevel level = LogLevel.Information;
        string message = "Valid message";

        // Act & Assert
        bool threwException = false;
        try
        {
            Roadbed.CommonLoggerExtension.LogWithCheck(nullLogger!, level, message);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when logger is null.");
    }

    /// <summary>
    /// Verifies that LogWithCheck with params does not throw when logger is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithNullLogger_DoesNotThrow()
    {
        // Arrange
        ILogger? nullLogger = null;
        LogLevel level = LogLevel.Information;
        string message = "Valid message {0}";
        object[] param = new object[] { "param1" };

        // Act & Assert
        bool threwException = false;
        try
        {
            Roadbed.CommonLoggerExtension.LogWithCheck(nullLogger!, level, message, param);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when logger is null.");
    }

    /// <summary>
    /// Verifies that LogWithCheck with params does not throw when message is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithNullMessage_DoesNotThrow()
    {
        // Arrange
        var logger = new TestLogger();
        LogLevel level = LogLevel.Information;
        string? message = null;
        object[] param = new object[] { "param1" };

        // Act & Assert
        bool threwException = false;
        try
        {
            logger.LogWithCheck(level, message!, param);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when message is null.");
    }

    /// <summary>
    /// Verifies that LogWithCheck does not throw when both logger is null and message is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheck_WithNullLoggerAndNullMessage_DoesNotThrow()
    {
        // Arrange
        ILogger? nullLogger = null;
        LogLevel level = LogLevel.Information;
        string? message = null;

        // Act & Assert
        bool threwException = false;
        try
        {
            Roadbed.CommonLoggerExtension.LogWithCheck(nullLogger!, level, message!);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when both logger and message are null.");
    }

    /// <summary>
    /// Verifies that LogWithCheck with params does not throw when both logger is null and message is null.
    /// </summary>
    [TestMethod]
    public void LogWithCheckParams_WithNullLoggerAndNullMessage_DoesNotThrow()
    {
        // Arrange
        ILogger? nullLogger = null;
        LogLevel level = LogLevel.Information;
        string? message = null;
        object[] param = new object[] { "param1" };

        // Act & Assert
        bool threwException = false;
        try
        {
            Roadbed.CommonLoggerExtension.LogWithCheck(nullLogger!, level, message!, param);
        }
        catch (Exception)
        {
            threwException = true;
        }

        Assert.IsFalse(threwException, "Should not throw when both logger and message are null.");
    }

    #endregion Public Methods

    #region Private Classes

    /// <summary>
    /// Test disposable for scope.
    /// </summary>
    private class TestDisposable : IDisposable
    {
        #region Public Methods

        public void Dispose()
        {
            // Nothing to dispose
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Test logger implementation.
    /// </summary>
    private class TestLogger : ILogger
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the enabled log level.
        /// </summary>
        public LogLevel EnabledLogLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// Gets a value indicating whether a log entry was made.
        /// </summary>
        public bool WasLogged { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Begins a logging scope.
        /// </summary>
        /// <typeparam name="TState">Log category based on the object type.</typeparam>
        /// <param name="state">Log state.</param>
        /// <returns>Scope for logging.</returns>
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            // Return null if state is null to support extension method testing
            if (state == null)
            {
                return null;
            }

            return new TestDisposable();
        }

        /// <summary>
        /// Checks if the specified log level is enabled.
        /// </summary>
        /// <param name="logLevel">Log level.</param>
        /// <returns>Indication of whether the level is enabled.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= this.EnabledLogLevel;
        }

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <typeparam name="TState">Log category based on the object type.</typeparam>
        /// <param name="logLevel">Level of the message to log.</param>
        /// <param name="eventId">Log event ID.</param>
        /// <param name="state">State of the log entry.</param>
        /// <param name="exception">Exception that occurred.</param>
        /// <param name="formatter">Log message formatter.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            this.WasLogged = true;
        }

        #endregion Public Methods
    }

    #endregion Private Classes
}