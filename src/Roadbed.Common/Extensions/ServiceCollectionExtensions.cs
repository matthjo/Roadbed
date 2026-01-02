/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

using System;
using System.Reflection;
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

    /// <summary>
    /// Installs services from the specified assembly that implement <see cref="IServiceCollectionInstaller"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <remarks>
    /// This method comes from the Installer.Microsoft.ServiceCollection NuGet package. You can find the original
    /// source code at: https://github.com/thisisnabi/Installer.Microsoft.ServiceCollection.
    /// </remarks>
    public static void InstallModulesInAppDomain(this IServiceCollection services, IConfiguration configuration)
    {
        var implementations = GetImplementations<IServiceCollectionInstaller>();

        foreach (var type in implementations)
        {
            if (Activator.CreateInstance(type) is IServiceCollectionInstaller instance)
            {
                instance.ConfigureServices(services, configuration);
            }
        }
    }

    /// <summary>
    /// Locates instances of a specific Type.
    /// </summary>
    /// <typeparam name="TInterface">Object type to locate.</typeparam>
    /// <returns>List of classes that have implemented an interface.</returns>
    private static IEnumerable<Type> GetImplementations<TInterface>()
    {
        // Get the Type object for the target interface
        Type interfaceType = typeof(TInterface);

        // Iterate through all loaded assemblies in the current AppDomain
        var implementations = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try
                {
                    // Attempt to get all types from the assembly
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException)
                {
                    // Handle cases where an assembly might not load all its types
                    // (e.g., missing dependencies). Just ignore the problem types
                    // for this search.
                    return new Type[0];
                }
            })
            .Where(type =>

                // Ensure the type is a class and not abstract
                type.IsClass && !type.IsAbstract &&

                // Check if the type implements the interface
                interfaceType.IsAssignableFrom(type));

        return implementations;
    }
}