namespace Roadbed.Test.Unit.Common;

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Unit tests for the ServiceLocator class.
/// </summary>
[TestClass]
public class ServiceLocatorTests
{
    #region Private Fields

    // Use a lock to prevent tests from running concurrently
    private static readonly object TestLock = new object();

    #endregion Private Fields

    #region Public Methods

    /// <summary>
    /// Cleanup method that runs after each test to reset ServiceLocator state.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        try
        {
            ServiceLocator.Reset();
        }
        finally
        {
            Monitor.Exit(TestLock);
        }
    }

    /// <summary>
    /// Verifies that GetService returns the correct service when initialized.
    /// </summary>
    [TestMethod]
    public void GetService_WhenInitialized_ReturnsCorrectService()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service = ServiceLocator.GetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service, "GetService should return a non-null service.");
        Assert.IsInstanceOfType(service, typeof(ServiceLocatorTestService1), "Service should be of correct type.");
    }

    /// <summary>
    /// Verifies that GetService works with multiple different service types.
    /// </summary>
    [TestMethod]
    public void GetService_WithMultipleServiceTypes_ReturnsCorrectServices()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s =>
        {
            s.AddSingleton<ServiceLocatorTestService1>();
            s.AddSingleton<ServiceLocatorTestService2>();
        });
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service1 = ServiceLocator.GetService<ServiceLocatorTestService1>();
        var service2 = ServiceLocator.GetService<ServiceLocatorTestService2>();

        // Assert
        Assert.IsNotNull(service1, "First service should not be null.");
        Assert.IsNotNull(service2, "Second service should not be null.");
        Assert.IsInstanceOfType(service1, typeof(ServiceLocatorTestService1));
        Assert.IsInstanceOfType(service2, typeof(ServiceLocatorTestService2));
    }

    /// <summary>
    /// Verifies that GetService returns singleton instance consistently.
    /// </summary>
    [TestMethod]
    public void GetService_WithSingletonService_ReturnsSameInstance()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service1 = ServiceLocator.GetService<ServiceLocatorTestService1>();
        var service2 = ServiceLocator.GetService<ServiceLocatorTestService1>();

        // Assert
        Assert.AreSame(service1, service2, "GetService should return the same singleton instance.");
    }

    /// <summary>
    /// Verifies that GetService throws when requesting unregistered service.
    /// </summary>
    [TestMethod]
    public void GetService_WithUnregisteredService_ThrowsInvalidOperationException()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider();
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        bool threwException = false;
        try
        {
            var service = ServiceLocator.GetService<ServiceLocatorTestService1>();
        }
        catch (InvalidOperationException)
        {
            threwException = true;
        }

        // Assert
        Assert.IsTrue(threwException, "Should throw InvalidOperationException when service is not registered.");
    }

    /// <summary>
    /// Initialize method that runs before each test.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        Monitor.Enter(TestLock);
        ServiceLocator.Reset();
    }

    /// <summary>
    /// Verifies complete workflow: initialize, retrieve, reset, re-initialize.
    /// </summary>
    [TestMethod]
    public void Integration_CompleteWorkflow_WorksCorrectly()
    {
        // Arrange
        var serviceProvider1 = this.CreateServiceProvider(s =>
            s.AddSingleton(new ServiceLocatorTestService1 { Value = "First" }));
        var serviceProvider2 = this.CreateServiceProvider(s =>
            s.AddSingleton(new ServiceLocatorTestService1 { Value = "Second" }));

        // Act & Assert - First initialization
        ServiceLocator.SetLocatorProvider(serviceProvider1);
        var service1 = ServiceLocator.GetService<ServiceLocatorTestService1>();
        Assert.AreEqual("First", service1.Value, "First service should have correct value.");

        // Reset
        ServiceLocator.Reset();
        var service1AfterReset = ServiceLocator.TryGetService<ServiceLocatorTestService1>();
        Assert.IsNull(service1AfterReset, "Service should not be available after reset.");

        // Second initialization
        ServiceLocator.SetLocatorProvider(serviceProvider2);
        var service2 = ServiceLocator.GetService<ServiceLocatorTestService1>();
        Assert.AreEqual("Second", service2.Value, "Second service should have correct value.");
    }

    /// <summary>
    /// Verifies that ServiceLocator works with complex service dependencies.
    /// </summary>
    [TestMethod]
    public void Integration_WithComplexDependencies_ResolvesCorrectly()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s =>
        {
            s.AddSingleton<ServiceLocatorTestService1>();
            s.AddSingleton<ServiceLocatorTestService2>();
            s.AddSingleton<ServiceLocatorTestServiceWithDependency>();
        });

        // Act
        ServiceLocator.SetLocatorProvider(serviceProvider);
        var serviceWithDep = ServiceLocator.GetService<ServiceLocatorTestServiceWithDependency>();

        // Assert
        Assert.IsNotNull(serviceWithDep, "Service with dependencies should be resolved.");
        Assert.IsNotNull(serviceWithDep.TestService, "Dependency should be injected.");
    }

    /// <summary>
    /// Verifies that Reset allows SetLocatorProvider to be called again.
    /// </summary>
    [TestMethod]
    public void Reset_AllowsSetLocatorProviderToBeCalledAgain_Successfully()
    {
        // Arrange
        var serviceProvider1 = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider1);

        // Act
        ServiceLocator.Reset();
        var serviceProvider2 = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider2);
        var service = ServiceLocator.GetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service, "Service should be retrievable after Reset and re-initialization.");
    }

    /// <summary>
    /// Verifies that Reset clears the service provider.
    /// </summary>
    [TestMethod]
    public void Reset_ClearsServiceProvider_Successfully()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        ServiceLocator.Reset();

        // Assert
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();
        Assert.IsNull(service, "Service should not be retrievable after Reset.");
    }

    /// <summary>
    /// Verifies that Reset can be called when not initialized.
    /// </summary>
    [TestMethod]
    public void Reset_WhenNotInitialized_DoesNotThrow()
    {
        // Act
        bool threwException = false;
        try
        {
            ServiceLocator.Reset();
        }
        catch (Exception)
        {
            threwException = true;
        }

        // Assert
        Assert.IsFalse(threwException, "Reset should not throw when not initialized.");
    }

    /// <summary>
    /// Verifies that SetLocatorProvider sets the service provider successfully.
    /// </summary>
    [TestMethod]
    public void SetLocatorProvider_WithValidServiceProvider_SetsProviderSuccessfully()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());

        // Act
        ServiceLocator.SetLocatorProvider(serviceProvider);
        var service = ServiceLocator.GetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service, "Service should be retrievable after SetLocatorProvider is called.");
    }

    /// <summary>
    /// Verifies that TryGetService does not throw exceptions.
    /// </summary>
    [TestMethod]
    public void TryGetService_DoesNotThrowException_WhenNotInitialized()
    {
        // Act & Assert
        try
        {
            var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();
            Assert.IsNull(service, "TryGetService should return null without throwing.");
        }
        catch (Exception ex)
        {
            Assert.Fail($"TryGetService should not throw an exception: {ex.Message}");
        }
    }

    /// <summary>
    /// Verifies that TryGetService returns the correct service when initialized and registered.
    /// </summary>
    [TestMethod]
    public void TryGetService_WhenInitializedAndRegistered_ReturnsService()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service, "TryGetService should return a non-null service when registered.");
    }

    /// <summary>
    /// Verifies that TryGetService calls GetService on the service provider when initialized.
    /// </summary>
    [TestMethod]
    public void TryGetService_WhenInitializedWithRegisteredService_CallsGetServiceAndReturnsResult()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service, "TryGetService should return the registered service.");
        Assert.IsInstanceOfType(service, typeof(ServiceLocatorTestService1), "Returned service should be of correct type.");
    }

    /// <summary>
    /// Verifies that TryGetService returns null when not initialized.
    /// </summary>
    [TestMethod]
    public void TryGetService_WhenNotInitialized_ReturnsNull()
    {
        // Act
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNull(service, "TryGetService should return null when ServiceLocator is not initialized.");
    }

    /// <summary>
    /// Verifies that TryGetService returns null when service is not registered.
    /// </summary>
    [TestMethod]
    public void TryGetService_WhenServiceNotRegistered_ReturnsNull()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider();
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNull(service, "TryGetService should return null when service is not registered.");
    }

    /// <summary>
    /// Verifies that TryGetService returns null when serviceProviderCollection is null but isInitialized is true.
    /// </summary>
    [TestMethod]
    public void TryGetService_WhenServiceProviderIsNullButInitialized_ReturnsNull()
    {
        // Arrange - Use reflection to set the internal state
        var isInitializedField = typeof(ServiceLocator).GetField(
            "isInitialized",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var serviceProviderField = typeof(ServiceLocator).GetField(
            "serviceProviderCollection",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        isInitializedField?.SetValue(null, true);
        serviceProviderField?.SetValue(null, null);

        // Act
        var service = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNull(service, "TryGetService should return null when serviceProviderCollection is null.");
    }

    /// <summary>
    /// Verifies that TryGetService works with multiple different service types.
    /// </summary>
    [TestMethod]
    public void TryGetService_WithMultipleServiceTypes_ReturnsCorrectServices()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s =>
        {
            s.AddSingleton<ServiceLocatorTestService1>();
            s.AddSingleton<ServiceLocatorTestService2>();
        });
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service1 = ServiceLocator.TryGetService<ServiceLocatorTestService1>();
        var service2 = ServiceLocator.TryGetService<ServiceLocatorTestService2>();

        // Assert
        Assert.IsNotNull(service1, "First service should not be null.");
        Assert.IsNotNull(service2, "Second service should not be null.");
        Assert.IsInstanceOfType(service1, typeof(ServiceLocatorTestService1));
        Assert.IsInstanceOfType(service2, typeof(ServiceLocatorTestService2));
    }

    /// <summary>
    /// Verifies that TryGetService returns singleton instance consistently.
    /// </summary>
    [TestMethod]
    public void TryGetService_WithSingletonService_ReturnsSameInstance()
    {
        // Arrange
        var serviceProvider = this.CreateServiceProvider(s => s.AddSingleton<ServiceLocatorTestService1>());
        ServiceLocator.SetLocatorProvider(serviceProvider);

        // Act
        var service1 = ServiceLocator.TryGetService<ServiceLocatorTestService1>();
        var service2 = ServiceLocator.TryGetService<ServiceLocatorTestService1>();

        // Assert
        Assert.IsNotNull(service1, "First service should not be null.");
        Assert.IsNotNull(service2, "Second service should not be null.");
        Assert.AreSame(service1, service2, "TryGetService should return the same singleton instance.");
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Creates a stable service provider with the specified services.
    /// </summary>
    private IServiceProvider CreateServiceProvider(Action<IServiceCollection>? configure = null)
    {
        var services = new ServiceCollection();
        configure?.Invoke(services);
        return services.BuildServiceProvider();
    }

    #endregion Private Methods

    #region Public Classes

    /// <summary>
    /// Test service class 1.
    /// </summary>
    public class ServiceLocatorTestService1
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets test value.
        /// </summary>
        public string Value { get; set; } = "Default1";

        #endregion Public Properties
    }

    /// <summary>
    /// Test service class 2.
    /// </summary>
    public class ServiceLocatorTestService2
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets test property.
        /// </summary>
        public string Property { get; set; } = "Test2";

        #endregion Public Properties
    }

    /// <summary>
    /// Service with dependency for testing dependency injection.
    /// </summary>
    public class ServiceLocatorTestServiceWithDependency
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorTestServiceWithDependency"/> class.
        /// </summary>
        /// <param name="testService">Test service dependency.</param>
        public ServiceLocatorTestServiceWithDependency(ServiceLocatorTestService1 testService)
        {
            this.TestService = testService;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the test service dependency.
        /// </summary>
        public ServiceLocatorTestService1 TestService { get; }

        #endregion Public Properties
    }

    #endregion Public Classes
}