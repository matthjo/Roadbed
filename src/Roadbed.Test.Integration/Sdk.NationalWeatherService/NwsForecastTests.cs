namespace Roadbed.Test.Integration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roadbed.Common;
using Roadbed.Messaging;
using Roadbed.Net.Installers;
using Roadbed.Sdk.NationalWeatherService;
using Roadbed.Sdk.NationalWeatherService.Dtos;

/// <summary>
/// Contains unit tests for verifying the behavior of the NwsForecast class.
/// </summary>
[TestClass]
public class NwsForecastTests
{
    #region Private Fields

    private static readonly MessagingMessageRequest<CommonKeyValuePair<string, string>> MessagingRequest =
                new MessagingMessageRequest<CommonKeyValuePair<string, string>>(
                    new MessagingPublisher("SDK Integration Test"),
                    "UNIT_TEST_V1");

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NwsForecastTests"/> class.
    /// </summary>
    public NwsForecastTests()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Install HTTP Client Factory
        installer.ConfigureServices(services, configuration);
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets object used to store information that is provided to unit tests.
    /// </summary>
    public TestContext TestContext { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Unit test to verify that IBaseForecastRepository retrieves data correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task NwsForecast_BusinessLogic_ValidNwsLocationRequest()
    {
        // Arrange (Given)
        NwsForecast repository = new NwsForecast(MessagingRequest);

        // Act (When)
        var forecastEntity = await repository.ListAsync(
            new NwsPhysicalAddress(41.265630, -95.922312),
            this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            forecastEntity,
            "No data was returned.");
    }

    #endregion Public Methods
}