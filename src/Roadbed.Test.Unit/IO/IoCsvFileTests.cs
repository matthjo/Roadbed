namespace Roadbed.Test.Unit.IO;

using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.IO;

/// <summary>
/// Contains unit tests for verifying the behavior of the IoCsvFile class.
/// </summary>
[TestClass]
public class IoCsvFileTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the constructor with DataMapper initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithDataMapper_InitializesProperties()
    {
        // Arrange (Given)
        var dataMapper = new TestCsvEntityMapper();

        // Act (When)
        var instance = new TestIoCsvFile(dataMapper);

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.IsNotNull(
            instance.DataRows,
            "DataRows should be initialized to empty list.");
        Assert.IsEmpty(
            instance.DataRows,
            "DataRows should be empty after construction.");
        Assert.AreSame(
            dataMapper,
            instance.DataMapper,
            "DataMapper should reference the same object that was passed to constructor.");
    }

    /// <summary>
    /// Unit test to verify that the constructor with FileInfo and DataMapper initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithFileInfoAndDataMapper_InitializesProperties()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.csv");
        var dataMapper = new TestCsvEntityMapper();

        // Act (When)
        var instance = new TestIoCsvFile(fileInfo, dataMapper);

        // Assert (Then)
        Assert.IsNotNull(
            instance,
            "Instance should be created successfully.");
        Assert.AreSame(
            fileInfo,
            instance.FileInfo,
            "FileInfo should reference the same object that was passed to constructor.");
        Assert.IsNotNull(
            instance.DataRows,
            "DataRows should be initialized to empty list.");
        Assert.IsEmpty(
            instance.DataRows,
            "DataRows should be empty after construction.");
        Assert.AreSame(
            dataMapper,
            instance.DataMapper,
            "DataMapper should reference the same object that was passed to constructor.");
    }

    /// <summary>
    /// Unit test to verify that the constructor accepts mixed case CSV extension.
    /// </summary>
    [TestMethod]
    public void Constructor_WithMixedCaseCsvExtension_AcceptsExtension()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.CsV");
        var dataMapper = new TestCsvEntityMapper();
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            var instance = new TestIoCsvFile(fileInfo, dataMapper);
        }
        catch (Exception)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsFalse(
            exceptionThrown,
            "Constructor should accept mixed case CSV extension.");
    }

    /// <summary>
    /// Unit test to verify that the constructor throws ArgumentException when extension is not CSV.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNonCsvExtension_ThrowsArgumentException()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.txt");
        var dataMapper = new TestCsvEntityMapper();
        ArgumentException? caughtException = null;

        // Act (When)
        try
        {
            var instance = new TestIoCsvFile(fileInfo, dataMapper);
        }
        catch (ArgumentException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "Constructor should throw ArgumentException when extension is not CSV.");
        Assert.AreEqual(
            "fileInfo",
            caughtException.ParamName,
            "Exception should indicate fileInfo parameter name.");
        Assert.Contains(
            "File extension isn't 'CSV'",
            caughtException.Message,
            "Exception message should indicate incorrect file extension.");
    }

    /// <summary>
    /// Unit test to verify that the constructor throws ArgumentNullException when Extension is null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNullExtension_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo();
        var dataMapper = new TestCsvEntityMapper();
        ArgumentNullException? caughtException = null;

        // Act (When)
        try
        {
            var instance = new TestIoCsvFile(fileInfo, dataMapper);
        }
        catch (ArgumentNullException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "Constructor should throw ArgumentNullException when Extension is null.");
        Assert.AreEqual(
            "fileInfo",
            caughtException.ParamName,
            "Exception should indicate fileInfo parameter name.");
    }

    /// <summary>
    /// Unit test to verify that the constructor throws ArgumentNullException when FileInfo is null.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNullFileInfo_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        IoFileInfo? nullFileInfo = null;
        var dataMapper = new TestCsvEntityMapper();
        ArgumentNullException? caughtException = null;

        // Act (When)
        try
        {
            var instance = new TestIoCsvFile(nullFileInfo!, dataMapper);
        }
        catch (ArgumentNullException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "Constructor should throw ArgumentNullException when FileInfo is null.");
        Assert.AreEqual(
            "fileInfo",
            caughtException.ParamName,
            "Exception should indicate fileInfo parameter name.");
    }

    /// <summary>
    /// Unit test to verify that the constructor accepts uppercase CSV extension.
    /// </summary>
    [TestMethod]
    public void Constructor_WithUppercaseCsvExtension_AcceptsExtension()
    {
        // Arrange (Given)
        var fileInfo = new IoFileInfo(@"C:\TestFolder\TestFile.CSV");
        var dataMapper = new TestCsvEntityMapper();
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            var instance = new TestIoCsvFile(fileInfo, dataMapper);
        }
        catch (Exception)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsFalse(
            exceptionThrown,
            "Constructor should accept uppercase CSV extension.");
    }

    /// <summary>
    /// Unit test to verify that DataMapper property can be set to null.
    /// </summary>
    [TestMethod]
    public void DataMapper_SetNull_AcceptsNullValue()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());

        // Act (When)
        instance.DataMapper = null;

        // Assert (Then)
        Assert.IsNull(
            instance.DataMapper,
            "DataMapper should be null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that DataMapper property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void DataMapper_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        var newMapper = new TestCsvEntityMapper();

        // Act (When)
        instance.DataMapper = newMapper;

        // Assert (Then)
        Assert.AreSame(
            newMapper,
            instance.DataMapper,
            "DataMapper should return the same object that was set.");
    }

    /// <summary>
    /// Unit test to verify that DataRows property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void DataRows_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        var newDataRows = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
            new TestDto { Id = 2, Name = "Test2" },
        };

        // Act (When)
        instance.DataRows = newDataRows;

        // Assert (Then)
        Assert.AreSame(
            newDataRows,
            instance.DataRows,
            "DataRows should return the same collection that was set.");
        Assert.HasCount(
            2,
            instance.DataRows,
            "DataRows should contain the expected number of items.");
    }

    /// <summary>
    /// Unit test to verify that ExportDataRowsAsContentString returns empty string when DataRows is empty.
    /// </summary>
    [TestMethod]
    public void ExportDataRowsAsContentString_EmptyDataRows_ReturnsHeaderOnly()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.DataRows = new List<TestDto>();

        // Act (When)
        string result = instance.ExportDataRowsAsContentString();

        // Assert (Then)
        Assert.IsFalse(
            string.IsNullOrEmpty(result),
            "ExportDataRowsAsContentString should return header even with empty DataRows.");
        Assert.Contains(
            "Id",
            result,
            "Export should contain header for Id.");
        Assert.Contains(
            "Name",
            result,
            "Export should contain header for Name.");
    }

    /// <summary>
    /// Unit test to verify that ExportDataRowsAsContentString returns empty string when configuration is null.
    /// </summary>
    [TestMethod]
    public void ExportDataRowsAsContentString_NullConfiguration_ReturnsEmptyString()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.DataRows = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
        };

        // Act (When)
        string result = instance.ExportDataRowsAsContentString(null!);

        // Assert (Then)
        Assert.AreEqual(
            string.Empty,
            result,
            "ExportDataRowsAsContentString should return empty string when configuration is null.");
    }

    /// <summary>
    /// Unit test to verify that ExportDataRowsAsContentString returns empty string when DataRows is null.
    /// </summary>
    [TestMethod]
    public void ExportDataRowsAsContentString_NullDataRows_ReturnsEmptyString()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.DataRows = null!;

        // Act (When)
        string result = instance.ExportDataRowsAsContentString();

        // Assert (Then)
        Assert.AreEqual(
            string.Empty,
            result,
            "ExportDataRowsAsContentString should return empty string when DataRows is null.");
    }

    /// <summary>
    /// Unit test to verify that ExportDataRowsAsContentString exports data correctly with default configuration.
    /// </summary>
    [TestMethod]
    public void ExportDataRowsAsContentString_WithDefaultConfiguration_ExportsDataCorrectly()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.DataRows = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
            new TestDto { Id = 2, Name = "Test2" },
        };

        // Act (When)
        string result = instance.ExportDataRowsAsContentString();

        // Assert (Then)
        Assert.IsFalse(
            string.IsNullOrEmpty(result),
            "ExportDataRowsAsContentString should return non-empty string.");
        Assert.Contains(
            "Id",
            result,
            "Export should contain header for Id.");
        Assert.Contains(
            "Name",
            result,
            "Export should contain header for Name.");
        Assert.Contains(
            "Test1",
            result,
            "Export should contain data from first row.");
        Assert.Contains(
            "Test2",
            result,
            "Export should contain data from second row.");
    }

    /// <summary>
    /// Unit test to verify that FromFile throws exception when file does not exist.
    /// </summary>
    [TestMethod]
    public void FromFile_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange (Given)
        string nonExistentPath = Path.Combine(Path.GetTempPath(), $"nonexistent_{Guid.NewGuid()}.csv");
        var dataMapper = new TestCsvEntityMapper();
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            var result = TestIoCsvFile.FromFile(nonExistentPath, dataMapper);
        }
        catch (FileNotFoundException)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsTrue(
            exceptionThrown,
            "FromFile should throw FileNotFoundException when file does not exist.");
    }

    /// <summary>
    /// Unit test to verify that FromFile reads valid CSV file correctly.
    /// </summary>
    [TestMethod]
    public void FromFile_ValidCsvFile_ReadsDataCorrectly()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        string csvContent = "Id,Name\n1,Test1\n2,Test2\n3,Test3";
        var dataMapper = new TestCsvEntityMapper();

        try
        {
            File.WriteAllText(testPath, csvContent);

            // Act (When)
            var result = TestIoCsvFile.FromFile(testPath, dataMapper);

            // Assert (Then)
            Assert.IsNotNull(
                result,
                "FromFile should return a valid instance.");
            Assert.IsNotNull(
                result.DataRows,
                "DataRows should be populated.");
            Assert.HasCount(
                3,
                result.DataRows,
                "DataRows should contain all rows from CSV file.");
            Assert.AreEqual(
                "Test1",
                result.DataRows[0].Name,
                "First row should be read correctly.");
            Assert.AreEqual(
                2,
                result.DataRows[1].Id,
                "Second row should be read correctly.");
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
    /// Unit test to verify that FromFile sets FileInfo property correctly.
    /// </summary>
    [TestMethod]
    public void FromFile_ValidCsvFile_SetsFileInfoCorrectly()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        string csvContent = "Id,Name\n1,Test1";
        var dataMapper = new TestCsvEntityMapper();

        try
        {
            File.WriteAllText(testPath, csvContent);

            // Act (When)
            var result = TestIoCsvFile.FromFile(testPath, dataMapper);

            // Assert (Then)
            Assert.IsNotNull(
                result.FileInfo,
                "FileInfo should be set.");
            Assert.AreEqual(
                testPath,
                result.FileInfo.FullPath,
                "FileInfo should have the correct path.");
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
    /// Unit test to verify that FromString handles empty content.
    /// </summary>
    [TestMethod]
    public void FromString_EmptyContent_ReturnsEmptyDataRows()
    {
        // Arrange (Given)
        string csvContent = "Id,Name";
        var dataMapper = new TestCsvEntityMapper();

        // Act (When)
        var result = TestIoCsvFile.FromString(csvContent, dataMapper);

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "FromString should return a valid instance.");
        Assert.IsEmpty(
            result.DataRows,
            "DataRows should be empty when CSV has only headers.");
    }

    /// <summary>
    /// Unit test to verify that FromString does not set FileInfo property.
    /// </summary>
    [TestMethod]
    public void FromString_ValidCsvContent_DoesNotSetFileInfo()
    {
        // Arrange (Given)
        string csvContent = "Id,Name\n1,Test1";
        var dataMapper = new TestCsvEntityMapper();

        // Act (When)
        var result = TestIoCsvFile.FromString(csvContent, dataMapper);

        // Assert (Then)
        Assert.IsNull(
            result.FileInfo,
            "FileInfo should be null when created from string.");
    }

    /// <summary>
    /// Unit test to verify that FromString reads valid CSV content correctly.
    /// </summary>
    [TestMethod]
    public void FromString_ValidCsvContent_ReadsDataCorrectly()
    {
        // Arrange (Given)
        string csvContent = "Id,Name\n1,Test1\n2,Test2\n3,Test3";
        var dataMapper = new TestCsvEntityMapper();

        // Act (When)
        var result = TestIoCsvFile.FromString(csvContent, dataMapper);

        // Assert (Then)
        Assert.IsNotNull(
            result,
            "FromString should return a valid instance.");
        Assert.IsNotNull(
            result.DataRows,
            "DataRows should be populated.");
        Assert.HasCount(
            3,
            result.DataRows,
            "DataRows should contain all rows from CSV content.");
        Assert.AreEqual(
            "Test1",
            result.DataRows[0].Name,
            "First row should be read correctly.");
        Assert.AreEqual(
            2,
            result.DataRows[1].Id,
            "Second row should be read correctly.");
    }

    /// <summary>
    /// Unit test to verify that data can be round-tripped through string operations.
    /// </summary>
    [TestMethod]
    public void Integration_ExportAndLoadString_PreservesData()
    {
        // Arrange (Given)
        var originalData = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
            new TestDto { Id = 2, Name = "Test2" },
        };

        var exportInstance = new TestIoCsvFile(new TestCsvEntityMapper());
        exportInstance.DataRows = originalData;

        // Export data
        string csvContent = exportInstance.ExportDataRowsAsContentString();

        // Act (When) - Load from string
        var loadInstance = TestIoCsvFile.FromString(csvContent, new TestCsvEntityMapper());

        // Assert (Then)
        Assert.HasCount(
            originalData.Count,
            loadInstance.DataRows,
            "Loaded data should have same count as exported data.");

        for (int i = 0; i < originalData.Count; i++)
        {
            Assert.AreEqual(
                originalData[i].Id,
                loadInstance.DataRows[i].Id,
                $"Id of row {i} should match.");
            Assert.AreEqual(
                originalData[i].Name,
                loadInstance.DataRows[i].Name,
                $"Name of row {i} should match.");
        }
    }

    /// <summary>
    /// Unit test to verify that data can be round-tripped through file operations.
    /// </summary>
    [TestMethod]
    public void Integration_SaveAndLoad_PreservesData()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        var originalData = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
            new TestDto { Id = 2, Name = "Test2" },
            new TestDto { Id = 3, Name = "Test3" },
        };

        try
        {
            // Save data
            var saveInstance = new TestIoCsvFile(new TestCsvEntityMapper());
            saveInstance.FileInfo = new IoFileInfo(testPath);
            saveInstance.DataRows = originalData;
            saveInstance.Save();

            // Act (When) - Load data
            var loadInstance = TestIoCsvFile.FromFile(testPath, new TestCsvEntityMapper());

            // Assert (Then)
            Assert.HasCount(
                originalData.Count,
                loadInstance.DataRows,
                "Loaded data should have same count as saved data.");

            for (int i = 0; i < originalData.Count; i++)
            {
                Assert.AreEqual(
                    originalData[i].Id,
                    loadInstance.DataRows[i].Id,
                    $"Id of row {i} should match.");
                Assert.AreEqual(
                    originalData[i].Name,
                    loadInstance.DataRows[i].Name,
                    $"Name of row {i} should match.");
            }
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
    /// Unit test to verify that LoadDataRowsFromFile resets existing DataRows.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromFile_ExistingDataRows_ResetsDataRows()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        string csvContent = "Id,Name\n1,NewTest";

        try
        {
            File.WriteAllText(testPath, csvContent);

            var instance = new TestIoCsvFile(new TestCsvEntityMapper());
            instance.FileInfo = new IoFileInfo(testPath);
            instance.DataRows = new List<TestDto>
            {
                new TestDto { Id = 99, Name = "OldTest" },
            };

            // Act (When)
            instance.LoadDataRowsFromFile();

            // Assert (Then)
            Assert.HasCount(
                1,
                instance.DataRows,
                "DataRows should contain only new data from file.");
            Assert.AreEqual(
                "NewTest",
                instance.DataRows[0].Name,
                "DataRows should contain new data, not old data.");
            Assert.AreEqual(
                1,
                instance.DataRows[0].Id,
                "DataRows should contain new data, not old data.");
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
    /// Unit test to verify that LoadDataRowsFromFile throws ArgumentNullException when DataMapper is null.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromFile_NullDataMapper_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        string csvContent = "Id,Name\n1,Test1";

        try
        {
            File.WriteAllText(testPath, csvContent);

            var instance = new TestIoCsvFile(new TestCsvEntityMapper());
            instance.FileInfo = new IoFileInfo(testPath);
            instance.DataMapper = null;
            ArgumentNullException? caughtException = null;

            // Act (When)
            try
            {
                instance.LoadDataRowsFromFile();
            }
            catch (ArgumentNullException ex)
            {
                caughtException = ex;
            }

            // Assert (Then)
            Assert.IsNotNull(
                caughtException,
                "LoadDataRowsFromFile should throw ArgumentNullException when DataMapper is null.");
            Assert.AreEqual(
                "dataMapper",
                caughtException.ParamName,
                "Exception should indicate dataMapper parameter name.");
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
    /// Unit test to verify that LoadDataRowsFromFile throws ArgumentNullException when FileInfo is null.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromFile_NullFileInfo_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.FileInfo = null;
        bool exceptionThrown = false;

        // Act (When)
        try
        {
            instance.LoadDataRowsFromFile();
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        // Assert (Then)
        Assert.IsTrue(
            exceptionThrown,
            "LoadDataRowsFromFile should throw ArgumentNullException when FileInfo is null.");
    }

    /// <summary>
    /// Unit test to verify that LoadDataRowsFromFile reads file correctly.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromFile_ValidFile_PopulatesDataRows()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        string csvContent = "Id,Name\n1,Test1\n2,Test2";

        try
        {
            File.WriteAllText(testPath, csvContent);

            var instance = new TestIoCsvFile(new TestCsvEntityMapper());
            instance.FileInfo = new IoFileInfo(testPath);

            // Act (When)
            instance.LoadDataRowsFromFile();

            // Assert (Then)
            Assert.IsNotNull(
                instance.DataRows,
                "DataRows should be populated.");
            Assert.HasCount(
                2,
                instance.DataRows,
                "DataRows should contain all rows from file.");
            Assert.AreEqual(
                "Test1",
                instance.DataRows[0].Name,
                "First row should be read correctly.");
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
    /// Unit test to verify that LoadDataRowsFromString resets existing DataRows.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromString_ExistingDataRows_ResetsDataRows()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        string csvContent = "Id,Name\n1,NewTest";
        instance.DataRows = new List<TestDto>
        {
            new TestDto { Id = 99, Name = "OldTest" },
        };

        // Act (When)
        instance.LoadDataRowsFromString(csvContent);

        // Assert (Then)
        Assert.HasCount(
            1,
            instance.DataRows,
            "DataRows should contain only new data from content.");
        Assert.AreEqual(
            "NewTest",
            instance.DataRows[0].Name,
            "DataRows should contain new data, not old data.");
    }

    /// <summary>
    /// Unit test to verify that LoadDataRowsFromString throws ArgumentNullException when DataMapper is null.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromString_NullDataMapper_ThrowsArgumentNullException()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.DataMapper = null;
        string csvContent = "Id,Name\n1,Test1";
        ArgumentNullException? caughtException = null;

        // Act (When)
        try
        {
            instance.LoadDataRowsFromString(csvContent);
        }
        catch (ArgumentNullException ex)
        {
            caughtException = ex;
        }

        // Assert (Then)
        Assert.IsNotNull(
            caughtException,
            "LoadDataRowsFromString should throw ArgumentNullException when DataMapper is null.");
        Assert.AreEqual(
            "dataMapper",
            caughtException.ParamName,
            "Exception should indicate dataMapper parameter name.");
    }

    /// <summary>
    /// Unit test to verify that LoadDataRowsFromString reads content correctly.
    /// </summary>
    [TestMethod]
    public void LoadDataRowsFromString_ValidContent_PopulatesDataRows()
    {
        // Arrange (Given)
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        string csvContent = "Id,Name\n1,Test1\n2,Test2";

        // Act (When)
        instance.LoadDataRowsFromString(csvContent);

        // Assert (Then)
        Assert.IsNotNull(
            instance.DataRows,
            "DataRows should be populated.");
        Assert.HasCount(
            2,
            instance.DataRows,
            "DataRows should contain all rows from content.");
        Assert.AreEqual(
            "Test1",
            instance.DataRows[0].Name,
            "First row should be read correctly.");
    }

    /// <summary>
    /// Unit test to verify that Save method with default configuration writes file correctly.
    /// </summary>
    [TestMethod]
    public void Save_DefaultConfiguration_WritesFileCorrectly()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.FileInfo = new IoFileInfo(testPath);
        instance.DataRows = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
            new TestDto { Id = 2, Name = "Test2" },
        };

        try
        {
            // Act (When)
            string result = instance.Save();

            // Assert (Then)
            Assert.AreEqual(
                testPath,
                result,
                "Save should return the file path.");
            Assert.IsTrue(
                File.Exists(testPath),
                "File should exist after Save is called.");

            string fileContent = File.ReadAllText(testPath);
            Assert.Contains(
                "Test1",
                fileContent,
                "File should contain data from first row.");
            Assert.Contains(
                "Test2",
                fileContent,
                "File should contain data from second row.");
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
    /// Unit test to verify that Save method returns file path.
    /// </summary>
    [TestMethod]
    public void Save_ValidData_ReturnsFilePath()
    {
        // Arrange (Given)
        string testPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.csv");
        var instance = new TestIoCsvFile(new TestCsvEntityMapper());
        instance.FileInfo = new IoFileInfo(testPath);
        instance.DataRows = new List<TestDto>
        {
            new TestDto { Id = 1, Name = "Test1" },
        };

        try
        {
            // Act (When)
            string result = instance.Save();

            // Assert (Then)
            Assert.AreEqual(
                testPath,
                result,
                "Save should return the file path that was saved.");
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
    /// Test implementation of ICsvEntityMapper for testing purposes.
    /// </summary>
    private class TestCsvEntityMapper : ICsvEntityMapper<TestDto>
    {
        #region Public Methods

        public TestDto MapEntity(CsvReader csvReader)
        {
            return new TestDto
            {
                Id = csvReader.GetField<int>("Id"),
                Name = csvReader.GetField<string>("Name") ?? string.Empty,
            };
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Test DTO for CSV file testing.
    /// </summary>
    private class TestDto
    {
        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        #endregion Public Properties
    }

    /// <summary>
    /// Test implementation of IoCsvFile for testing purposes.
    /// </summary>
    private class TestIoCsvFile : IoCsvFile<TestDto>
    {
        #region Public Constructors

        public TestIoCsvFile(ICsvEntityMapper<TestDto> dataMapper)
            : base(dataMapper)
        {
        }

        public TestIoCsvFile(IoFileInfo fileInfo, ICsvEntityMapper<TestDto> dataMapper)
            : base(fileInfo, dataMapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public static new TestIoCsvFile FromFile(string path, ICsvEntityMapper<TestDto> dataMapper)
        {
            TestIoCsvFile file = new TestIoCsvFile(
                new IoFileInfo(path),
                dataMapper);

            file.LoadDataRowsFromFile();

            return file;
        }

        public static new TestIoCsvFile FromString(string content, ICsvEntityMapper<TestDto> dataMapper)
        {
            TestIoCsvFile file = new TestIoCsvFile(dataMapper);

            file.LoadDataRowsFromString(content);

            return file;
        }

        #endregion Public Methods
    }

    #endregion Private Classes
}