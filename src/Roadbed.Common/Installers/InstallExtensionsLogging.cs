namespace Roadbed.Common.Installers;

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Installer for Extensions Logging services.
/// </summary>
public class InstallExtensionsLogging : IServiceCollectionInstaller
{
    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add logging services to the IServiceCollection
        services.AddLogging();

        // Build the service provider
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // Manually get the ILoggerFactory instance
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        // Register the ILoggerFactory instance in Dependency Injection
        services.AddSingleton<ILoggerFactory>(loggerFactory);

        // Retain Serivce Collection in custom ServiceLocator
        ServiceLocator.SetLocatorProvider(serviceProvider);
    }
}