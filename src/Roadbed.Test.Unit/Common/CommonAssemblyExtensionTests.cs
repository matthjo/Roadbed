namespace Roadbed.Test.Unit.Common;

using Roadbed.Common;
using System.Reflection;

/// <summary>
/// Contains unit tests for verifying the behavior of the CommonAssemblyExtensions class.
/// </summary>
[TestClass]
public class CommonAssemblyExtensionTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that content extracted correctly from embedded resource.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_ExtractContent_ActualEmbeddedResource()
    {
        // Arrange (Given)
        string expectedSnippet = "unit testing";

        // Act (When)
        CommonEmbeddedResourceResponse actualContent = this.GetType().Assembly.ReadTextResource(
            "Roadbed.Test.Unit.Common.Mocks.EmbeddedTextDocument.txt");

        // Assert (Then)
        Assert.IsTrue(
            actualContent.IsReadSuccessful,
            "The embedded file should have been succesfully read.");
        Assert.Contains(
            expectedSnippet,
            actualContent.Data,
            "Expected content missing from embedded file.");
    }

    /// <summary>
    /// Unit test to verify that content extracted correctly from embedded resource.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_ExtractContent_BlankFile()
    {
        // Arrange (Given)

        // Act (When)
        CommonEmbeddedResourceResponse actualContent = this.GetType().Assembly.ReadTextResource(
            string.Empty);

        // Assert (Then)
        Assert.IsFalse(
            actualContent.IsReadSuccessful,
            "The embedded file should have failed to be read.");
        Assert.IsNull(
            actualContent.Data,
            "No content should be found in fake file.");
    }

    /// <summary>
    /// Unit test to verify that content extracted correctly from embedded resource.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_ExtractContent_FakeEmbeddedResource()
    {
        // Arrange (Given)

        // Act (When)
        CommonEmbeddedResourceResponse actualContent = this.GetType().Assembly.ReadTextResource(
            "Roadbed.Test.Unit.Mocks.FakeEmbeddedDocument.txt");

        // Assert (Then)
        Assert.IsFalse(
            actualContent.IsReadSuccessful,
            "The embedded file should have failed to be read.");
        Assert.IsNull(
            actualContent.Data,
            "No content should be found in fake file.");
    }

    /// <summary>
    /// Unit test to verify that ReadTextResource handles null assembly correctly.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_ExtractContent_NullAssembly()
    {
        // Arrange (Given)
        Assembly nullAssembly = null!;
        string fileName = "SomeFile.txt";

        // Act (When)
        CommonEmbeddedResourceResponse actualContent = CommonAssembly.ReadTextResource(
            nullAssembly!,
            fileName);

        // Assert (Then)
        Assert.IsFalse(
            actualContent.IsReadSuccessful,
            "The read should have failed due to null assembly.");
        Assert.IsNull(
            actualContent.Data,
            "No content should be returned for null assembly.");
    }

    /// <summary>
    /// Unit test to verify that IsAssemblyLoaded returns false when assembly name is empty.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_IsAssemblyLoaded_EmptyAssemblyName_ReturnsFalse()
    {
        // Arrange (Given)
        string emptyAssemblyName = string.Empty;

        // Act (When)
        bool decision = CommonAssemblyExtension.IsAssemblyLoaded(emptyAssemblyName);

        // Assert (Then)
        Assert.IsFalse(
            decision,
            "Should return false when assembly name is empty.");
    }

    /// <summary>
    /// Unit test to verify a fake assembly is not loaded.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_IsAssemblyLoaded_FakeAssemblyVerified()
    {
        // Arrange (Given)
        string expectedSnippet = "Roadbed.Fake.Unreal";

        // Act (When)
        bool decision = CommonAssemblyExtension.IsAssemblyLoaded(
            expectedSnippet);

        // Assert (Then)
        Assert.IsFalse(
            decision,
            "The Roadbed.Fake.Unreal assemble is fake and should not be loaded.");
    }

    /// <summary>
    /// Unit test to verify that IsAssemblyLoaded returns false when assembly name is null.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_IsAssemblyLoaded_NullAssemblyName_ReturnsFalse()
    {
        // Arrange (Given)
        string? nullAssemblyName = null;

        // Act (When)
        bool decision = CommonAssemblyExtension.IsAssemblyLoaded(nullAssemblyName!);

        // Assert (Then)
        Assert.IsFalse(
            decision,
            "Should return false when assembly name is null.");
    }

    /// <summary>
    /// Unit test to verify a real assembly is loaded.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_IsAssemblyLoaded_RealAssemblyVerified()
    {
        // Arrange (Given)
        string expectedSnippet = "Roadbed.Common";

        // Act (When)
        bool decision = CommonAssemblyExtension.IsAssemblyLoaded(
            expectedSnippet);

        // Assert (Then)
        Assert.IsTrue(
            decision,
            "The Roadbed.Common assemble is real and should be loaded for the extension to be called.");
    }

    /// <summary>
    /// Unit test to verify that IsAssemblyLoaded returns false after checking all assemblies when none match.
    /// </summary>
    [TestMethod]
    public void CommonAssemblyExtension_IsAssemblyLoaded_SearchesAllAssembliesWithoutMatch()
    {
        // Arrange (Given)
        string nonExistentAssembly = "Roadbed.Completely.NonExistent.Assembly.That.Does.Not.Exist.XYZ123";

        // Act (When)
        bool decision = CommonAssemblyExtension.IsAssemblyLoaded(nonExistentAssembly);

        // Assert (Then)
        Assert.IsFalse(
            decision,
            "Should return false after searching all loaded assemblies without finding a match.");
    }

    #endregion Public Methods
}