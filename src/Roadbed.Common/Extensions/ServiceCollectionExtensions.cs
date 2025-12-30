/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for common Service Collection operations.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Installs services from the specified assembly that implement <see cref="IServiceCollectionInstaller"/>.
    /// </summary>
    /// <typeparam name="T">Assembly to scan for implemenations of <see cref="IServiceCollectionInstaller"/>.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <remarks>
    /// This method comes from the Installer.Microsoft.ServiceCollection NuGet package. You can find the original
    /// source code at: https://github.com/thisisnabi/Installer.Microsoft.ServiceCollection.
    /// </remarks>
    public static void InstallFromAssembly<T>(this IServiceCollection services, IConfiguration configuration)
    {
        // Get all public classes that implement from IInstaller
        var installerItems = typeof(T).Assembly
                                    .GetExportedTypes()
                                        .Where(x => typeof(IServiceCollectionInstaller).IsAssignableFrom(x) &&
                                                    x is { IsAbstract: false, IsInterface: false })
                                        .Select(Activator.CreateInstance)
                                        .Cast<IServiceCollectionInstaller>()
                                        .ToList();

        // Configure ServiceCollection for all founded classes
        foreach (var installer in installerItems)
        {
            installer.ConfigureServices(services, configuration);
        }
    }
}
