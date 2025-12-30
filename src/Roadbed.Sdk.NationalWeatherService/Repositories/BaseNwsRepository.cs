namespace Roadbed.Sdk.NationalWeatherService.Repositories;

using System;
using Roadbed.Common;
using Roadbed.Messaging;
using Roadbed.Net;

/// <summary>
/// Base repository for National Weather Service SDK.
/// </summary>
internal abstract class BaseNwsRepository
{
    #region Public Fields

    /// <summary>
    /// Base API path for the National Weather Service RESTful API.
    /// </summary>
    public const string BaseApiPath = "https://api.weather.gov";

    #endregion Public Fields

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseNwsRepository"/> class.
    /// </summary>
    /// <param name="request">Messaging request for messages sent to API.</param>
    public BaseNwsRepository(MessagingMessageRequest<CommonKeyValuePair<string, string>> request)
    {
        this.Request = request;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the message request.
    /// </summary>
    public MessagingMessageRequest<CommonKeyValuePair<string, string>> Request { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Creates a GET HTTP request for the specified endpoint path.
    /// </summary>
    /// <param name="endPointPath">API endpoint.</param>
    /// <returns>HTTP GET request for the National Weather Service API.</returns>
    public NetHttpRequest CreateHttpGetRequest(string endPointPath)
    {
        ArgumentNullException.ThrowIfNull(endPointPath);

        string appName = this.Request.Publisher.Identifier;

        if (!string.IsNullOrEmpty(this.Request.Publisher.Name))
        {
            appName = this.Request.Publisher.Name;
        }

        NetHttpRequest request = new NetHttpRequest
        {
            Method = HttpMethod.Get,
            HttpEndPoint = new Uri(endPointPath),
            HttpHeaders = new List<NetHttpHeader>()
            {
                { new NetHttpHeader("User-Agent", appName) },
                { new NetHttpHeader("Accept", "application/geo+json") },
            },
            EnableCompression = false,
        };

        return request;
    }

    #endregion Public Methods
}