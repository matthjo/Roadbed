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
    #region Private Fields

    /// <summary>
    /// Type of interface used to install modules.
    /// </summary>
    private static readonly Type InterfaceType = typeof(IServiceCollectionInstaller);

    #endregion Private Fields

    #region Public Methods

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
        InvokeInstallers(typeof(T).Assembly, services, configuration);
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
        var visited = new HashSet<string>();
        var queue = new Queue<Assembly>();

        // Start with all currently loaded assemblies in the AppDomain
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !string.IsNullOrEmpty(a.FullName) &&
                        !a.FullName.StartsWith("System.") &&
                        !a.FullName.StartsWith("Microsoft."));

        foreach (var assembly in loadedAssemblies)
        {
            queue.Enqueue(assembly);
        }

        // Also include the entry assembly if it exists
        var rootAssembly = Assembly.GetEntryAssembly();
        if (rootAssembly != null)
        {
            queue.Enqueue(rootAssembly);
        }

        // Loop through Assemblies until empty
        while (queue.Any())
        {
            // Grab one from queue
            Assembly assembly = queue.Dequeue();

            if ((assembly == null) || string.IsNullOrEmpty(assembly.FullName))
            {
                continue;
            }

            // Skip if already visited
            if (visited.Contains(assembly.FullName))
            {
                continue;
            }

            // Mark as visited
            visited.Add(assembly.FullName);

            // Process this assembly for installers
            InvokeInstallers(assembly, services, configuration);

            // Get referenced assemblies
            try
            {
                AssemblyName[] references = assembly.GetReferencedAssemblies();

                foreach (var reference in references)
                {
                    // Skip Microsoft/System assemblies
                    if (reference.FullName.StartsWith("System.") ||
                        reference.FullName.StartsWith("Microsoft."))
                    {
                        continue;
                    }

                    // Skip if already visited
                    if (visited.Contains(reference.FullName))
                    {
                        continue;
                    }

                    Assembly loadedAssembly = Assembly.Load(reference);
                    queue.Enqueue(loadedAssembly);
                }
            }
            catch (Exception)
            {
                // Failed to get referenced assemblies, continue with next assembly
            }
        }
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Locates instances of a specific Type.
    /// </summary>
    /// <param name="assembly">Assembly to scan.</param>
    /// <returns>List of classes that have implemented an interface.</returns>
    private static IList<IServiceCollectionInstaller> GetImplementations(Assembly assembly)
    {
        // Get public classes that implement from IServiceCollectionInstaller
        var installerInstances = assembly.GetExportedTypes()
                                        .Where(x => InterfaceType.IsAssignableFrom(x) &&
                                                    x is { IsAbstract: false, IsInterface: false })
                                        .Select(Activator.CreateInstance)
                                        .Cast<IServiceCollectionInstaller>()
                                        .ToList();

        return installerInstances;
    }

    /// <summary>
    /// Invokes instances of IServiceCollectionInstaller.
    /// </summary>
    /// <param name="assembly">Assembly to scan for instances of IServiceCollectionInstaller.</param>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    private static void InvokeInstallers(Assembly assembly, IServiceCollection services, IConfiguration configuration)
    {
        // Locate instances of IServiceCollectionInstaller
        var implementations = GetImplementations(assembly);

        foreach (var implemenation in implementations)
        {
            implemenation.ConfigureServices(services, configuration);
        }
    }

    #endregion Private Methods
}