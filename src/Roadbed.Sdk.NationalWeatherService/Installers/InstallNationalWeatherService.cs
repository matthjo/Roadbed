namespace Roadbed.Sdk.NationalWeatherService.Installers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roadbed.Sdk.NationalWeatherService.Repositories;

/// <summary>
/// Installer for National Weather Service SDK.
/// </summary>
public class InstallNationalWeatherService : IServiceCollectionInstaller
{
    #region Public Methods

    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add Internal Repositories
        services.AddScoped<INwsLocationRepository<NwsLocationResponse, string>, NwsLocationRepository>();
        services.AddScoped<INwsForecastDailyRepository<NwsForecastResponse, string>, NwsForecastDailyRepository>();
        services.AddScoped<INwsForecastHourlyRepository<NwsForecastResponse, string>, NwsForecastHourlyRepository>();

        // Build the service provider
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // Retain Serivce Collection in custom ServiceLocator
        ServiceLocator.SetLocatorProvider(serviceProvider);
    }

    #endregion Public Methods
}