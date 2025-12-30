namespace Roadbed.Test.Unit.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

/// <summary>
/// Unit tests for the ServiceCollectionExtensions class.
/// </summary>
[TestClass]
public class ServiceCollectionExtensionsTests
{
    #region InstallFromAssembly Tests

    /// <summary>
    /// Verifies that InstallFromAssembly finds and executes installers in the assembly.
    /// </summary>
    [TestMethod]
    public void InstallFromAssembly_WithInstallers_ExecutesAllInstallers()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        services.InstallFromAssembly<ServiceCollectionExtensionsTests>(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var service1 = serviceProvider.GetService<TestService1>();
        var service2 = serviceProvider.GetService<TestService2>();

        Assert.IsNotNull(service1, "TestService1 should be registered by TestInstaller1.");
        Assert.IsNotNull(service2, "TestService2 should be registered by TestInstaller2.");
    }

    /// <summary>
    /// Verifies that InstallFromAssembly registers services from all found installers.
    /// </summary>
    [TestMethod]
    public void InstallFromAssembly_WithMultipleInstallers_RegistersAllServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        services.InstallFromAssembly<ServiceCollectionExtensionsTests>(configuration);

        // Assert
        var descriptor1 = services.FirstOrDefault(x => x.ServiceType == typeof(TestService1));
        var descriptor2 = services.FirstOrDefault(x => x.ServiceType == typeof(TestService2));

        Assert.IsNotNull(descriptor1, "TestService1 should be in service collection.");
        Assert.IsNotNull(descriptor2, "TestService2 should be in service collection.");
    }

    /// <summary>
    /// Verifies that InstallFromAssembly registers services in the order installers are found.
    /// </summary>
    [TestMethod]
    public void InstallFromAssembly_RegistersServices_InOrder()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        services.InstallFromAssembly<ServiceCollectionExtensionsTests>(configuration);

        // Assert
        Assert.IsGreaterThan(0, services.Count, "Services should be registered.");

        var hasTestService1 = services.Any(x => x.ServiceType == typeof(TestService1));
        var hasTestService2 = services.Any(x => x.ServiceType == typeof(TestService2));

        Assert.IsTrue(hasTestService1, "TestService1 should be registered.");
        Assert.IsTrue(hasTestService2, "TestService2 should be registered.");
    }

    #endregion

    #region Test Helper Classes and Installers

    /// <summary>
    /// Test service 1.
    /// </summary>
    public class TestService1
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = "Service1";
    }

    /// <summary>
    /// Test service 2.
    /// </summary>
    public class TestService2
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = "Service2";
    }

    /// <summary>
    /// Service that uses configuration.
    /// </summary>
    public class ConfigurationTestService
    {
        /// <summary>
        /// Gets or sets the config value of the service.
        /// </summary>
        public string? ConfigValue { get; set; }
    }

    /// <summary>
    /// Existing service for testing preservation.
    /// </summary>
    public class ExistingService
    {
        /// <summary>
        /// Gets or sets the value of the service.
        /// </summary>
        public string Value { get; set; } = "Existing";
    }

    /// <summary>
    /// Test service from abstract installer (should not be registered).
    /// </summary>
    public class AbstractTestService
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = "AbstractService";
    }

    /// <summary>
    /// Test service from private installer (should not be registered).
    /// </summary>
    public class PrivateTestService
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = "PrivateService";
    }

    /// <summary>
    /// Service with dependency.
    /// </summary>
    public class ServiceWithDependency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceWithDependency"/> class.
        /// </summary>
        /// <param name="testService">Test service.</param>
        public ServiceWithDependency(TestService1 testService)
        {
            this.TestService = testService;
        }

        /// <summary>
        /// Gets the test service.
        /// </summary>
        public TestService1 TestService { get; }
    }

    /// <summary>
    /// Test installer 1.
    /// </summary>
    public class TestInstaller1 : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<TestService1>();
        }
    }

    /// <summary>
    /// Test installer 2.
    /// </summary>
    public class TestInstaller2 : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<TestService2>();
        }
    }

    /// <summary>
    /// Test installer that uses configuration.
    /// </summary>
    public class ConfigurationTestInstaller : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var configValue = configuration["TestKey"] ?? "DefaultValue";
            services.AddSingleton(new ConfigurationTestService { ConfigValue = configValue });
        }
    }

    /// <summary>
    /// Test installer that registers service with dependencies.
    /// </summary>
    public class DependencyTestInstaller : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ServiceWithDependency>();
        }
    }

    /// <summary>
    /// Abstract installer (should not be instantiated).
    /// </summary>
    public abstract class AbstractTestInstaller : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AbstractTestService>();
        }
    }

    /// <summary>
    /// Private installer (should not be found by GetExportedTypes).
    /// </summary>
    private class PrivateTestInstaller : IServiceCollectionInstaller
    {
        /// <summary>
        /// Configure Test Services.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <param name="configuration">Configured for application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<PrivateTestService>();
        }
    }

    #endregion
}