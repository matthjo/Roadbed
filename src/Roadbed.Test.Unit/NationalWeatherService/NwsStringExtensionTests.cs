namespace Roadbed.Test.Unit.NationalWeatherService;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Sdk.NationalWeatherService;

/// <summary>
/// Tests for the <see cref="NwsStringExtensions"/> class.
/// </summary>
[TestClass]
public class NwsStringExtensionsTests
{
    #region IsValidWFO Tests

    /// <summary>
    /// Verifies that IsValidWFO returns true when provided with a valid uppercase WFO identifier.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_ValidUppercaseWFO_ReturnsTrue()
    {
        // Arrange
        string wfo = "AKQ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns true when provided with a valid lowercase WFO identifier.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_ValidLowercaseWFO_ReturnsTrue()
    {
        // Arrange
        string wfo = "akq";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns true when provided with a valid mixed case WFO identifier.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_ValidMixedCaseWFO_ReturnsTrue()
    {
        // Arrange
        string wfo = "AkQ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with an invalid WFO identifier.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_InvalidWFO_ReturnsFalse()
    {
        // Arrange
        string wfo = "XYZ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with a null string.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_NullString_ReturnsFalse()
    {
        // Arrange
        string? wfo = null;

        // Act
        bool result = wfo!.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with an empty string.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_EmptyString_ReturnsFalse()
    {
        // Arrange
        string wfo = string.Empty;

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with a whitespace-only string.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_WhitespaceString_ReturnsFalse()
    {
        // Arrange
        string wfo = "   ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns true for the first WFO identifier in the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_FirstWFOInList_ReturnsTrue()
    {
        // Arrange
        string wfo = "AKQ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns true for the last WFO identifier in the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_LastWFOInList_ReturnsTrue()
    {
        // Arrange
        string wfo = "ONP";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns true for a WFO identifier in the middle of the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_MiddleWFOInList_ReturnsTrue()
    {
        // Arrange
        string wfo = "GRR";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when the WFO identifier has leading whitespace.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_WFOWithLeadingWhitespace_ReturnsFalse()
    {
        // Arrange
        string wfo = " AKQ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when the WFO identifier has trailing whitespace.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_WFOWithTrailingWhitespace_ReturnsFalse()
    {
        // Arrange
        string wfo = "AKQ ";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with a two-character string.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_TwoCharacterString_ReturnsFalse()
    {
        // Arrange
        string wfo = "AK";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidWFO returns false when provided with a four-character string.
    /// </summary>
    [TestMethod]
    public void IsValidWFO_FourCharacterString_ReturnsFalse()
    {
        // Arrange
        string wfo = "AKQX";

        // Act
        bool result = wfo.IsValidWFO();

        // Assert
        Assert.IsFalse(result);
    }

    #endregion

    #region IsValidState Tests

    /// <summary>
    /// Verifies that IsValidState returns true when provided with a valid uppercase state identifier.
    /// </summary>
    [TestMethod]
    public void IsValidState_ValidUppercaseState_ReturnsTrue()
    {
        // Arrange
        string state = "AL";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true when provided with a valid lowercase state identifier.
    /// </summary>
    [TestMethod]
    public void IsValidState_ValidLowercaseState_ReturnsTrue()
    {
        // Arrange
        string state = "al";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true when provided with a valid mixed case state identifier.
    /// </summary>
    [TestMethod]
    public void IsValidState_ValidMixedCaseState_ReturnsTrue()
    {
        // Arrange
        string state = "Al";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with an invalid state identifier.
    /// </summary>
    [TestMethod]
    public void IsValidState_InvalidState_ReturnsFalse()
    {
        // Arrange
        string state = "ZZ";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with a null string.
    /// </summary>
    [TestMethod]
    public void IsValidState_NullString_ReturnsFalse()
    {
        // Arrange
        string? state = null;

        // Act
        bool result = state!.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with an empty string.
    /// </summary>
    [TestMethod]
    public void IsValidState_EmptyString_ReturnsFalse()
    {
        // Arrange
        string state = string.Empty;

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with a whitespace-only string.
    /// </summary>
    [TestMethod]
    public void IsValidState_WhitespaceString_ReturnsFalse()
    {
        // Arrange
        string state = "   ";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for the first state identifier in the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidState_FirstStateInList_ReturnsTrue()
    {
        // Arrange
        string state = "AL";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for the last state identifier in the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidState_LastStateInList_ReturnsTrue()
    {
        // Arrange
        string state = "WY";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for a state identifier in the middle of the valid list.
    /// </summary>
    [TestMethod]
    public void IsValidState_MiddleStateInList_ReturnsTrue()
    {
        // Arrange
        string state = "NJ";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for Guam territory identifier (GU).
    /// </summary>
    [TestMethod]
    public void IsValidState_TerritoryGuam_ReturnsTrue()
    {
        // Arrange
        string state = "GU";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for Puerto Rico territory identifier (PR).
    /// </summary>
    [TestMethod]
    public void IsValidState_TerritoryPuertoRico_ReturnsTrue()
    {
        // Arrange
        string state = "PR";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for Virgin Islands territory identifier (VI).
    /// </summary>
    [TestMethod]
    public void IsValidState_TerritoryVirginIslands_ReturnsTrue()
    {
        // Arrange
        string state = "VI";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for American Samoa territory identifier (AS).
    /// </summary>
    [TestMethod]
    public void IsValidState_TerritoryAmericanSamoa_ReturnsTrue()
    {
        // Arrange
        string state = "AS";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns true for District of Columbia identifier (DC).
    /// </summary>
    [TestMethod]
    public void IsValidState_DistrictOfColumbia_ReturnsTrue()
    {
        // Arrange
        string state = "DC";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when the state identifier has leading whitespace.
    /// </summary>
    [TestMethod]
    public void IsValidState_StateWithLeadingWhitespace_ReturnsFalse()
    {
        // Arrange
        string state = " AL";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when the state identifier has trailing whitespace.
    /// </summary>
    [TestMethod]
    public void IsValidState_StateWithTrailingWhitespace_ReturnsFalse()
    {
        // Arrange
        string state = "AL ";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with a single-character string.
    /// </summary>
    [TestMethod]
    public void IsValidState_SingleCharacterString_ReturnsFalse()
    {
        // Arrange
        string state = "A";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with a three-character string.
    /// </summary>
    [TestMethod]
    public void IsValidState_ThreeCharacterString_ReturnsFalse()
    {
        // Arrange
        string state = "ALA";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that IsValidState returns false when provided with a numeric string.
    /// </summary>
    [TestMethod]
    public void IsValidState_NumericString_ReturnsFalse()
    {
        // Arrange
        string state = "12";

        // Act
        bool result = state.IsValidState();

        // Assert
        Assert.IsFalse(result);
    }

    #endregion
}