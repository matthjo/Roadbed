namespace Roadbed.Test.Unit.Net.Installers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;
using Roadbed.Net.Installers;
using System.Net.Http;

/// <summary>
/// Contains unit tests for verifying the behavior of the InstallNetHttpClient class.
/// </summary>
[TestClass]
public class InstallNetHttpClientTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that ConfigureServices configures CompressedClient.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_ConfiguresCompressedClient_Successfully()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Act (When)
        installer.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var compressedClient = httpClientFactory.CreateClient("CompressedClient");

        // Assert (Then)
        Assert.IsNotNull(
            compressedClient,
            "CompressedClient should be created successfully.");
    }

    /// <summary>
    /// Unit test to verify that ConfigureServices configures DefaultClient.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_ConfiguresDefaultClient_Successfully()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Act (When)
        installer.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var defaultClient = httpClientFactory.CreateClient("DefaultClient");

        // Assert (Then)
        Assert.IsNotNull(
            defaultClient,
            "DefaultClient should be created successfully.");
    }

    /// <summary>
    /// Unit test to verify that ConfigureServices can create named clients.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_CreatesNamedClients_BothClientsAreDistinct()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Act (When)
        installer.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var defaultClient = httpClientFactory.CreateClient("DefaultClient");
        var compressedClient = httpClientFactory.CreateClient("CompressedClient");

        // Assert (Then)
        Assert.IsNotNull(
            defaultClient,
            "DefaultClient should be created.");
        Assert.IsNotNull(
            compressedClient,
            "CompressedClient should be created.");
        Assert.AreNotSame(
            defaultClient,
            compressedClient,
            "Named clients should be distinct instances.");
    }

    /// <summary>
    /// Unit test to verify that ConfigureServices registers IHttpClientFactory.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_RegistersIHttpClientFactory_Successfully()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Act (When)
        installer.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

        // Assert (Then)
        Assert.IsNotNull(
            httpClientFactory,
            "IHttpClientFactory should be registered in the service collection.");
    }

    #endregion Public Methods
}