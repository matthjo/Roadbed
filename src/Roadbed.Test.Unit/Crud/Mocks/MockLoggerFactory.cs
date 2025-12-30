namespace Roadbed.Test.Unit.Crud.Mocks;

using Microsoft.Extensions.Logging;

/// <summary>
/// Mock logger factory for testing.
/// </summary>
public class MockLoggerFactory : ILoggerFactory
{
    #region Public Methods

    /// <inheritdoc/>
    public void AddProvider(ILoggerProvider provider)
    {
    }

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName) => new MockLogger();

    /// <inheritdoc/>
    public void Dispose()
    {
    }

    #endregion Public Methods
}