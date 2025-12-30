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
/// Initializes a new instance of the <see cref="NwsLocationResponse"/> class.
/// </remarks>
public record NwsLocationResponse
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
    public LocationProperties? Properties { get; set; }

    #endregion Public Properties
}

/// <summary>
/// Properties section of the API response.
/// </summary>
public class LocationProperties
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the URL to the County from the properties section.
    /// </summary>
    [JsonProperty("gridX")]
    public int? GridCoordinateX { get; set; }

    /// <summary>
    /// Gets or sets the URL to the County from the properties section.
    /// </summary>
    [JsonProperty("gridY")]
    public int? GridCoordinateY { get; set; }

    /// <summary>
    /// Gets or sets the URL to the County from the properties section.
    /// </summary>
    [JsonProperty("county")]
    public string? County { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Fire Weather Zone from the properties section.
    /// </summary>
    [JsonProperty("fireWeatherZone")]
    public string? FireWeatherZone { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Forecast Grid Data from the properties section.
    /// </summary>
    [JsonProperty("forecastGridData")]
    public string? ForecastGridData { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Forecast Hourly from the properties section.
    /// </summary>
    [JsonProperty("forecastHourly")]
    public string? ForecastHourly { get; set; }

    /// <summary>
    /// Gets or sets the Forecast Office ID from the properties section.
    /// </summary>
    [JsonProperty("forecastOffice")]
    public string? ForecastOffice { get; set; }

    /// <summary>
    /// Gets or sets the Forecast Office ID from the properties section.
    /// </summary>
    [JsonProperty("gridId")]
    public string? ForecastOfficeId { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Forecast Url from the properties section.
    /// </summary>
    [JsonProperty("forecast")]
    public string? ForecastUrl { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Forecast Zone from the properties section.
    /// </summary>
    [JsonProperty("forecastZone")]
    public string? ForecastZone { get; set; }

    /// <summary>
    /// Gets or sets the URL to the Observation Stations from the properties section.
    /// </summary>
    [JsonProperty("observationStations")]
    public string? ObservationStations { get; set; }

    /// <summary>
    /// Gets or sets the TimeZone from the properties section.
    /// </summary>
    [JsonProperty("timeZone")]
    public string? TimeZone { get; set; }

    #endregion Public Properties
}