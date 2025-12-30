/*
 * The namespace Roadbed.Sdk.NationalWeatherService.Entities was removed on purpose and replaced with Roadbed.Sdk.NationalWeatherService so that no additional using statements are required.
 */

namespace Roadbed.Sdk.NationalWeatherService;

using Microsoft.Extensions.Logging.Abstractions;
using Roadbed.Common;
using Roadbed.Messaging;
using Roadbed.Sdk.NationalWeatherService.Dtos;
using Roadbed.Sdk.NationalWeatherService.Repositories;

/// <summary>
/// Entity for the National Weather Service forecast.
/// </summary>
public class NwsForecast
    : BaseClassWithLogging<NwsForecast>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NwsForecast"/> class.
    /// </summary>
    /// <param name="messagingRquest">Messaging request for messages sent to API.</param>
    /// <remarks>
    /// The Name in the <see cref="MessagingPublisher"/> is used as the User Agent string for the National Weather Service API.
    /// This string can be anything, and the more unique to your application the less likely it will be affected by a security event.
    /// If you include contact information (website or email), we can contact you if your string is associated to a security event.
    /// This will be replaced with an API key in the future.
    /// </remarks>
    public NwsForecast(
        MessagingMessageRequest<CommonKeyValuePair<string, string>> messagingRquest)
        : base(NullLoggerFactory.Instance)
    {
        this.MessagingRequest = messagingRquest;

        // TO DO: Replace with Dependency Injection
        this.ForecastDailyRepository = new NwsForecastDailyRepository(messagingRquest);
        this.ForecastHourlyRepository = new NwsForecastHourlyRepository(messagingRquest);
        this.LocationRepository = new NwsLocationRepository(messagingRquest);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NwsForecast"/> class.
    /// </summary>
    /// <param name="messagingRquest">Messaging request for messages sent to API.</param>
    /// <param name="forecastDailyRepository">Daily Forecast Repository to use in CRUD operations.</param>
    /// <param name="forecastHourlyRepository">Hourly Forecast Repository to use in CRUD operations.</param>
    /// <param name="locationRepository">Location Repository to use in CRUD operations.</param>
    /// <remarks>
    /// The Name in the <see cref="MessagingPublisher"/> is used as the User Agent string for the National Weather Service API.
    /// This string can be anything, and the more unique to your application the less likely it will be affected by a security event.
    /// If you include contact information (website or email), we can contact you if your string is associated to a security event.
    /// This will be replaced with an API key in the future.
    /// </remarks>
    public NwsForecast(
        MessagingMessageRequest<CommonKeyValuePair<string, string>> messagingRquest,
        INwsForecastDailyRepository<NwsForecastResponse, string> forecastDailyRepository,
        INwsForecastHourlyRepository<NwsForecastResponse, string> forecastHourlyRepository,
        INwsLocationRepository<NwsLocationResponse, string> locationRepository)
        : base(NullLoggerFactory.Instance)
    {
        this.MessagingRequest = messagingRquest;
        this.ForecastDailyRepository = forecastDailyRepository;
        this.ForecastHourlyRepository = forecastHourlyRepository;
        this.LocationRepository = locationRepository;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the <see cref="INwsForecastDailyRepository{T, TKey}"/> Repository.
    /// </summary>
    public INwsForecastDailyRepository<NwsForecastResponse, string> ForecastDailyRepository
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the <see cref="INwsForecastHourlyRepository{T, TKey}"/> Repository.
    /// </summary>
    public INwsForecastHourlyRepository<NwsForecastResponse, string> ForecastHourlyRepository
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the <see cref="INwsLocationRepository{T, TKey}"/> Repository.
    /// </summary>
    public INwsLocationRepository<NwsLocationResponse, string> LocationRepository
    {
        get;
        set;
    }

    /// <summary>
    /// Gets the Hourly Forecast Points for the Forecast.
    /// </summary>
    public IList<NwsForecastPeriod>? HourlyForecastPeriods
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets the Daily Forecast Points for the Forecast.
    /// </summary>
    public IList<NwsForecastPeriod>? DailyForecastPeriods
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets the Weather Forecast Office for the Forecast.
    /// </summary>
    public NwsWeatherForecastOffice? WeatherForecastOffice
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets the Physical Address for the Forecast.
    /// </summary>
    public NwsPhysicalAddress? PhysicalAddress
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets the Created On for the Forecast.
    /// </summary>
    public DateTimeOffset? ForecastCreateOn
    {
        get;
        internal set;
    }

    #endregion Public Properties

    #region Private Properties

    /// <summary>
    /// Gets or sets the Response from the Location API.
    /// </summary>
    /// <remarks>
    /// Represents the Weather Forecast Office based on the physical address.
    /// </remarks>
    private NwsForecastResponse? ForecastDailyResponse
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Response from the Location API.
    /// </summary>
    /// <remarks>
    /// Represents the Weather Forecast Office based on the physical address.
    /// </remarks>
    private NwsForecastResponse? ForecastHourlyResponse
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Response from the Location API.
    /// </summary>
    /// <remarks>
    /// Represents the Weather Forecast Office based on the physical address.
    /// </remarks>
    private NwsLocationResponse? LocationResponse
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Messaging request for the interactions with the National Weather Service API.
    /// </summary>
    private MessagingMessageRequest<CommonKeyValuePair<string, string>> MessagingRequest
    {
        get;
        set;
    }

    #endregion Private Properties

    #region Public Methods

    /// <summary>
    /// Looks up a forecast for a physical location.
    /// </summary>
    /// <param name="latitude">Latitude of the coordinate.</param>
    /// <param name="longitude">Longitude of the coordinate.</param>
    /// <param name="messagingRquest">Messaging request for messages sent to API.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>Forecast for a physical location.</returns>
    public static async Task<NwsForecast> LookUpForecast(
        double latitude,
        double longitude,
        MessagingMessageRequest<CommonKeyValuePair<string, string>> messagingRquest,
        CancellationToken cancellationToken)
    {
        // Entity
        NwsForecast forecast = new NwsForecast(messagingRquest);

        // Fetch Forcast
        var result = await forecast.ListAsync(
            new NwsPhysicalAddress(latitude, longitude),
            cancellationToken);

        return result;
    }

    /// <summary>
    /// List Operation for the Data Transfer Object (DTO) entity.
    /// </summary>
    /// <param name="locationRequest">Request for National Weather Service forecast.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>List of the Data Transfer Object (DTO) entites.</returns>
    public async Task<NwsForecast> ListAsync(
        NwsPhysicalAddress locationRequest,
        CancellationToken cancellationToken)
    {
        // Log message
        this.LogTrace($"List operation called in {this}.");

        // Remember the Location
        this.PhysicalAddress = locationRequest;

        // Fill Private Properties
        await this.GetApiResponse(locationRequest, cancellationToken);

        // Fill Properties
        this.PopulatePropertiesAfterApiResponse();

        // Return Weather Forecast Office ID
        return this;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Fills entity properties after the API has responded.
    /// </summary>
    private void PopulatePropertiesAfterApiResponse()
    {
        // Verify results from 3 Seperate API endpoints
        if ((this.LocationResponse == null) ||
            (this.LocationResponse.Properties == null) ||
            (this.ForecastDailyResponse == null) ||
            (this.ForecastDailyResponse.Properties == null) ||
            (this.ForecastDailyResponse.Properties.Periods == null) ||
            (this.ForecastHourlyResponse == null) ||
            (this.ForecastHourlyResponse.Properties == null) ||
            (this.ForecastHourlyResponse.Properties.Periods == null))
        {
            return;
        }

        // Populate Properties
        this.PopulateCreatedOn();
        this.PopulateWeatherForecastOffice();
        this.PopulateDailyForecastPeriods();
        this.PopulateHourlyForecastPeriods();
    }

    /// <summary>
    /// Fills Weather Forecast Office Property.
    /// </summary>
    private void PopulateCreatedOn()
    {
        // Use Daily API Response as Forecast Created On
        if (DateTimeOffset.TryParse(
            this.ForecastDailyResponse!.Properties!.GeneratedAt,
            out DateTimeOffset createdOnResult))
        {
            this.ForecastCreateOn = createdOnResult;
        }
    }

    /// <summary>
    /// Fills Weather Forecast Office Property.
    /// </summary>
    private void PopulateWeatherForecastOffice()
    {
        if ((this.LocationResponse != null) &&
            (this.LocationResponse.Properties != null))
        {
            // Weather Forecast Office
            this.WeatherForecastOffice = new NwsWeatherForecastOffice(
                this.LocationResponse.Properties.ForecastOfficeId!,
                this.LocationResponse.Properties.TimeZone!);
        }
    }

    /// <summary>
    /// Fills Daily Forecast Periods Property.
    /// </summary>
    private void PopulateDailyForecastPeriods()
    {
        this.DailyForecastPeriods = new List<NwsForecastPeriod>();

        foreach (ForecastPeriodResponse period in this.ForecastDailyResponse!.Properties!.Periods!)
        {
            this.DailyForecastPeriods.Add(this.CreatePeriod(period));
        }
    }

    /// <summary>
    /// Fills Hourly Forecast Periods Property.
    /// </summary>
    private void PopulateHourlyForecastPeriods()
    {
        this.HourlyForecastPeriods = new List<NwsForecastPeriod>();

        foreach (ForecastPeriodResponse period in this.ForecastHourlyResponse!.Properties!.Periods!)
        {
            this.HourlyForecastPeriods.Add(this.CreatePeriod(period));
        }
    }

    /// <summary>
    /// Fills Hourly Forecast Periods Property.
    /// </summary>
    private NwsForecastPeriod CreatePeriod(ForecastPeriodResponse period)
    {
        if (period == null)
        {
            return default!;
        }

        decimal temp = period.Temperature!.Value;

        if (period.TemperatureUnit == "C")
        {
            temp = temp.ConvertCelsiusToFahrenheit();
        }

        decimal chance = 0;

        if (period.Precipitation!.Unit!.Equals("wmoUnit:percent", StringComparison.CurrentCultureIgnoreCase) &&
            decimal.TryParse(period.Precipitation!.Value, out decimal chanceResult))
        {
            chance = chanceResult;
        }

        NwsForecastPeriod result =
            new NwsForecastPeriod(
                period.Number!.Value,
                period.Name!,
                period.StartTime!.Value,
                period.EndTime!.Value,
                temp,
                period.WindSpeed!,
                period.WindDirection!,
                period.ForecastShort!,
                period.ForecastDetailed!,
                chance);

        return result;
    }

    /// <summary>
    /// Get the forecast data for a physical location.
    /// </summary>
    /// <param name="locationRequest">Request for National Weather Service forecast.</param>
    /// <param name="cancellationToken">Token to notify when an operation should be canceled.</param>
    /// <returns>Forecast API response.</returns>
    private async Task GetApiResponse(
        NwsPhysicalAddress locationRequest,
        CancellationToken cancellationToken)
    {
        NwsLocationResponse location = await this.LocationRepository.ListAsync(locationRequest, cancellationToken);

        // Verify Location Response
        if ((location == null) ||
            (location.Properties == null) ||
            (location.Properties.ForecastOfficeId == null) ||
            (location.Properties.GridCoordinateX == null) ||
            (location.Properties.GridCoordinateY == null))
        {
            return;
        }
        else
        {
            // Assign Results to Properties
            this.LocationResponse = location;

            NwsForecastRequest forecastRequest = new NwsForecastRequest(
                location.Properties.ForecastOfficeId,
                location.Properties.GridCoordinateX.Value,
                location.Properties.GridCoordinateY.Value);

            // Fetch Daily Forecast
            this.ForecastDailyResponse = await this.ForecastDailyRepository.ListAsync(
                forecastRequest,
                cancellationToken);

            // Fetch Hour Forecast
            this.ForecastHourlyResponse = await this.ForecastHourlyRepository.ListAsync(
                forecastRequest,
                cancellationToken);
        }

        return;
    }

    #endregion Private Methods
}