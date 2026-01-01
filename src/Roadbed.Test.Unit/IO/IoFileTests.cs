namespace Roadbed.Test.Unit.IO;

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.IO;

/// <summary>
/// Contains unit tests for verifying the behavior of the IoFile class.
/// </summary>
[TestClass]
public class IoFileTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the parameterless constructor initializes with null FileInfo.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithNullFileInfo()
    {
        // Arrange (Given)

        // Act (When)
        var instance = new TestIoFile();

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when using parameterless constructor.");
    }

    /// <summary>
    /// Unit test to verify that the constructor with FileInfo parameter initializes correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithFileInfo_InitializesFileInfo()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.txt");

        // Act (When)
        var instance = new TestIoFile(fileInfo);

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.IsNotNull(
            instance.FileInfo,
            "FileInfo should not be null when initialized with FileInfo parameter.");
        Assert.AreSame(
            fileInfo,
            instance.FileInfo,
            "FileInfo should reference the same object that was passed to constructor.");
    }

    /// <summary>
    /// Unit test to verify that the constructor with null FileInfo accepts null value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNullFileInfo_AcceptsNullValue()
    {
        // Arrange (Given)
        IoFileInfo? nullFileInfo = null;

        // Act (When)
        var instance = new TestIoFile(nullFileInfo!);

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully even with null FileInfo.");
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when null is passed to constructor.");
    }

    /// <summary>
    /// Unit test to verify that FileInfo property can be set to null.
    /// </summary>
    [TestMethod]
    public void FileInfo_SetNull_AcceptsNullValue()
    {
        // Arrange (Given)
        var instance = new TestIoFile(new IoFileInfo(@"C:\TestFolder\TestFile.txt"));

        // Act (When)
        instance.FileInfo = null;

        // Assert (Then)
        Assert.IsNull(
            instance.FileInfo,
            "FileInfo should be null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that FileInfo property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void FileInfo_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var instance = new TestIoFile();
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.txt");

        // Act (When)
        instance.FileInfo = fileInfo;

        // Assert (Then)
        Assert.IsNotNull(
            instance.FileInfo,
            "FileInfo should not be null after being set.");
        Assert.AreSame(
            fileInfo,
            instance.FileInfo,
            "FileInfo should return the same object that was set.");
    }

    /// <summary>
    /// Unit test to verify that Save method creates directory if it does not exist.
    /// </summary>
    [TestMethod]
    public void Save_DirectoryDoesNotExist_CreatesDirectoryAndSavesFile()
    {
        // Arrange (Given)
        string testDirectory = Path.Combine(Path.GetTempPath(), $"testdir_{Guid.NewGuid()}");
        string testPath = Path.Combine(testDirectory, "testfile.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string fileContent = "Test content";

        try
        {
            // Ensure directory doesn't exist
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }

            // Create the directory for the test
            Directory.CreateDirectory(testDirectory);

            // Act (When)
            string result = instance.Save(fileContent);

            // Assert (Then)
            Assert.IsTrue(
                File.Exists(testPath),
                "File should be created even when directory had to be created.");
            Assert.AreEqual(
                testPath,
                result,
                "Save method should return the correct file path.");
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
        }
    }

    /// <summary>
    /// Unit test to verify that Save method overwrites existing file.
    /// </summary>
    [TestMethod]
    public void Save_FileAlreadyExists_OverwritesExistingFile()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string originalContent = "Original content";
        string newContent = "New content";

        try
        {
            // Create file with original content
            File.WriteAllText(testPath, originalContent);

            // Act (When)
            instance.Save(newContent);
            string savedContent = File.ReadAllText(testPath).TrimEnd();

            // Assert (Then)
            Assert.AreEqual(
                newContent,
                savedContent,
                "Save method should overwrite existing file with new content.");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }

    /// <summary>
    /// Unit test to verify that Save method returns empty string when file content is empty.
    /// </summary>
    [TestMethod]
    public void Save_FileContentIsEmpty_ReturnsEmptyString()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string fileContent = string.Empty;

        try
        {
            // Act (When)
            string result = instance.Save(fileContent);

            // Assert (Then)
            Assert.AreEqual(
                string.Empty,
                result,
                "Save method should return empty string when file content is empty.");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }

    /// <summary>
    /// Unit test to verify that Save method returns empty string when file content is null.
    /// </summary>
    [TestMethod]
    public void Save_FileContentIsNull_ReturnsEmptyString()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string? fileContent = null;

        try
        {
            // Act (When)
            string result = instance.Save(fileContent!);

            // Assert (Then)
            Assert.AreEqual(
                string.Empty,
                result,
                "Save method should return empty string when file content is null.");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }

    /// <summary>
    /// Unit test to verify that Save method throws ArgumentNullException when FileInfo Extension is empty.
    /// </summary>
    [TestMethod]
    public void Save_FileInfoExtensionIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var instance = new TestIoFile();
        instance.FileInfo = new IoFileInfo(@"C:\TestFolder\TestFile");
        string fileContent = "Test content";
        ArgumentNullException? caughtException = null;

        // Act (When)
        try
        {
            instance.Save(fileContent);
        }
        catch (ArgumentNullException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "Save method should throw ArgumentNullException when FileInfo Extension is empty.");
        Assert.AreEqual(
            "fileInfo",
            caughtException.ParamName,
            "Exception should indicate fileInfo parameter name.");
    }

    /// <summary>
    /// Unit test to verify that Save method throws ArgumentNullException when FileInfo Extension is null.
    /// </summary>
    [TestMethod]
    public void Save_FileInfoExtensionIsNull_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var instance = new TestIoFile();
        instance.FileInfo = new IoFileInfo();
        string fileContent = "Test content";
        ArgumentNullException? caughtException = null;

        // Act (When)
        try
        {
            instance.Save(fileContent);
        }
        catch (ArgumentNullException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "Save method should throw ArgumentNullException when FileInfo Extension is null.");
        Assert.AreEqual(
            "fileInfo",
            caughtException.ParamName,
            "Exception should indicate fileInfo parameter name.");
    }

    /// <summary>
    /// Unit test to verify that Save method throws ArgumentNullException when FileInfo is null.
    /// </summary>
    [TestMethod]
    public void Save_FileInfoIsNull_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var instance = new TestIoFile();
        string fileContent = "Test content";
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            instance.Save(fileContent);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsTrue(
            exceptionThrown,
            "Save method should throw ArgumentNullException when FileInfo is null.");
    }

    /// <summary>
    /// Unit test to verify that Save method writes correct content to file.
    /// </summary>
    [TestMethod]
    public void Save_ValidFileContent_WritesCorrectContentToFile()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string fileContent = "Test file content line 1";

        try
        {
            // Act (When)
            instance.Save(fileContent);
            string savedContent = File.ReadAllText(testPath).TrimEnd();

            // Assert (Then)
            Assert.AreEqual(
                fileContent,
                savedContent,
                "Saved file content should match the content that was passed to Save method.");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }

    /// <summary>
    /// Unit test to verify that Save method writes content to file and returns file path.
    /// </summary>
    [TestMethod]
    public void Save_ValidFileContentAndPath_WritesFileAndReturnsPath()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        var instance = new TestIoFile(new IoFileInfo(testPath));
        string fileContent = "Test file content";

        try
        {
            // Act (When)
            string result = instance.Save(fileContent);

            // Assert (Then)
            Assert.AreEqual(
                testPath,
                result,
                "Save method should return the file path that was saved.");
            Assert.IsTrue(
                File.Exists(testPath),
                "File should exist after Save method is called.");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }

    #endregion Public Methods

    #region Private Classes

    /// <summary>
    /// Test implementation of IoFile for testing purposes.
    /// </summary>
    private class TestIoFile : IoFile
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestIoFile"/> class.
        /// </summary>
        public TestIoFile()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestIoFile"/> class.
        /// </summary>
        /// <param name="fileInfo">System information about the file.</param>
        public TestIoFile(IoFileInfo fileInfo)
            : base(fileInfo)
        {
        }

        #endregion Public Constructors
    }

    #endregion Private Classes
}