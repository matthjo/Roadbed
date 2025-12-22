namespace Roadbed.Test.Unit;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;
using Roadbed.Test.Unit.Crud.Mocks;

/// <summary>
/// Contains unit tests for verifying the behavior of the CommonClassWithLogging class.
/// </summary>
[TestClass]
public class CommonClassWithLoggingTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that this.LoggerFactory is null when initialized using the no-parameter constructor.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_InitializeNoParameterConstructor_NullProperties()
    {
        // Arrange (Given)
        var entity = new UnitSimpleEntity<CommonClassWithLoggingTests>();

        // Act (When)
        var result = entity.LoggerFactory;

        // Assert (Then)
        Assert.AreEqual(
            NullLoggerFactory.Instance,
            result,
            "LoggerFactory property should be null.");
    }

    /// <summary>
    /// Unit test to verify that LogDebug ignored the message as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogDebugDisabled_MessageNotLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogCollectorOptions options = new FakeLogCollectorOptions();
        IOptions<FakeLogCollectorOptions> optionsAccessor = Options.Create(options);
        FakeLogCollector collector = new FakeLogCollector(optionsAccessor);
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>(collector);
        fakeLogger.ControlLevel(LogLevel.Debug, enabled: false);
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Message that should be ignored";

        // Act (When)
        entity.LogDebug(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(0, snapshot);
    }

    /// <summary>
    /// Unit test to verify that LogDebug worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogDebugNoParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";

        // Act (When)
        entity.LogDebug(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Debug, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogDebug with Params worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogDebugWithParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var dataPoint = "John Doe";
        var expectedMessage = "Something about {dataPoint}";

        // Act (When)
        entity.LogDebug(expectedMessage, dataPoint);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(dataPoint, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Debug, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogError worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogErrorNoParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";

        // Act (When)
        entity.LogError(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Error, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogError with Params worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogErrorWithParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var dataPoint = "John Doe";
        var expectedMessage = "Something about {dataPoint}";

        // Act (When)
        entity.LogError(expectedMessage, dataPoint);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(dataPoint, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Error, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogError With Scope worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogErrorWithScope_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";
        var expectedScopeKey = "DataPoint";
        var expectedScopeValue = "John Doe";

        // Act (When)
        entity.BeginScope(expectedScopeKey, expectedScopeValue);
        entity.LogError(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Error, snapshot[0].Level);

        IReadOnlyList<object?> scopes = snapshot[0].Scopes;

        Assert.HasCount(1, scopes);
    }

    /// <summary>
    /// Unit test to verify that Scope is not added when the Key is blank.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogErrorWithScope_KeyMissingAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";
        var expectedScopeKey = string.Empty;
        var expectedScopeValue = "John Doe";

        // Act (When)
        entity.BeginScope(expectedScopeKey, expectedScopeValue);
        entity.LogError(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Error, snapshot[0].Level);

        IReadOnlyList<object?> scopes = snapshot[0].Scopes;

        Assert.HasCount(0, scopes);
    }

    /// <summary>
    /// Unit test to verify that LogInformation ignored the message as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogInformationDisabled_MessageNotLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogCollectorOptions options = new FakeLogCollectorOptions();
        IOptions<FakeLogCollectorOptions> optionsAccessor = Options.Create(options);
        FakeLogCollector collector = new FakeLogCollector(optionsAccessor);
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>(collector);
        fakeLogger.ControlLevel(LogLevel.Information, enabled: false);
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Message that should be ignored";

        // Act (When)
        entity.LogInformation(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(0, snapshot);
    }

    /// <summary>
    /// Unit test to verify that LogInformation worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogInformationNoParams_BlankMessageNotLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = string.Empty;

        // Act (When)
        entity.LogInformation(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(0, snapshot);
    }

    /// <summary>
    /// Unit test to verify that LogInformation worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogInformationNoParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";

        // Act (When)
        entity.LogInformation(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Information, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogInformation with Params worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogInformationWithParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var dataPoint = "John Doe";
        var expectedMessage = "Something about {dataPoint}";

        // Act (When)
        entity.LogInformation(expectedMessage, dataPoint);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(dataPoint, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Information, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogTrace worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogTraceNoParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";

        // Act (When)
        entity.LogTrace(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Trace, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogTrace with Params worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogTraceWithParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var dataPoint = "John Doe";
        var expectedMessage = "Something about {dataPoint}";

        // Act (When)
        entity.LogTrace(expectedMessage, dataPoint);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(dataPoint, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Trace, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogWarning worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogWarningNoParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var expectedMessage = "Testing log entry write.";

        // Act (When)
        entity.LogWarning(expectedMessage);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(expectedMessage, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Warning, snapshot[0].Level);
    }

    /// <summary>
    /// Unit test to verify that LogWarning with Params worked as intended.
    /// </summary>
    [TestMethod]
    public void CommonClassWithLogging_LogWarningWithParams_MessageLoggedAsExpected()
    {
        // Arrange (Given)
        FakeLogger fakeLogger = new FakeLogger<CommonClassWithLoggingTests>();
        UnitSimpleEntity<CommonClassWithLoggingTests> entity = new UnitSimpleEntity<CommonClassWithLoggingTests>(fakeLogger);

        var dataPoint = "John Doe";
        var expectedMessage = "Something about {dataPoint}";

        // Act (When)
        entity.LogWarning(expectedMessage, dataPoint);

        // Assert (Then)
        Assert.IsNotNull(fakeLogger);
        Assert.IsNotNull(fakeLogger.Collector);

        IReadOnlyList<FakeLogRecord> snapshot = fakeLogger.Collector.GetSnapshot();

        Assert.HasCount(1, snapshot);
        Assert.Contains(dataPoint, snapshot[0].Message);
        Assert.AreEqual(LogLevel.Warning, snapshot[0].Level);
    }

    #endregion Public Methods
}