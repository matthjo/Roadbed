/*
 * The namespace Roadbed.Net.Entities was removed on purpose and replaced with Roadbed.Net so that no additional using statements are required.
 */

namespace Roadbed.Net;

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Service for managing HttpClient operations using IHttpClientFactory.
/// </summary>
public class NetHttpClient
{
    #region Private Fields

    /// <summary>
    /// Factory for creating HttpClient instances.
    /// </summary>
    private readonly IHttpClientFactory httpClientFactory;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NetHttpClient"/> class.
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HttpClient instances.</param>
    /// <exception cref="ArgumentNullException">httpClientFactory is null.</exception>
    public NetHttpClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        this.httpClientFactory = httpClientFactory;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Make Http Request.
    /// </summary>
    /// <typeparam name="T">Object returned from the API.</typeparam>
    /// <param name="request">API request.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>Http response from the API.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    public static async Task<NetHttpResponse<T>> MakeRequestAsync<T>(
    NetHttpRequest request,
    CancellationToken cancelToken)
    {
        // Fetch from Dependency Injection
        var factory = ServiceLocator.GetService<IHttpClientFactory>();
        ArgumentNullException.ThrowIfNull(factory);

        // Create instance
        NetHttpClient service = new NetHttpClient(factory);

        // Return the result
        return await service.MakeHttpRequestAsync<T>(request, cancelToken);
    }

    /// <summary>
    /// Make Http Request.
    /// </summary>
    /// <typeparam name="T">Object returned from the API.</typeparam>
    /// <param name="request">API request.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>Http response from the API.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    public async Task<NetHttpResponse<T>> MakeHttpRequestAsync<T>(
        NetHttpRequest request,
        CancellationToken cancelToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Response Container
        NetHttpResponse<T> response;

        try
        {
            using (HttpRequestMessage requestMessage = CreateHttpRequestMessage(request))
            {
                // Make request with Retry Pattern.
                HttpResponseMessage message =
                    await this.MakeRequestWithBackoffRetryAsync(request, requestMessage, cancelToken);

                // Add the Data
                if (message.IsSuccessStatusCode &&
                    (message.StatusCode != HttpStatusCode.NotFound))
                {
                    // Grab the body of the response
                    string responseBody = await message.Content.ReadAsStringAsync(cancelToken);

                    response = NetHttpResponse<T>.Success(
                            (int)message.StatusCode,
                            message.ReasonPhrase,
                            (T)Convert.ChangeType(responseBody, typeof(T)));
                }
                else
                {
                    response = NetHttpResponse<T>.Failure(
                            (int)message.StatusCode,
                            message.ReasonPhrase,
                            "Not a successful HTTP call. Status code indicates a failed HTTP Request.");
                }
            }
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            response = NetHttpResponse<T>.Failure(
                500,
                string.Concat(ex?.Message, " ", ex?.InnerException?.Message),
                "Not a successful HTTP call. An unknown error occurred with the HTTP Request.");
        }
        catch (SocketException ex)
        {
            response = NetHttpResponse<T>.Failure(
                500,
                string.Concat(ex?.Message, " ", ex?.InnerException?.Message),
                "Not a successful HTTP call. An unknown error occurred with the socket.");
        }
        catch (Exception ex)
        {
            response = NetHttpResponse<T>.Failure(
                500,
                string.Concat(ex?.Message, " ", ex?.InnerException?.Message),
                "Not a successful HTTP call. An unknown error occurred.");
        }

        return response;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Create <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpRequestMessage"/>.</param>
    /// <returns><see cref="HttpRequestMessage"/> based on the <see cref="NetHttpRequest"/>.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    private static HttpRequestMessage CreateHttpRequestMessage(NetHttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpRequestMessage message = new HttpRequestMessage(
            request.Method,
            request.HttpEndPoint)
        {
            Content = request.Content,
        };

        // Add HTTP Headers
        if (request.HttpHeaders != null)
        {
            foreach (NetHttpHeader header in request.HttpHeaders)
            {
                if (!string.IsNullOrEmpty(header.Name))
                {
                    message.Headers.Add(header.Name.Trim(), header.Value);
                }
            }
        }

        // Add Authentication
        if ((request.Authentication != null) &&
            (request.Authentication.AuthenticationType != NetHttpAuthenticationType.Unknown))
        {
            string name = request.Authentication.AuthenticationType switch
            {
                NetHttpAuthenticationType.Basic => "Basic",
                NetHttpAuthenticationType.Bearer => "Bearer",
                _ => string.Empty
            };

            if (!string.IsNullOrEmpty(name))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue(
                    name,
                    request.Authentication.Value);
            }
        }

        return message;
    }

    /// <summary>
    /// Implements the wait logic for the backoff strategy.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the backoff calculation.</param>
    /// <param name="attempt">Count representing which attempt.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>Completed Task after the delay.</returns>
    private static async Task WaitAsync(
        NetHttpRequest request,
        int attempt,
        CancellationToken cancelToken)
    {
        // Backoff strategy - increase delay with each attempt
        var amount = Math.Pow(request.RetryPattern.DelayMultiplierInSeconds, attempt);
        await Task.Delay(TimeSpan.FromSeconds(amount), cancelToken);
    }

    /// <summary>
    /// Create <see cref="HttpClient"/> using the factory.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <returns><see cref="HttpClient"/> based on the <see cref="NetHttpRequest"/>.</returns>
    private HttpClient CreateHttpClient(NetHttpRequest request)
    {
        // Create client with named configuration or default
        string clientName = request.EnableCompression ? "CompressedClient" : "DefaultClient";

        HttpClient client = this.httpClientFactory.CreateClient(clientName);

        // Set timeout per request
        client.Timeout = TimeSpan.FromSeconds(request.TimeoutInSecondsPerAttempt);

        return client;
    }

    /// <summary>
    /// Implementation of the retry pattern with backoff strategy.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <param name="message">Body of Http Request.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>API response.</returns>
    private async Task<HttpResponseMessage> MakeRequestWithBackoffRetryAsync(
        NetHttpRequest request,
        HttpRequestMessage message,
        CancellationToken cancelToken)
    {
        for (int attempt = 0; attempt <= request.RetryPattern.MaxAttempts; attempt++)
        {
            try
            {
                using (HttpClient client = this.CreateHttpClient(request))
                {
                    // Send request with timeout
                    HttpResponseMessage response = await client.SendAsync(
                        message,
                        HttpCompletionOption.ResponseContentRead,
                        cancelToken)
                        .WaitAsync(
                            TimeSpan.FromSeconds(request.TimeoutInSecondsPerAttempt),
                            cancelToken);

                    // Determine if we want to try again
                    if ((response.StatusCode == HttpStatusCode.ServiceUnavailable) ||
                        (response.StatusCode == HttpStatusCode.RequestTimeout) ||
                        (response.StatusCode == HttpStatusCode.GatewayTimeout))
                    {
                        await WaitAsync(request, attempt, cancelToken);
                        continue;
                    }

                    return response;
                }
            }
            catch (HttpRequestException)
            {
                // Networking issue occurred
                await WaitAsync(request, attempt, cancelToken);
            }
            catch (TimeoutException)
            {
                // Task timeout occurred
                await WaitAsync(request, attempt, cancelToken);
            }
        }

        // Retry pattern exhausted
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Unable to complete Http Request."),
        };
    }

    #endregion Private Methods
}