namespace Roadbed.Test.Unit.NationalWeatherService;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Sdk.NationalWeatherService;

/// <summary>
/// Unit tests for the NwsDecimalExtensions class.
/// </summary>
[TestClass]
public class NwsDecimalExtensionsTests
{
    #region ConvertCelsiusToFahrenheit Tests

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns 32 when provided with 0 degrees Celsius (freezing point).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_FreezingPoint_Returns32()
    {
        // Arrange
        decimal celsius = 0m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(32m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns 212 when provided with 100 degrees Celsius (boiling point).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_BoilingPoint_Returns212()
    {
        // Arrange
        decimal celsius = 100m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(212m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns -40 when provided with -40 degrees Celsius (equal point).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_NegativeForty_ReturnsNegativeForty()
    {
        // Arrange
        decimal celsius = -40m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(-40m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns 98.6 when provided with 37 degrees Celsius (body temperature).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_BodyTemperature_Returns98Point6()
    {
        // Arrange
        decimal celsius = 37m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(98.6m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns 68 when provided with 20 degrees Celsius (room temperature).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_RoomTemperature_Returns68()
    {
        // Arrange
        decimal celsius = 20m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(68m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns -459.67 when provided with -273.15 degrees Celsius (absolute zero).
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_AbsoluteZero_ReturnsNegative459Point67()
    {
        // Arrange
        decimal celsius = -273.15m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(-459.67m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns a positive value when provided with a positive Celsius value.
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_PositiveValue_ReturnsPositiveValue()
    {
        // Arrange
        decimal celsius = 25m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(77m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit returns a negative value when provided with a negative Celsius value below -40.
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_NegativeValue_ReturnsNegativeValue()
    {
        // Arrange
        decimal celsius = -50m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(-58m, result);
    }

    /// <summary>
    /// Verifies that ConvertCelsiusToFahrenheit correctly handles decimal precision with fractional values.
    /// </summary>
    [TestMethod]
    public void ConvertCelsiusToFahrenheit_DecimalValue_ReturnsAccurateResult()
    {
        // Arrange
        decimal celsius = 23.5m;

        // Act
        decimal result = celsius.ConvertCelsiusToFahrenheit();

        // Assert
        Assert.AreEqual(74.3m, result);
    }

    #endregion

    #region ConvertKilometersPerHourToMilesPerHour Tests

    /// <summary>
    /// Verifies that ConvertKilometersPerHourToMilesPerHour returns 0 when provided with 0 kilometers per hour.
    /// </summary>
    [TestMethod]
    public void ConvertKilometersPerHourToMilesPerHour_Zero_ReturnsZero()
    {
        // Arrange
        decimal kilometers = 0m;

        // Act
        decimal result = kilometers.ConvertKilometersPerHourToMilesPerHour();

        // Assert
        Assert.AreEqual(0m, result);
    }

    /// <summary>
    /// Verifies that ConvertKilometersPerHourToMilesPerHour returns approximately 1 when provided with 1.609 kilometers per hour.
    /// </summary>
    [TestMethod]
    public void ConvertKilometersPerHourToMilesPerHour_OnePointSixZeroNine_ReturnsOne()
    {
        // Arrange
        decimal kilometers = 1.609m;

        // Act
        decimal result = kilometers.ConvertKilometersPerHourToMilesPerHour();

        // Assert
        Assert.AreEqual(1m, Math.Round(result, 0));
    }

    #endregion

    #region ConvertMetersPerSecondToMilesPerHour Tests

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour returns 0 when provided with 0 meters per second.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_Zero_ReturnsZero()
    {
        // Arrange
        decimal meters = 0m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(0m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour returns approximately 2.237 when provided with 1 meter per second.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_One_ReturnsApproximately2Point24()
    {
        // Arrange
        decimal meters = 1m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(2.23694m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour returns approximately 22.369 when provided with 10 meters per second.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_Ten_ReturnsApproximately22Point37()
    {
        // Arrange
        decimal meters = 10m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(22.3694m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour returns approximately 223.694 when provided with 100 meters per second.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_OneHundred_ReturnsApproximately224()
    {
        // Arrange
        decimal meters = 100m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(223.694m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour correctly handles decimal precision with fractional values.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_DecimalValue_ReturnsAccurateResult()
    {
        // Arrange
        decimal meters = 5.5m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(12.30317m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour correctly handles small decimal values.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_SmallDecimal_ReturnsAccurateResult()
    {
        // Arrange
        decimal meters = 0.5m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(1.11847m, result);
    }

    /// <summary>
    /// Verifies that ConvertMetersPerSecondToMilesPerHour correctly handles large values representing hurricane-force winds.
    /// </summary>
    [TestMethod]
    public void ConvertMetersPerSecondToMilesPerHour_HurricaneForceWinds_ReturnsAccurateResult()
    {
        // Arrange
        decimal meters = 50m;

        // Act
        decimal result = meters.ConvertMetersPerSecondToMilesPerHour();

        // Assert
        Assert.AreEqual(111.847m, result);
    }

    #endregion
}