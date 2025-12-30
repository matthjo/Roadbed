namespace Roadbed.Net.Installers;

using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Installer for Net HTTP Client services.
/// </summary>
public class InstallNetHttpClient : IServiceCollectionInstaller
{
    #region Public Methods

    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Configure default HTTP client without compression
        services.AddHttpClient("DefaultClient")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.None,
            });

        // Configure HTTP client with compression enabled
        services.AddHttpClient("CompressedClient")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
            });

        // Build the service provider
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // Manually get the ILoggerFactory instance
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        // Register the ILoggerFactory instance in Dependency Injection
        services.AddSingleton<ILoggerFactory>(loggerFactory);

        // Retain Serivce Collection in custom ServiceLocator
        ServiceLocator.SetLocatorProvider(serviceProvider);
    }

    #endregion Public Methods
}