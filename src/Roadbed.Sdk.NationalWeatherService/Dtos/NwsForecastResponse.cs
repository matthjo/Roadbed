/*
 * The namespace Roadbed.Sdk.NationalWeatherService.Dtos was removed on purpose and replaced with Roadbed.Sdk.NationalWeatherService so that no additional using statements are required.
 */

namespace Roadbed.Sdk.NationalWeatherService;

using Newtonsoft.Json;
using Roadbed.Crud;

/// <summary>
/// Forecast data from the National Weather Service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NwsForecastResponse"/> class.
/// </remarks>
public record NwsForecastResponse
    : BaseDataTransferObject<string>
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the type section from the API response.
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the properties section from the API response.
    /// </summary>
    [JsonProperty("properties")]
    public ForecastPropertyResponse? Properties { get; set; }

    #endregion Public Properties
}

/// <summary>
/// Properties section of the API response.
/// </summary>
public class ForecastPropertyResponse
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the Generated At from the properties section.
    /// </summary>
    [JsonProperty("generatedAt")]
    public string? GeneratedAt { get; set; }

    /// <summary>
    /// Gets or sets the Forecast Periods from the properties section.
    /// </summary>
    [JsonProperty("periods")]
    public IList<ForecastPeriodResponse>? Periods { get; set; }

    #endregion Public Properties
}

/// <summary>
/// Properties section of the API response.
/// </summary>
public class ForecastPeriodResponse
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the Number from the periods section under the properties section.
    /// </summary>
    [JsonProperty("number")]
    public int? Number { get; set; }

    /// <summary>
    /// Gets or sets the Name from the periods section under the properties section.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the Start Time from the periods section under the properties section.
    /// </summary>
    [JsonProperty("startTime")]
    public DateTimeOffset? StartTime { get; set; }

    /// <summary>
    /// Gets or sets the End Time from the periods section under the properties section.
    /// </summary>
    [JsonProperty("endTime")]
    public DateTimeOffset? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the Is Day Time from the periods section under the properties section.
    /// </summary>
    [JsonProperty("isDaytime")]
    public bool? IsDayTime { get; set; }

    /// <summary>
    /// Gets or sets the Temperature from the periods section under the properties section.
    /// </summary>
    [JsonProperty("temperature")]
    public decimal? Temperature { get; set; }

    /// <summary>
    /// Gets or sets the Temperature Unit from the periods section under the properties section.
    /// </summary>
    [JsonProperty("temperatureUnit")]
    public string? TemperatureUnit { get; set; }

    /// <summary>
    /// Gets or sets the Wind Speed from the periods section under the properties section.
    /// </summary>
    [JsonProperty("windSpeed")]
    public string? WindSpeed { get; set; }

    /// <summary>
    /// Gets or sets the Wind Direction from the periods section under the properties section.
    /// </summary>
    [JsonProperty("windDirection")]
    public string? WindDirection { get; set; }

    /// <summary>
    /// Gets or sets the Short Forecast from the periods section under the properties section.
    /// </summary>
    [JsonProperty("shortForecast")]
    public string? ForecastShort { get; set; }

    /// <summary>
    /// Gets or sets the Detailed Forecast from the periods section under the properties section.
    /// </summary>
    [JsonProperty("detailedForecast")]
    public string? ForecastDetailed { get; set; }

    /// <summary>
    /// Gets or sets the Precipitation from the periods section under the properties section.
    /// </summary>
    [JsonProperty("probabilityOfPrecipitation")]
    public ForecastPrecipitationResponse? Precipitation { get; set; }

    #endregion Public Properties
}

/// <summary>
/// Properties section of the API response.
/// </summary>
public class ForecastPrecipitationResponse
{
    /// <summary>
    /// Gets or sets the Unit from the probability of precipitation section under the periods section.
    /// </summary>
    [JsonProperty("unitCode")]
    public string? Unit { get; set; }

    /// <summary>
    /// Gets or sets the Value from the probability of precipitation section under the periods section.
    /// </summary>
    [JsonProperty("value")]
    public string? Value { get; set; }
}