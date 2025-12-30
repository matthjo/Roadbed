namespace Roadbed.Sdk.NationalWeatherService.Dtos;

/// <summary>
/// Request for National Weather Service forecast based on latitude and longitude.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NwsForecastRequest"/> class.
/// </remarks>
/// <param name="forecastOfficeId">Weather Forecast Office Identifier.</param>
/// <param name="gridCoordinateX">X Grid Coordinate of the request.</param>
/// <param name="gridCoordinateY">Y Grid Coordinate of the request.</param>
public class NwsForecastRequest(string forecastOfficeId, int gridCoordinateX, int gridCoordinateY)
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the X Grid Coordinate of the request.
    /// </summary>
    public int? GridCoordinateX { get; set; } = gridCoordinateX;

    /// <summary>
    /// Gets or sets the Y Grid Coordinate of the request.
    /// </summary>
    public int? GridCoordinateY { get; set; } = gridCoordinateY;

    /// <summary>
    /// Gets or sets the Weather Forecast Office ID for the request.
    /// </summary>
    public string WeatherForecastOfficeId { get; set; } = forecastOfficeId;

    #endregion Public Properties
}