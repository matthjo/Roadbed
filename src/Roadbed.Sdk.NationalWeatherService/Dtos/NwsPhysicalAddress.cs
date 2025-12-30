namespace Roadbed.Sdk.NationalWeatherService.Dtos;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Request for National Weather Service forecast based on latitude and longitude.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NwsPhysicalAddress"/> class.
/// </remarks>
/// <param name="Latitude">Latitude of the coordinate</param>
/// <param name="Longitude">Longitude of the coordinate.</param>
[SuppressMessage(
    "StyleCop",
    "SA1313",
    Justification = "Default constructor OK to not start with lower-case letter")]
public record NwsPhysicalAddress(double Latitude, double Longitude);