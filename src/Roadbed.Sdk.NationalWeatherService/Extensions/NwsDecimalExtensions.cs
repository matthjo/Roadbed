/*
 * The namespace Roadbed.Sdk.NationalWeatherService.Extensions was removed on purpose and replaced with Roadbed.Sdk.NationalWeatherService so that no additional using statements are required.
 */

namespace Roadbed.Sdk.NationalWeatherService;

/// <summary>
/// Extensions for weather Decimal operations.
/// </summary>
public static class NwsDecimalExtensions
{
    #region Public Methods

    /// <summary>
    /// Converts Celsius To Fahrenheit.
    /// </summary>
    /// <param name="celsius">Celsius value to convert.</param>
    /// <returns>Fahrenheit value based on the Celsius.</returns>
    public static decimal ConvertCelsiusToFahrenheit(this decimal celsius)
    {
        return (celsius * 9 / 5) + 32;
    }

    /// <summary>
    /// Converts Kilometers To Miles.
    /// </summary>
    /// <param name="kilometers">Kilometers value to convert.</param>
    /// <returns>Mile value based on the Kilometers.</returns>
    public static decimal ConvertKilometersPerHourToMilesPerHour(this decimal kilometers)
    {
        return kilometers / 1.609m;
    }

    /// <summary>
    /// Converts Meters To Miles.
    /// </summary>
    /// <param name="meters">Meter value to convert.</param>
    /// <returns>Mile value based on the Meters.</returns>
    public static decimal ConvertMetersPerSecondToMilesPerHour(this decimal meters)
    {
        return meters * 2.23694m;
    }

    #endregion Public Methods
}