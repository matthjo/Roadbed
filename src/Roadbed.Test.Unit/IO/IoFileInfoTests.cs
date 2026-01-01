namespace Roadbed.Test.Unit.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.IO;
using System.IO;

/// <summary>
/// Contains unit tests for verifying the behavior of the IoFileInfo class.
/// </summary>
[TestClass]
public class IoFileInfoTests
{
    #region Public Methods

    #region Constructor Tests

    /// <summary>
    /// Unit test to verify that the parameterless constructor initializes with null values.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithNullValues()
    {
        // Arrange (Given)

        // Act (When)
        var instance = new IoFileInfo();

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.IsNull(
            instance.FullPath,
            "FullPath should be null when using parameterless constructor.");
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when using parameterless constructor.");
        Assert.IsNull(
            instance.Extension,
            "Extension should be null when using parameterless constructor.");
    }

    /// <summary>
    /// Unit test to verify that the constructor with path parameter initializes FullPath correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidPath_InitializesFullPath()
    {
        // Arrange (Given)
        string testPath = @"C:\TestFolder\TestFile.txt";

        // Act (When)
        var instance = new IoFileInfo(testPath);

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.IsNotNull(
            instance.FullPath,
            "FullPath should not be null when initialized with valid path.");
        Assert.IsNotNull(
            instance.FileInfo,
            "FileInfo should not be null when initialized with valid path.");
    }

    #endregion Constructor Tests

    #region FullPath Property Tests

    /// <summary>
    /// Unit test to verify that setting FullPath to a valid value updates the property.
    /// </summary>
    [TestMethod]
    public void FullPath_SetValidValue_UpdatesProperty()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();
        string expectedFile = @"TestFile.txt";
        string testPath = string.Concat(@"C:\TestFolder\", expectedFile);

        // Act (When)
        instance.FullPath = testPath;

        // Assert (Then)
        Assert.IsNotNull(
            instance.FullPath,
            "FullPath should not be null after being set.");
        Assert.Contains(
            expectedFile,
            instance.FullPath,
            "FullPath should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that setting FullPath to null sets FileInfo to null.
    /// </summary>
    [TestMethod]
    public void FullPath_SetNull_SetsFileInfoToNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo(@"C:\TestFolder\TestFile.txt");

        // Act (When)
        instance.FullPath = null;

        // Assert (Then)
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when FullPath is set to null.");
        Assert.IsNull(
            instance.FullPath,
            "FullPath should be null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that setting FullPath to empty string sets FileInfo to null.
    /// </summary>
    [TestMethod]
    public void FullPath_SetEmptyString_SetsFileInfoToNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo(@"C:\TestFolder\TestFile.txt");

        // Act (When)
        instance.FullPath = string.Empty;

        // Assert (Then)
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when FullPath is set to empty string.");
        Assert.IsNull(
            instance.FullPath,
            "FullPath should be null when set to empty string.");
    }

    /// <summary>
    /// Unit test to verify that FullPath returns null when FileInfo is null.
    /// </summary>
    [TestMethod]
    public void FullPath_FileInfoIsNull_ReturnsNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();

        // Act (When)
        string? result = instance.FullPath;

        // Assert (Then)
        Assert.IsNull(
            result,
            "FullPath should return null when FileInfo is null.");
    }

    #endregion FullPath Property Tests

    #region Extension Property Tests

    /// <summary>
    /// Unit test to verify that Extension returns null when FullPath is null.
    /// </summary>
    [TestMethod]
    public void Extension_FullPathIsNull_ReturnsNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNull(
            result,
            "Extension should return null when FullPath is null.");
    }

