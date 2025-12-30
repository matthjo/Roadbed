namespace Roadbed.Sdk.NationalWeatherService.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Roadbed.Common;
using Roadbed.Messaging;
using Roadbed.Net;
using Roadbed.Sdk.NationalWeatherService.Dtos;

/// <summary>
/// CRUD repository for NwsForecast entity.
/// </summary>
internal class NwsLocationRepository
    : BaseNwsRepository, INwsLocationRepository<NwsLocationResponse, string>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NwsLocationRepository"/> class.
    /// </summary>
    /// <param name="request">Messaging request for messages sent to API.</param>
    public NwsLocationRepository(MessagingMessageRequest<CommonKeyValuePair<string, string>> request)
        : base(request)
    {
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc />
    /// <remarks>
    /// This will provide the grid forecast endpoints for three format options in these properties:
    /// - forecast - forecast for 12h periods over the next seven days.
    /// - forecastHourly - forecast for hourly periods over the next seven days.
    /// - forecastGridData - raw forecast data over the next seven days.
    /// </remarks>
    public async Task<NwsLocationResponse> ListAsync(NwsPhysicalAddress request, CancellationToken cancellationToken)
    {
        // URL syntax for API Endpoint:
        // https://api.weather.gov/points/{latitude},{longitude}
        string endPoint = string.Join(
                "/",
                BaseApiPath,
                "points",
                string.Concat(request.Latitude, ',', request.Longitude));

        // Create Request
        NetHttpRequest apiRequest = this.CreateHttpGetRequest(endPoint);

        // Make HTTP request
        NetHttpResponse<string> response =
            await NetHttpClientService.MakeRequestAsync<string>(apiRequest, cancellationToken);

        // Verify Response
        if (response.IsSuccessStatusCode)
        {
            NwsLocationResponse? result =
                JsonConvert.DeserializeObject<NwsLocationResponse>(response.Data);

            if (result != null)
            {
                return result;
            }
        }

        return default!;
    }

    #endregion Public Methods
}