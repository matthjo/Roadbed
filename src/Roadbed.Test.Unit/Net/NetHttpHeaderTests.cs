namespace Roadbed.Test.Unit.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpHeader record class.
/// </summary>
[TestClass]
public class NetHttpHeaderTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the parameterized constructor handles empty strings.
    /// </summary>
    [TestMethod]
    public void Constructor_EmptyStrings_InitializesWithEmptyStrings()
    {
        // Arrange (Given)
        string emptyName = string.Empty;
        string emptyValue = string.Empty;

        // Act (When)
        var header = new NetHttpHeader(emptyName, emptyValue);

        // Assert (Then)
        Assert.AreEqual(
            emptyName,
            header.Name,
            "Name should be empty string when empty string is provided.");
        Assert.AreEqual(
            emptyValue,
            header.Value,
            "Value should be empty string when empty string is provided.");
    }

    /// <summary>
    /// Unit test to verify that the parameterless constructor initializes properties to null.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithNullValues()
    {
        // Arrange (Given)

        // Act (When)
        var header = new NetHttpHeader();

        // Assert (Then)
        Assert.IsNull(
            header.Name,
            "Name should be initialized to null.");
        Assert.IsNull(
            header.Value,
            "Value should be initialized to null.");
    }

    /// <summary>
    /// Unit test to verify that the parameterized constructor handles null name parameter.
    /// </summary>
    [TestMethod]
    public void Constructor_NullName_InitializesNameToNull()
    {
        // Arrange (Given)
        string? nullName = null;
        string value = "application/json";

        // Act (When)
        var header = new NetHttpHeader(nullName!, value);

        // Assert (Then)
        Assert.IsNull(
            header.Name,
            "Name should be null when null is provided.");
        Assert.AreEqual(
            value,
            header.Value,
            "Value should be initialized to the provided value.");
    }

    /// <summary>
    /// Unit test to verify that the parameterized constructor handles null value parameter.
    /// </summary>
    [TestMethod]
    public void Constructor_NullValue_InitializesValueToNull()
    {
        // Arrange (Given)
        string name = "Authorization";
        string? nullValue = null;

        // Act (When)
        var header = new NetHttpHeader(name, nullValue!);

        // Assert (Then)
        Assert.AreEqual(
            name,
            header.Name,
            "Name should be initialized to the provided value.");
        Assert.IsNull(
            header.Value,
            "Value should be null when null is provided.");
    }

    /// <summary>
    /// Unit test to verify that the parameterized constructor initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidNameAndValue_InitializesWithProvidedValues()
    {
        // Arrange (Given)
        string expectedName = "Content-Type";
        string expectedValue = "application/json";

        // Act (When)
        var header = new NetHttpHeader(expectedName, expectedValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedName,
            header.Name,
            "Name should be initialized to the provided value.");
        Assert.AreEqual(
            expectedValue,
            header.Value,
            "Value should be initialized to the provided value.");
    }

    /// <summary>
    /// Unit test to verify that two records with null values are equal.
    /// </summary>
    [TestMethod]
    public void Equality_BothNull_ReturnsTrue()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader(null!, null!);
        var header2 = new NetHttpHeader(null!, null!);

        // Act (When)
        bool areEqual = header1 == header2;

        // Assert (Then)
        Assert.IsTrue(
            areEqual,
            "Records with null values should be equal.");
    }

    /// <summary>
    /// Unit test to verify that two records with different names are not equal.
    /// </summary>
    [TestMethod]
    public void Equality_DifferentNames_ReturnsFalse()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Authorization", "application/json");

        // Act (When)
        bool areEqual = header1 == header2;

        // Assert (Then)
        Assert.IsFalse(
            areEqual,
            "Records with different names should not be equal.");
    }

    /// <summary>
    /// Unit test to verify that two records with different values are not equal.
    /// </summary>
    [TestMethod]
    public void Equality_DifferentValues_ReturnsFalse()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Content-Type", "text/html");

        // Act (When)
        bool areEqual = header1 == header2;

        // Assert (Then)
        Assert.IsFalse(
            areEqual,
            "Records with different values should not be equal.");
    }

    /// <summary>
    /// Unit test to verify that two records with same values are equal.
    /// </summary>
    [TestMethod]
    public void Equality_SameValues_ReturnsTrue()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Content-Type", "application/json");

        // Act (When)
        bool areEqual = header1 == header2;
        bool areEqualUsingEquals = header1.Equals(header2);

        // Assert (Then)
        Assert.IsTrue(
            areEqual,
            "Records with same values should be equal using == operator.");
        Assert.IsTrue(
            areEqualUsingEquals,
            "Records with same values should be equal using Equals method.");
    }

    /// <summary>
    /// Unit test to verify that GetHashCode returns different value for different records.
    /// </summary>
    [TestMethod]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Authorization", "Bearer token");

        // Act (When)
        int hash1 = header1.GetHashCode();
        int hash2 = header2.GetHashCode();

        // Assert (Then)
        Assert.AreNotEqual(
            hash1,
            hash2,
            "Records with different values should have different hash codes.");
    }

    /// <summary>
    /// Unit test to verify that GetHashCode returns same value for equal records.
    /// </summary>
    [TestMethod]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange (Given)
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Content-Type", "application/json");

        // Act (When)
        int hash1 = header1.GetHashCode();
        int hash2 = header2.GetHashCode();

        // Assert (Then)
        Assert.AreEqual(
            hash1,
            hash2,
            "Records with same values should have same hash code.");
    }

    /// <summary>
    /// Unit test to verify that Name property can be set to null.
    /// </summary>
    [TestMethod]
    public void Name_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var header = new NetHttpHeader("Initial-Name", "value");

        // Act (When)
        header.Name = null;

        // Assert (Then)
        Assert.IsNull(
            header.Name,
            "Name should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that Name property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void Name_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var header = new NetHttpHeader();
        string expectedName = "X-Custom-Header";

        // Act (When)
        header.Name = expectedName;

        // Assert (Then)
        Assert.AreEqual(
            expectedName,
            header.Name,
            "Name should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that properties can handle common HTTP header names.
    /// </summary>
    [TestMethod]
    public void Properties_CommonHttpHeaders_StoresCorrectly()
    {
        // Arrange (Given)
        var headers = new[]
        {
            new NetHttpHeader("Content-Type", "application/json"),
            new NetHttpHeader("Authorization", "Bearer token"),
            new NetHttpHeader("Accept", "application/json"),
            new NetHttpHeader("User-Agent", "Mozilla/5.0"),
            new NetHttpHeader("Cache-Control", "no-cache"),
        };

        // Act & Assert (When & Then)
        foreach (var header in headers)
        {
            Assert.IsNotNull(
                header.Name,
                "Name should not be null for common HTTP headers.");
            Assert.IsNotNull(
                header.Value,
                "Value should not be null for common HTTP headers.");
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(header.Name),
                "Name should not be empty or whitespace.");
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(header.Value),
                "Value should not be empty or whitespace.");
        }
    }

    /// <summary>
    /// Unit test to verify that properties can handle special characters.
    /// </summary>
    [TestMethod]
    public void Properties_SetSpecialCharacters_ReturnsSpecialCharacters()
    {
        // Arrange (Given)
        string specialName = "X-Custom-Header!@#$%";
        string specialValue = "value!@#$%^&*()";

        // Act (When)
        var header = new NetHttpHeader(specialName, specialValue);

        // Assert (Then)
        Assert.AreEqual(
            specialName,
            header.Name,
            "Name should correctly handle and return special characters.");
        Assert.AreEqual(
            specialValue,
            header.Value,
            "Value should correctly handle and return special characters.");
    }

    /// <summary>
    /// Unit test to verify that ToString returns expected format.
    /// </summary>
    [TestMethod]
    public void ToString_ValidValues_ReturnsFormattedString()
    {
        // Arrange (Given)
        var header = new NetHttpHeader("Content-Type", "application/json");

        // Act (When)
        string result = header.ToString();

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "ToString should not return null.");
        Assert.Contains("Content-Type", result, "ToString should contain the Name value.");
        Assert.Contains("application/json", result, "ToString should contain the Value value.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set to null.
    /// </summary>
    [TestMethod]
    public void Value_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var header = new NetHttpHeader("name", "initial-value");

        // Act (When)
        header.Value = null;

        // Assert (Then)
        Assert.IsNull(
            header.Value,
            "Value should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that Value property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void Value_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var header = new NetHttpHeader();
        string expectedValue = "custom-value";

        // Act (When)
        header.Value = expectedValue;

        // Assert (Then)
        Assert.AreEqual(
            expectedValue,
            header.Value,
            "Value should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that with-expression creates new record with modified property.
    /// </summary>
    [TestMethod]
    public void WithExpression_ModifyName_CreatesNewRecordWithModifiedName()
    {
        // Arrange (Given)
        var originalHeader = new NetHttpHeader("Content-Type", "application/json");
        string newName = "Authorization";

        // Act (When)
        var modifiedHeader = new NetHttpHeader
        {
            Name = newName,
            Value = originalHeader.Value,
        };

        // Assert (Then)
        Assert.AreEqual(
            "Content-Type",
            originalHeader.Name,
            "Original record Name should remain unchanged.");
        Assert.AreEqual(
            newName,
            modifiedHeader.Name,
            "Modified record Name should have the new value.");
        Assert.AreEqual(
            originalHeader.Value,
            modifiedHeader.Value,
            "Modified record Value should remain the same as original.");
    }

    /// <summary>
    /// Unit test to verify that with-expression creates new record with modified value.
    /// </summary>
    [TestMethod]
    public void WithExpression_ModifyValue_CreatesNewRecordWithModifiedValue()
    {
        // Arrange (Given)
        var originalHeader = new NetHttpHeader("Content-Type", "application/json");
        string newValue = "text/html";

        // Act (When)
        var modifiedHeader = new NetHttpHeader
        {
            Name = originalHeader.Name,
            Value = newValue,
        };

        // Assert (Then)
        Assert.AreEqual(
            "application/json",
            originalHeader.Value,
            "Original record Value should remain unchanged.");
        Assert.AreEqual(
            newValue,
            modifiedHeader.Value,
            "Modified record Value should have the new value.");
        Assert.AreEqual(
            originalHeader.Name,
            modifiedHeader.Name,
            "Modified record Name should remain the same as original.");
    }

    #endregion Public Methods
}