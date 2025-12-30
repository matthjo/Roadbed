namespace Roadbed.Test.Unit.Common;

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Common;

/// <summary>
/// Unit tests for the CommonEmbeddedResourceResponse class.
/// </summary>
[TestClass]
public class CommonEmbeddedResourceResponseTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that Data property getter returns the correct value.
    /// </summary>
    [TestMethod]
    public void DataProperty_Getter_ReturnsCorrectValue()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);
        string data = response.Data;

        // Assert
        Assert.IsNotNull(data, "Data property should return non-null value for successful read.");
        Assert.IsGreaterThan(0, data.Length, "Data property should return non-empty content.");
    }

    /// <summary>
    /// Verifies that ErrorMessage property getter returns the correct value for failure.
    /// </summary>
    [TestMethod]
    public void ErrorMessageProperty_Getter_ReturnsCorrectValueForFailure()
    {
        // Arrange
        Assembly? nullAssembly = null;
        string fileName = "SomeFile.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(nullAssembly!, fileName);
        string errorMessage = response.ErrorMessage!;

        // Assert
        Assert.IsNotNull(errorMessage, "ErrorMessage property should return non-null value for failure.");
        Assert.AreEqual("Assembly is null.", errorMessage, "ErrorMessage should match the expected error.");
    }

    /// <summary>
    /// Verifies that ErrorMessage property getter returns empty string for success.
    /// </summary>
    [TestMethod]
    public void ErrorMessageProperty_Getter_ReturnsEmptyStringForSuccess()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);
        string errorMessage = response.ErrorMessage!;

        // Assert
        Assert.AreEqual(string.Empty, errorMessage, "ErrorMessage should be empty for successful read.");
    }

    /// <summary>
    /// Verifies that a failed response due to empty filename has null Data property.
    /// </summary>
    [TestMethod]
    public void Failure_WithEmptyFilename_SetsDataToNull()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string emptyFileName = string.Empty;

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, emptyFileName);

        // Assert
        Assert.IsNull(response.Data, "Data should be null for empty filename.");
    }

    /// <summary>
    /// Verifies that a failed response due to empty filename has appropriate ErrorMessage.
    /// </summary>
    [TestMethod]
    public void Failure_WithEmptyFilename_SetsErrorMessageCorrectly()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string emptyFileName = string.Empty;

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, emptyFileName);

        // Assert
        Assert.AreEqual(
            "File name is null or empty.",
            response.ErrorMessage,
            "ErrorMessage should indicate that the filename is null or empty.");
    }

    /// <summary>
    /// Verifies that a failed response due to empty filename has IsReadSuccessful set to false.
    /// </summary>
    [TestMethod]
    public void Failure_WithEmptyFilename_SetsIsReadSuccessfulToFalse()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string emptyFileName = string.Empty;

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, emptyFileName);

        // Assert
        Assert.IsFalse(response.IsReadSuccessful, "IsReadSuccessful should be false for empty filename.");
    }

    /// <summary>
    /// Verifies that a failed response due to file not found has null Data property.
    /// </summary>
    [TestMethod]
    public void Failure_WithFileNotFound_SetsDataToNull()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string nonExistentFile = "Roadbed.Test.Unit.Mocks.NonExistent.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, nonExistentFile);

        // Assert
        Assert.IsNull(response.Data, "Data should be null for non-existent file.");
    }

    /// <summary>
    /// Verifies that a failed response due to file not found has appropriate ErrorMessage.
    /// </summary>
    [TestMethod]
    public void Failure_WithFileNotFound_SetsErrorMessageCorrectly()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string nonExistentFile = "Roadbed.Test.Unit.Mocks.NonExistent.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, nonExistentFile);

        // Assert
        Assert.AreEqual(
            "File not found or it is empty.",
            response.ErrorMessage,
            "ErrorMessage should indicate that the file was not found or is empty.");
    }

    /// <summary>
    /// Verifies that a failed response due to file not found has IsReadSuccessful set to false.
    /// </summary>
    [TestMethod]
    public void Failure_WithFileNotFound_SetsIsReadSuccessfulToFalse()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string nonExistentFile = "Roadbed.Test.Unit.Mocks.NonExistent.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, nonExistentFile);

        // Assert
        Assert.IsFalse(response.IsReadSuccessful, "IsReadSuccessful should be false for non-existent file.");
    }

    /// <summary>
    /// Verifies that a failed response due to null assembly has null Data property.
    /// </summary>
    [TestMethod]
    public void Failure_WithNullAssembly_SetsDataToNull()
    {
        // Arrange
        Assembly? nullAssembly = null;
        string fileName = "SomeFile.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(nullAssembly!, fileName);

        // Assert
        Assert.IsNull(response.Data, "Data should be null for a failed read.");
    }

    /// <summary>
    /// Verifies that a failed response due to null assembly has appropriate ErrorMessage.
    /// </summary>
    [TestMethod]
    public void Failure_WithNullAssembly_SetsErrorMessageCorrectly()
    {
        // Arrange
        Assembly? nullAssembly = null;
        string fileName = "SomeFile.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(nullAssembly!, fileName);

        // Assert
        Assert.AreEqual(
            "Assembly is null.",
            response.ErrorMessage,
            "ErrorMessage should indicate that the assembly is null.");
    }

    /// <summary>
    /// Verifies that a failed response due to null assembly has IsReadSuccessful set to false.
    /// </summary>
    [TestMethod]
    public void Failure_WithNullAssembly_SetsIsReadSuccessfulToFalse()
    {
        // Arrange
        Assembly? nullAssembly = null;
        string fileName = "SomeFile.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(nullAssembly!, fileName);

        // Assert
        Assert.IsFalse(response.IsReadSuccessful, "IsReadSuccessful should be false for null assembly.");
    }

    /// <summary>
    /// Verifies that a failure response has consistent property values.
    /// </summary>
    [TestMethod]
    public void Integration_FailureResponse_HasConsistentProperties()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string nonExistentFile = "Roadbed.Test.Unit.Mocks.DoesNotExist.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, nonExistentFile);

        // Assert
        Assert.IsFalse(response.IsReadSuccessful, "IsReadSuccessful should be false.");
        Assert.IsNull(response.Data, "Data should be null.");
        Assert.IsNotNull(response.ErrorMessage, "ErrorMessage should not be null.");
        Assert.IsGreaterThan(0, response.ErrorMessage.Length, "ErrorMessage should contain text.");
    }

    /// <summary>
    /// Verifies that multiple failure scenarios produce appropriate responses.
    /// </summary>
    [TestMethod]
    public void Integration_MultipleFailureScenarios_ProduceAppropriateResponses()
    {
        // Arrange
        Assembly? nullAssembly = null;
        Assembly validAssembly = this.GetType().Assembly;
        string validFileName = "SomeFile.txt";
        string emptyFileName = string.Empty;

        // Act
        CommonEmbeddedResourceResponse response1 = CommonAssembly.ReadTextResource(nullAssembly!, validFileName);
        CommonEmbeddedResourceResponse response2 = CommonAssembly.ReadTextResource(validAssembly, emptyFileName);
        CommonEmbeddedResourceResponse response3 = CommonAssembly.ReadTextResource(validAssembly, "NonExistent.txt");

        // Assert - All should be failures
        Assert.IsFalse(response1.IsReadSuccessful, "Response1 should be a failure.");
        Assert.IsFalse(response2.IsReadSuccessful, "Response2 should be a failure.");
        Assert.IsFalse(response3.IsReadSuccessful, "Response3 should be a failure.");

        // Assert - All should have null Data
        Assert.IsNull(response1.Data, "Response1 Data should be null.");
        Assert.IsNull(response2.Data, "Response2 Data should be null.");
        Assert.IsNull(response3.Data, "Response3 Data should be null.");

        // Assert - All should have different error messages
        Assert.AreEqual("Assembly is null.", response1.ErrorMessage);
        Assert.AreEqual("File name is null or empty.", response2.ErrorMessage);
        Assert.AreEqual("File not found or it is empty.", response3.ErrorMessage);
    }

    /// <summary>
    /// Verifies that a successful response has consistent property values.
    /// </summary>
    [TestMethod]
    public void Integration_SuccessResponse_HasConsistentProperties()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);

        // Assert
        Assert.IsTrue(response.IsReadSuccessful, "IsReadSuccessful should be true.");
        Assert.IsNotNull(response.Data, "Data should not be null.");
        Assert.AreEqual(string.Empty, response.ErrorMessage, "ErrorMessage should be empty.");
        Assert.IsGreaterThan(0, response.Data.Length, "Data should contain content.");
    }

    /// <summary>
    /// Verifies that IsReadSuccessful property getter returns false for failure.
    /// </summary>
    [TestMethod]
    public void IsReadSuccessfulProperty_Getter_ReturnsFalseForFailure()
    {
        // Arrange
        Assembly? nullAssembly = null;
        string fileName = "SomeFile.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(nullAssembly!, fileName);
        bool isSuccess = response.IsReadSuccessful;

        // Assert
        Assert.IsFalse(isSuccess, "IsReadSuccessful property should return false for failed read.");
    }

    /// <summary>
    /// Verifies that IsReadSuccessful property getter returns true for success.
    /// </summary>
    [TestMethod]
    public void IsReadSuccessfulProperty_Getter_ReturnsTrueForSuccess()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);
        bool isSuccess = response.IsReadSuccessful;

        // Assert
        Assert.IsTrue(isSuccess, "IsReadSuccessful property should return true for successful read.");
    }

    /// <summary>
    /// Verifies that a successful response contains the expected data content.
    /// </summary>
    [TestMethod]
    public void Success_Response_ContainsExpectedDataContent()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";
        string expectedSnippet = "unit testing";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);

        // Assert
        Assert.Contains(
            expectedSnippet,
            response.Data,
            "Data should contain the expected content from the embedded resource.");
    }

    /// <summary>
    /// Verifies that a successful response has non-null Data property.
    /// </summary>
    [TestMethod]
    public void Success_Response_SetsDataToNonNull()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);

        // Assert
        Assert.IsNotNull(response.Data, "Data should not be null for a successful read.");
    }

    /// <summary>
    /// Verifies that a successful response has empty ErrorMessage.
    /// </summary>
    [TestMethod]
    public void Success_Response_SetsErrorMessageToEmpty()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);

        // Assert
        Assert.AreEqual(string.Empty, response.ErrorMessage, "ErrorMessage should be empty for a successful read.");
    }

    /// <summary>
    /// Verifies that a successful response has IsReadSuccessful set to true.
    /// </summary>
    [TestMethod]
    public void Success_Response_SetsIsReadSuccessfulToTrue()
    {
        // Arrange
        Assembly assembly = this.GetType().Assembly;
        string validResource = "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt";

        // Act
        CommonEmbeddedResourceResponse response = CommonAssembly.ReadTextResource(assembly, validResource);

        // Assert
        Assert.IsTrue(response.IsReadSuccessful, "IsReadSuccessful should be true for a successful read.");
    }

    #endregion Public Methods
}