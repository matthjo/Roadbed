/*
 * The namespace Roadbed.Net.Entities was removed on purpose and replaced with Roadbed.Net so that no additional using statements are required.
 */

namespace Roadbed.Net;

using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;

/// <summary>
/// Service for managing HttpClient operations.
/// </summary>
public static class NetHttpClient
{
    #region Public Methods

    /// <summary>
    /// Make Http Request.
    /// </summary>
    /// <typeparam name="T">Object returned from the API.</typeparam>
    /// <param name="request">API request.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>Http response from the API.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    /// <exception cref="Exception">Raised after the end of the retry pattern is reached.</exception>
    public static async Task<NetHttpResponse<T>> MakeRequestAsync<T>(
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
                    await MakeRequestWithBackoffRetryAsync(request, requestMessage, cancelToken);

                // Add the Data
                if (message.IsSuccessStatusCode ||
                    (message.StatusCode != HttpStatusCode.NotFound))
                {
                    // Grab the body of the response
                    string responseBody = await message.Content.ReadAsStringAsync();

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

        // Return the Result
        return response;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Create <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <param name="handler"><see cref="HttpClientHandler"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <returns><see cref="HttpClient"/> based on the <see cref="NetHttpRequest"/>.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    private static HttpClient CreateHttpClient(NetHttpRequest request, HttpClientHandler handler)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Result Container
        HttpClient client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(request.TimeoutInSecondsPerAttempt),
        };

        // Return Result
        return client;
    }

    /// <summary>
    /// Create <see cref="HttpClientHandler"/>.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClientHandler"/>.</param>
    /// <returns><see cref="HttpClientHandler"/> based on the <see cref="NetHttpRequest"/>.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    private static HttpClientHandler CreateHttpClientHandler(NetHttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Result Container
        HttpClientHandler handler = new HttpClientHandler();

        // Compression
        if (request.EnableCompression)
        {
            // Allow support for Deflate or GZip
            handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }

        // Return Result
        return handler;
    }

    /// <summary>
    /// Create <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpRequestMessage"/>.</param>
    /// <returns><see cref="HttpRequestMessage"/> based on the <see cref="NetHttpRequest"/>.</returns>
    /// <exception cref="ArgumentNullException">Request is null.</exception>
    private static HttpRequestMessage CreateHttpRequestMessage(NetHttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Result Container
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
            string name = string.Empty;

            if (request.Authentication.AuthenticationType == NetHttpAuthenticationType.Basic)
            {
                name = "Basic";
            }
            else if (request.Authentication.AuthenticationType == NetHttpAuthenticationType.Bearer)
            {
                name = "Bearer";
            }

            message.Headers.Authorization = new AuthenticationHeaderValue(
                name,
                request.Authentication.Value);
        }

        // Return Result
        return message;
    }

    /// <summary>
    /// Implementation of the retry pattern with backoff strategy.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <param name="message">Body of Http Request.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>API response.</returns>
    /// <exception cref="Exception">Raised after the end of the retry pattern is reached.</exception>
    private static async Task<HttpResponseMessage> MakeRequestWithBackoffRetryAsync(
        NetHttpRequest request,
        HttpRequestMessage message,
        CancellationToken cancelToken)
    {
        for (int attempt = 0; attempt <= request.RetryPattern.MaxAttempts; attempt++)
        {
            try
            {
                using (HttpClientHandler handler = CreateHttpClientHandler(request))
                {
                    using (HttpClient client = CreateHttpClient(request, handler))
                    {
                        // Result Container. Wrap with Task timeout incase HttpClient gets hung up.
                        HttpResponseMessage response = await client.SendAsync(message).WaitAsync(
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

                        // Return Result
                        return response;
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Networking issue Occurred
                await WaitAsync(request, attempt, cancelToken);
            }
            catch (TimeoutException)
            {
                // Task Timeout Occurred
                await WaitAsync(request, attempt, cancelToken);
            }
        }

        // Retry Pattern exhausted. Return bad request.
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Unable to complete Http Request."),
        };
    }

    /// <summary>
    /// Implements the wait logic for the backoff strategy.
    /// </summary>
    /// <param name="request"><see cref="NetHttpRequest"/> to use in the creation of the <see cref="HttpClient"/>.</param>
    /// <param name="attempt">Count representing which attempt.</param>
    /// <param name="cancelToken">Token to cancel tasks.</param>
    /// <returns>Completed Task after the delay.</returns>
    private static async Task WaitAsync(
        NetHttpRequest request,
        int attempt,
        CancellationToken cancelToken)
    {
        // Backoff Strategy. Increase delay with each attempt.
        var amount = Math.Pow(request.RetryPattern.DelayMultiplierInSeconds, attempt);

        // exponential backoff waiting retry logic
        await Task.Delay(TimeSpan.FromSeconds(amount), cancelToken);
    }

    #endregion Private Methods
}