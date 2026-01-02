/*
 * The namespace Roadbed.Common was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Service Provider collection from the <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// WARNING: Service Locator is an anti-pattern. You should use constructor injection where possible.
/// </remarks>
public static class ServiceLocator
{
    #region Private Fields

    private static readonly Lock LockObject = new Lock();
    private static bool isInitialized = false;
    private static IServiceProvider? serviceProviderCollection;

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Gets the service of type T from <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">Service to retrieve.</typeparam>
    /// <returns>Service from <see cref="IServiceProvider"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when ServiceLocator has not been initialized.</exception>
    public static T GetService<T>()
        where T : notnull
    {
        lock (LockObject)
        {
            if (!isInitialized || serviceProviderCollection == null)
            {
                return default!;
            }

            return serviceProviderCollection.GetRequiredService<T>();
        }
    }

    /// <summary>
    /// Utilized in instances of <see cref="IServiceCollectionInstaller"/> to retain the service provider.
    /// </summary>
    /// <param name="serviceProvider">Defines a mechanism for retrieving a service object.</param>
    /// <exception cref="ArgumentNullException">Thrown when serviceProvider is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when SetLocatorProvider has already been called.</exception>
    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        lock (LockObject)
        {
            serviceProviderCollection = serviceProvider;
            isInitialized = true;
        }
    }

    /// <summary>
    /// Attempts to get the service of type T from <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">Service to retrieve.</typeparam>
    /// <returns>Service from <see cref="IServiceProvider"/>, or default if not found or not initialized.</returns>
    public static T? TryGetService<T>()
        where T : class
    {
        lock (LockObject)
        {
            if (isInitialized && serviceProviderCollection != null)
            {
                return serviceProviderCollection.GetService<T>();
            }

            return null;
        }
    }

    #endregion Public Methods

    #region Internal Methods

    /// <summary>
    /// Resets the ServiceLocator. Primarily for testing purposes.
    /// </summary>
    internal static void Reset()
    {
        lock (LockObject)
        {
            serviceProviderCollection = null;
            isInitialized = false;
        }
    }

    #endregion Internal Methods
}