namespace Roadbed.Test.Unit.Crud.Mocks;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

/// <summary>
/// Mock logger for testing.
/// </summary>
public class MockLogger : ILogger
{
    #region Public Properties

    /// <summary>
    /// Gets the logged messages.
    /// </summary>
    public List<string> LoggedMessages { get; } = new List<string>();

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Starts a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">Log state type.</typeparam>
    /// <param name="state">State of the log message.</param>
    /// <returns>Scope for the mock logger.</returns>
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return default!;
    }

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <inheritdoc/>
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
#pragma warning disable CS8633 // Nullability in constraints for type parameter doesn't match the constraints for type parameter in implicitly implemented interface method'.

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
       where TState : notnull
#pragma warning restore CS8633 // Nullability in constraints for type parameter doesn't match the constraints for type parameter in implicitly implemented interface method'.
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    {
        this.LoggedMessages.Add(formatter(state, exception));
    }

    #endregion Public Methods
}