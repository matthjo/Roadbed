namespace Roadbed.Test.Unit.Common;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed;

/// <summary>
/// Contains unit tests for verifying the behavior of the BaseClassWithLogging class constructors.
/// </summary>
[TestClass]
public class BaseClassWithLoggingTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the ILogger constructor handles incompatible logger type.
    /// </summary>
    [TestMethod]
    public void Constructor_IncompatibleILoggerType_InitializesWithNullLogger()
    {
        // Arrange (Given)
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var incompatibleLogger = loggerFactory.CreateLogger<string>();

        // Act (When)
        var testInstance = new TestClass(incompatibleLogger);

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
        Assert.IsInstanceOfType(
            testInstance.Logger,
            typeof(NullLogger<TestClass>),
            "Logger should be an instance of NullLogger<TestClass> when incompatible logger type is provided.");
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.IsInstanceOfType(
            testInstance.LoggerFactory,
            typeof(NullLoggerFactory),
            "LoggerFactory should be an instance of NullLoggerFactory.");
    }

    /// <summary>
    /// Unit test to verify that the parameterless constructor initializes with NullLogger.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithNullLogger()
    {
        // Arrange (Given)

        // Act (When)
        var testInstance = new TestClass();

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
        Assert.IsInstanceOfType(
            testInstance.Logger,
            typeof(NullLogger<TestClass>),
            "Logger should be an instance of NullLogger<TestClass>.");
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.IsInstanceOfType(
            testInstance.LoggerFactory,
            typeof(NullLoggerFactory),
            "LoggerFactory should be an instance of NullLoggerFactory.");
    }

    /// <summary>
    /// Unit test to verify that the ILogger constructor handles null logger parameter.
    /// </summary>
    [TestMethod]
    public void Constructor_NullILogger_InitializesWithNullLogger()
    {
        // Arrange (Given)
        ILogger? nullLogger = null;

        // Act (When)
        var testInstance = new TestClass(nullLogger!);

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
        Assert.IsInstanceOfType(
            testInstance.Logger,
            typeof(NullLogger<TestClass>),
            "Logger should be an instance of NullLogger<TestClass> when null is provided.");
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.IsInstanceOfType(
            testInstance.LoggerFactory,
            typeof(NullLoggerFactory),
            "LoggerFactory should be an instance of NullLoggerFactory.");
    }

    /// <summary>
    /// Unit test to verify that the ILoggerFactory constructor handles null factory parameter.
    /// </summary>
    [TestMethod]
    public void Constructor_NullILoggerFactory_InitializesWithNullLoggerFactory()
    {
        // Arrange (Given)
        ILoggerFactory? nullLoggerFactory = null;

        // Act (When)
        var testInstance = new TestClass(nullLoggerFactory!);

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.IsInstanceOfType(
            testInstance.LoggerFactory,
            typeof(NullLoggerFactory),
            "LoggerFactory should be an instance of NullLoggerFactory when null is provided.");
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
    }

    /// <summary>
    /// Unit test to verify that the ILogger constructor initializes with the provided logger.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidILogger_InitializesWithProvidedLogger()
    {
        // Arrange (Given)
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<TestClass>();

        // Act (When)
        var testInstance = new TestClass(logger);

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
        Assert.AreSame(
            logger,
            testInstance.Logger,
            "Logger should be the same instance that was provided.");
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.IsInstanceOfType(
            testInstance.LoggerFactory,
            typeof(NullLoggerFactory),
            "LoggerFactory should be an instance of NullLoggerFactory when only logger is provided.");
    }

    /// <summary>
    /// Unit test to verify that the ILoggerFactory constructor initializes with the provided factory.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidILoggerFactory_InitializesWithProvidedFactory()
    {
        // Arrange (Given)
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        // Act (When)
        var testInstance = new TestClass(loggerFactory);

        // Assert (Then)
        Assert.IsNotNull(
            testInstance.LoggerFactory,
            "LoggerFactory should not be null.");
        Assert.AreSame(
            loggerFactory,
            testInstance.LoggerFactory,
            "LoggerFactory should be the same instance that was provided.");
        Assert.IsNotNull(
            testInstance.Logger,
            "Logger should not be null.");
        Assert.IsNotInstanceOfType(
            testInstance.Logger,
            typeof(NullLogger<TestClass>),
            "Logger should not be NullLogger when valid LoggerFactory is provided.");
    }

    #endregion Public Methods

    #region Private Classes

    /// <summary>
    /// Test implementation of BaseClassWithLogging for testing purposes.
    /// </summary>
    private class TestClass : BaseClassWithLogging<TestClass>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        public TestClass()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        public TestClass(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="loggerFactory">Represents a type used to configure the logging system.</param>
        public TestClass(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        #endregion Public Constructors
    }

    #endregion Private Classes
}