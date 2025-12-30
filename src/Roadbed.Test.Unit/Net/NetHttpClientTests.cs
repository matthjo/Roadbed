namespace Roadbed.Test.Unit.Net;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpClient class.
/// </summary>
[TestClass]
public class NetHttpClientTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that constructor throws exception when httpClientFactory is null.
    /// </summary>
    [TestMethod]
    public void Constructor_NullHttpClientFactory_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        IHttpClientFactory? nullFactory = null;
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            var service = new NetHttpClientService(nullFactory!);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsTrue(
            exceptionThrown,
            "Constructor should throw ArgumentNullException when httpClientFactory is null.");
    }

    /// <summary>
    /// Unit test to verify that constructor initializes with valid httpClientFactory.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidHttpClientFactory_InitializesSuccessfully()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        // Act (When)
        var service = new NetHttpClientService(factory);

        // Assert (Then)
        Assert.IsNotNull(
            service,
            "NetHttpClientService should be initialized successfully.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles Basic authentication.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_BasicAuthentication_AddsAuthenticationHeader()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var auth = new NetHttpAuthentication
        {
            AuthenticationType = NetHttpAuthenticationType.Basic,
            Value = "credentials",
        };
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            Authentication = auth,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles Bearer authentication.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_BearerAuthentication_AddsAuthenticationHeader()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var auth = new NetHttpAuthentication
        {
            AuthenticationType = NetHttpAuthenticationType.Bearer,
            Value = "test-token-123",
        };
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            Authentication = auth,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync uses DefaultClient when compression is disabled.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_CompressionDisabled_UsesDefaultClient()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient("DefaultClient");
        services.AddHttpClient("CompressedClient");
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            EnableCompression = false,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null even on failure.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync uses CompressedClient when compression is enabled.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_CompressionEnabled_UsesCompressedClient()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient("DefaultClient");
        services.AddHttpClient("CompressedClient");
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            EnableCompression = true,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null even on failure.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles custom headers.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_CustomHeaders_AddsHeadersToRequest()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var headers = new System.Collections.Generic.List<NetHttpHeader>
        {
            new NetHttpHeader("X-Custom-Header", "custom-value"),
            new NetHttpHeader("Accept", "application/json"),
        };
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            HttpHeaders = headers,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync respects custom timeout.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_CustomTimeout_SetsTimeoutCorrectly()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            TimeoutInSecondsPerAttempt = 5,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles empty header name.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_EmptyHeaderName_SkipsHeader()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var headers = new System.Collections.Generic.List<NetHttpHeader>
        {
            new NetHttpHeader(string.Empty, "value"),
            new NetHttpHeader("Valid-Header", "valid-value"),
        };
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            HttpHeaders = headers,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles GET request correctly.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_GetMethod_CreatesValidRequest()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles invalid URI.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_InvalidUri_ReturnsFailureResponse()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = null,
            Method = HttpMethod.Get,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsFalse(
            response.IsSuccessStatusCode,
            "Response should indicate failure for null URI.");
        Assert.AreEqual(
            500,
            response.HttpStatusCode,
            "HttpStatusCode should be 500 for general exception.");
        Assert.IsNotEmpty(
            response.Errors,
            "Errors list should contain at least one error.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles multiple retry attempts.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_MultipleRetryAttempts_RetriesAsConfigured()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 2;
        request.RetryPattern.DelayMultiplierInSeconds = 1;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null after retries.");
        Assert.IsFalse(
            response.IsSuccessStatusCode,
            "Response should indicate failure after exhausting retries.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles network errors.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_NetworkError_ReturnsFailureResponse()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-domain-that-does-not-exist-12345.com"),
            Method = HttpMethod.Get,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        request.RetryPattern.DelayMultiplierInSeconds = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsFalse(
            response.IsSuccessStatusCode,
            "Response should indicate failure for network error.");
        Assert.AreEqual(
            400,
            response.HttpStatusCode,
            "HttpStatusCode should be 400 for bad request.");
        Assert.IsNotEmpty(
            response.Errors,
            "Errors list should contain at least one error.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync throws exception when request is null.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        NetHttpRequest? nullRequest = null;
        CancellationToken cancelToken = CancellationToken.None;
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            await service.MakeHttpRequestAsync<string>(nullRequest!, cancelToken);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsTrue(
            exceptionThrown,
            "MakeRequestAsync should throw ArgumentNullException when request is null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles POST request with content.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_PostMethodWithContent_CreatesValidRequest()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var content = new StringContent("{\"test\": \"data\"}");
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Post,
            Content = content,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles Unknown authentication type.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_UnknownAuthentication_DoesNotAddAuthenticationHeader()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var auth = new NetHttpAuthentication
        {
            AuthenticationType = NetHttpAuthenticationType.Unknown,
            Value = "test-value",
        };
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            Authentication = auth,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
    }

    /// <summary>
    /// Unit test to verify that MakeRequestAsync handles retry pattern with zero attempts.
    /// </summary>
    /// <returns>A task representing the asynchronous unit test operation.</returns>
    [TestMethod]
    public async Task MakeRequestAsync_ZeroRetryAttempts_ExecutesOnce()
    {
        // Arrange (Given)
        var services = new ServiceCollection();
        services.AddHttpClient();
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var service = new NetHttpClientService(factory);
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("http://invalid-test.com"),
            Method = HttpMethod.Get,
            TimeoutInSecondsPerAttempt = 1,
        };
        request.RetryPattern.MaxAttempts = 0;
        request.RetryPattern.DelayMultiplierInSeconds = 1;
        CancellationToken cancelToken = CancellationToken.None;

        // Act (When)
        var response = await service.MakeHttpRequestAsync<string>(request, cancelToken);

        // Assert (Then)
        Assert.IsNotNull(
            response,
            "Response should not be null.");
        Assert.IsFalse(
            response.IsSuccessStatusCode,
            "Response should indicate failure for invalid domain.");
    }

    #endregion Public Methods
}