    /// <summary>
    /// Unit test to verify that Extension returns null when FullPath is empty.
    /// </summary>
    [TestMethod]
    public void Extension_FullPathIsEmpty_ReturnsNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();
        instance.FullPath = string.Empty;

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNull(
            result,
            "Extension should return null when FullPath is empty.");
    }

    /// <summary>
    /// Unit test to verify that Extension returns null when FileInfo is null.
    /// </summary>
    [TestMethod]
    public void Extension_FileInfoIsNull_ReturnsNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNull(
            result,
            "Extension should return null when FileInfo is null.");
    }

    /// <summary>
    /// Unit test to verify that Extension returns correct value for file with extension.
    /// </summary>
    [TestMethod]
    public void Extension_ValidFileWithExtension_ReturnsCorrectExtension()
    {
        // Arrange (Given)
        string testPath = @"C:\TestFolder\TestFile.txt";
        var instance = new IoFileInfo(testPath);
        string expectedExtension = ".txt";

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "Extension should not be null for valid file path with extension.");
        Assert.AreEqual(
            expectedExtension,
            result,
            "Extension should return the correct file extension.");
    }

    /// <summary>
    /// Unit test to verify that Extension returns correct value for file with multiple dots.
    /// </summary>
    [TestMethod]
    public void Extension_FileWithMultipleDots_ReturnsLastExtension()
    {
        // Arrange (Given)
        string testPath = @"C:\TestFolder\TestFile.backup.txt";
        var instance = new IoFileInfo(testPath);
        string expectedExtension = ".txt";

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "Extension should not be null for valid file path with extension.");
        Assert.AreEqual(
            expectedExtension,
            result,
            "Extension should return only the last extension when file has multiple dots.");
    }

    /// <summary>
    /// Unit test to verify that Extension returns empty string for file without extension.
    /// </summary>
    [TestMethod]
    public void Extension_FileWithoutExtension_ReturnsEmptyString()
    {
        // Arrange (Given)
        string testPath = @"C:\TestFolder\TestFile";
        var instance = new IoFileInfo(testPath);

        // Act (When)
        string? result = instance.Extension;

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "Extension should not be null for valid file path.");
        Assert.AreEqual(
            string.Empty,
            result,
            "Extension should return empty string when file has no extension.");
    }

    #endregion Extension Property Tests

    #region FileInfo Property Tests

    /// <summary>
    /// Unit test to verify that FileInfo is null after parameterless constructor.
    /// </summary>
    [TestMethod]
    public void FileInfo_ParameterlessConstructor_IsNull()
    {
        // Arrange (Given)
        var instance = new IoFileInfo();

        // Act (When)
        FileInfo? result = instance.FileInfo;

        // Assert (Then)
        Assert.IsNull(
            result,
            "FileInfo should be null after using parameterless constructor.");
    }

    /// <summary>
    /// Unit test to verify that FileInfo is created when path is provided to constructor.
    /// </summary>
    [TestMethod]
    public void FileInfo_PathProvidedToConstructor_IsNotNull()
    {
        // Arrange (Given)
        string testPath = @"C:\TestFolder\TestFile.txt";

        // Act (When)
        var instance = new IoFileInfo(testPath);

        // Assert (Then)
        Assert.IsNotNull(
            instance.FileInfo,
            "FileInfo should not be null when path is provided to constructor.");
    }

    #endregion FileInfo Property Tests

    #region Integration Tests

    /// <summary>
    /// Unit test to verify that changing FullPath updates both FileInfo and Extension.
    /// </summary>
    [TestMethod]
    public void FullPath_ChangedValue_UpdatesFileInfoAndExtension()
    {
        // Arrange (Given)
        var instance = new IoFileInfo(@"C:\TestFolder\OldFile.txt");
        string newPath = @"C:\TestFolder\NewFile.doc";
        string expectedExtension = ".doc";

        // Act (When)
        instance.FullPath = newPath;

        // Assert (Then)
        Assert.AreEqual(
            expectedExtension,
            instance.Extension,
            "Extension should reflect the new file extension.");
    }

    /// <summary>
    /// Unit test to verify that setting FullPath to null clears all dependent properties.
    /// </summary>
    [TestMethod]
    public void FullPath_SetToNull_ClearsAllDependentProperties()
    {
        // Arrange (Given)
        var instance = new IoFileInfo(@"C:\TestFolder\TestFile.txt");

        // Act (When)
        instance.FullPath = null;

        // Assert (Then)
        Assert.IsNull(
            instance.FullPath,
            "FullPath should be null after being set to null.");
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when FullPath is set to null.");
        Assert.IsNull(
            instance.Extension,
            "Extension should be null when FullPath is set to null.");
    }

    #endregion Integration Tests

    #endregion Public Methods
}