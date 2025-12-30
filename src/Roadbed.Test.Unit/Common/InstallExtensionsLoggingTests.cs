namespace Roadbed.Test.Unit.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Common.Installers;

/// <summary>
/// Unit tests for the InstallExtensionsLogging class.
/// </summary>
[TestClass]
public class InstallExtensionsLoggingTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that ConfigureServices adds logging services to the service collection.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_AddsLoggingServices_Successfully()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallExtensionsLogging();

        // Act
        installer.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        // Assert
        Assert.IsNotNull(loggerFactory, "ILoggerFactory should be registered in the service collection.");
    }

    #endregion Public Methods
}