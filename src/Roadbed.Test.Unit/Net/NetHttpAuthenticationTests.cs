namespace Roadbed.Test.Unit.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpAuthentication class.
/// </summary>
[TestClass]
public class NetHttpAuthenticationTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that AuthenticationType property can be set to each enum value.
    /// </summary>
    [TestMethod]
    public void AuthenticationType_SetEachEnumValue_ReturnsCorrectValue()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();

        // Act & Assert (When & Then)
        foreach (NetHttpAuthenticationType authType in Enum.GetValues(typeof(NetHttpAuthenticationType)))
        {
            authentication.AuthenticationType = authType;
            Assert.AreEqual(
                authType,
                authentication.AuthenticationType,
                $"AuthenticationType should return {authType} when set to that value.");
        }
    }

    /// <summary>
    /// Unit test to verify that AuthenticationType property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void AuthenticationType_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedType = NetHttpAuthenticationType.Bearer;

        // Act (When)
        authentication.AuthenticationType = expectedType;

        // Assert (Then)
        Assert.AreEqual(
            expectedType,
            authentication.AuthenticationType,
            "AuthenticationType should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that the default constructor initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithDefaultValues()
    {
        // Arrange (Given)

        // Act (When)
        var authentication = new NetHttpAuthentication();

        // Assert (Then)
        Assert.AreEqual(
            default(NetHttpAuthenticationType),
            authentication.AuthenticationType,
            "AuthenticationType should be initialized to default value.");
        Assert.IsNull(
            authentication.Value,
            "Value should be initialized to null.");
    }

    /// <summary>
    /// Unit test to verify that properties can be modified multiple times.
    /// </summary>
    [TestMethod]
    public void Properties_ModifyMultipleTimes_ReturnsLatestValues()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var initialType = NetHttpAuthenticationType.Bearer;
        var finalType = NetHttpAuthenticationType.Basic;
        var initialValue = "initial-token";
        var finalValue = "final-token";

        // Act (When)
        authentication.AuthenticationType = initialType;
        authentication.Value = initialValue;
        authentication.AuthenticationType = finalType;
        authentication.Value = finalValue;

        // Assert (Then)
        Assert.AreEqual(
            finalType,
            authentication.AuthenticationType,
            "AuthenticationType should return the latest value that was set.");
        Assert.AreEqual(
            finalValue,
            authentication.Value,
            "Value should return the latest string that was set.");
    }

    /// <summary>
    /// Unit test to verify that both properties can be set independently.
    /// </summary>
    [TestMethod]
    public void Properties_SetBothIndependently_ReturnCorrectValues()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedType = NetHttpAuthenticationType.Bearer;
        var expectedValue = "bearer-token-xyz";

        // Act (When)
        authentication.AuthenticationType = expectedType;
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedType,
            authentication.AuthenticationType,
            "AuthenticationType should return the value that was set.");
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should return the string that was set.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set to empty string.
    /// </summary>
    [TestMethod]
    public void Value_SetEmptyString_ReturnsEmptyString()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedValue = string.Empty;

        // Act (When)
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should return empty string when set to empty string.");
    }

    /// <summary>
    /// Unit test to verify that Value property can handle long strings.
    /// </summary>
    [TestMethod]
    public void Value_SetLongString_ReturnsLongString()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedValue = new string('a', 1000);

        // Act (When)
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should correctly handle and return long strings.");
        Assert.AreEqual(
            1000,
            authentication.Value?.Length,
            "Value length should match the set string length.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set to null.
    /// </summary>
    [TestMethod]
    public void Value_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        authentication.Value = "initial-value";

        // Act (When)
        authentication.Value = null;

        // Assert (Then)
        Assert.IsNull(
            authentication.Value,
            "Value should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that Value property can handle special characters.
    /// </summary>
    [TestMethod]
    public void Value_SetSpecialCharacters_ReturnsSpecialCharacters()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedValue = "token!@#$%^&*()_+-={}[]|:;<>?,./";

        // Act (When)
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should correctly handle and return special characters.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set and retrieved with valid string.
    /// </summary>
    [TestMethod]
    public void Value_SetValidString_ReturnsSetValue()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedValue = "test-token-12345";

        // Act (When)
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should return the string that was set.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set to whitespace string.
    /// </summary>
    [TestMethod]
    public void Value_SetWhitespaceString_ReturnsWhitespaceString()
    {
        // Arrange (Given)
        var authentication = new NetHttpAuthentication();
        var expectedValue = "   ";

        // Act (When)
        authentication.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            authentication.Value,
            "Value should return whitespace string when set to whitespace.");
    }

    #endregion Public Methods
}