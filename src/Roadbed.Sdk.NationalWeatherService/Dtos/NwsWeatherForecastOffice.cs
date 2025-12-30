namespace Roadbed.Sdk.NationalWeatherService.Dtos;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Weather Forecast Office Data Transfer Object (DTO).
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NwsWeatherForecastOffice"/> class.
/// </remarks>
/// <param name="Identifier">3-Character Identifier for the Weather Forecast Office.</param>
/// <param name="TimeZone">TimeZone for the Weather Forecast Office.</param>
[SuppressMessage(
    "StyleCop",
    "SA1313",
    Justification = "Default constructor OK to not start with lower-case letter")]
public record NwsWeatherForecastOffice(
    string Identifier,
    string TimeZone);