namespace Roadbed.Sdk.NationalWeatherService.Dtos;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Forecast period from the National Weather Service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NwsForecastPeriod"/> class.
/// </remarks>
/// <param name="DisplayOrder">Order to display the period.</param>
/// <param name="Name">Name of the period.</param>
/// <param name="StartTime">Start Time of the period.</param>
/// <param name="EndTime">End Time of the period.</param>
/// <param name="TemperatureInFahrenheit">Temperature forecast for the period.</param>
/// <param name="WindSpeed">Wind Speed for the period.</param>
/// <param name="WindDirection">Wind Direction for the period.</param>
/// <param name="DescriptionShort">Short Forecast for the period.</param>
/// <param name="DescriptionDetailed">Detailed Forecast for the period.</param>
/// <param name="ChanceOfPrecipitation">Percentage of the chance of precipitation during the period.</param>
[SuppressMessage(
    "StyleCop",
    "SA1313",
    Justification = "Default constructor OK to not start with lower-case letter")]
public record NwsForecastPeriod(
    int DisplayOrder,
    string Name,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    decimal TemperatureInFahrenheit,
    string WindSpeed,
    string WindDirection,
    string DescriptionShort,
    string DescriptionDetailed,
    decimal ChanceOfPrecipitation);