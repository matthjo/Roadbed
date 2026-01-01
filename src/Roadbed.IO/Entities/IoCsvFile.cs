/*
 * The namespace Roadbed.IO.Entities was removed on purpose and replaced with Roadbed.IO so that no additional using statements are required.
 */

namespace Roadbed.IO;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// Implementation of <see cref="IoFile"/> for CSV files.
/// </summary>
/// <typeparam name="T">Type of Data Transfer Object (DTO) that represents each row in the CSV file.</typeparam>
public class IoCsvFile<T>
    : IoFile
{
    #region Protected Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IoCsvFile{T}"/> class.
    /// </summary>
    /// <param name="fileInfo">System information about the file.</param>
    /// <param name="dataMapper">Data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.</param>
    protected IoCsvFile(ICsvEntityMapper<T> dataMapper)
    {
        // Initialize Properties
        this.DataRows = new List<T>();
        this.DataMapper = dataMapper;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IoCsvFile{T}"/> class.
    /// </summary>
    /// <param name="fileInfo">System information about the file.</param>
    /// <param name="dataMapper">Data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.</param>
    protected IoCsvFile(IoFileInfo fileInfo, ICsvEntityMapper<T> dataMapper)
        : base(fileInfo)
    {
        // Validate "In" Values
        ValidateFileInfo(fileInfo);

        if (fileInfo!.Extension!.ToLower() != ".csv")
        {
            throw new ArgumentException("File extension isn't 'CSV'.", nameof(fileInfo));
        }

        // Initialize Properties
        this.DataRows = new List<T>();
        this.DataMapper = dataMapper;
    }

    #endregion Protected Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.
    /// </summary>
    public ICsvEntityMapper<T>? DataMapper
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the rows of data in the CSV.
    /// </summary>
    public IList<T> DataRows
    {
        get;
        set;
    }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Creates an instance of <see cref="IoCsvFile{T}"/> from a file.
    /// </summary>
    /// <param name="path">System information about the file.</param>
    /// <param name="dataMapper">Data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.</param>
    /// <returns>Instance of <see cref="IoCsvFile{T}"/>.</returns>
    public static IoCsvFile<T> FromFile(string path, ICsvEntityMapper<T> dataMapper)
    {
        // Create Instance
        IoCsvFile<T> file = new IoCsvFile<T>(
            new IoFileInfo(path),
            dataMapper);

        // Read the File
        file.LoadDataRowsFromFile();

        // Return result
        return file;
    }

    /// <summary>
    /// Creates an instance of <see cref="IoCsvFile{T}"/> from a file.
    /// </summary>
    /// <param name="content">System information about the file.</param>
    /// <param name="dataMapper">Data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.</param>
    /// <returns>Instance of <see cref="IoCsvFile{T}"/>.</returns>
    public static IoCsvFile<T> FromString(string content, ICsvEntityMapper<T> dataMapper)
    {
        // Create Instance
        IoCsvFile<T> file = new IoCsvFile<T>(
            dataMapper);

        // Read the File
        file.LoadDataRowsFromString(content);

        // Return result
        return file;
    }

    /// <summary>
    /// Exports the <see cref="DataRows"/> property as a content string.
    /// </summary>
    /// <param name="content">In-memory content to use to fill the <see cref="DataRows"/> property.</param>
    /// <returns>Content string formatted as a CSV.</returns>
    public string ExportDataRowsAsContentString()
    {
        return this.ExportDataRowsAsContentString(GetDefaultConfiguration());
    }

    /// <summary>
    /// Exports the <see cref="DataRows"/> property as a content string.
    /// </summary>
    /// <param name="configuration">CsvHelper configuration used in the export process.</param>
    /// <returns>Content string formatted as a CSV.</returns>
    public string ExportDataRowsAsContentString(CsvConfiguration configuration)
    {
        if ((this.DataRows == null) || (configuration == null))
        {
            return string.Empty;
        }

        string result = string.Empty;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, configuration))
                {
                    csvWriter.WriteRecords(this.DataRows);

                    streamWriter.Flush();
                    result = Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Fills the <see cref="DataRows"/> property by reading the CSV content from the <see cref="IoFileInfo"/>.
    /// </summary>
    /// <param name="content">In-memory content to use to fill the <see cref="DataRows"/> property.</param>
    public void LoadDataRowsFromFile()
    {
        // Validate "In" Properties
        ValidateFileInfo(this.FileInfo!);
        ValidateDataMapper(this.DataMapper);

        // Reset "Out" Properties
        this.DataRows = new List<T>();

        // Fill Data Rows from File
        using (FileStream stream = new FileStream(this.FileInfo!.FullPath!, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (TextReader textReader = new StreamReader(stream))
            {
                using (CsvReader csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        this.DataRows.Add(this.DataMapper!.MapEntity(csvReader));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Fills the <see cref="DataRows"/> property by reading the CSV content from a string.
    /// </summary>
    /// <param name="content">In-memory content to use to fill the <see cref="DataRows"/> property.</param>
    public void LoadDataRowsFromString(string content)
    {
        // Validate "In" Properties
        ValidateDataMapper(this.DataMapper);

        // Reset "Out" Properties
        this.DataRows = new List<T>();

        // Fill Data Rows from Content
        using (TextReader textReader = new StringReader(content))
        {
            using (CsvReader csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture))
            {
                csvReader.Read();
                csvReader.ReadHeader();

                while (csvReader.Read())
                {
                    this.DataRows.Add(this.DataMapper!.MapEntity(csvReader));
                }
            }
        }
    }

    /// <summary>
    /// Saves the file content to the file path specified in <see cref="IoCsvFile{T}.FullPath"/>.
    /// </summary>
    /// <returns>Path to the file that was saved.</returns>
    public string Save()
    {
        return this.Save(GetDefaultConfiguration());
    }

    /// <summary>
    /// Saves the file content to the file path specified in <see cref="IoCsvFile{T}.FullPath"/>.
    /// </summary>
    /// <param name="configuration">CsvHelper configuration used in the export process.</param>
    /// <returns>Path to the file that was saved.</returns>
    public string Save(CsvConfiguration configuration)
    {
        return this.Save(
            this.ExportDataRowsAsContentString(configuration));
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Default CsvHelper configuration used in the export process.
    /// </summary>
    private static CsvConfiguration GetDefaultConfiguration()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            Encoding = Encoding.UTF8,
        };
    }

    /// <summary>
    /// Validates the Data Mapper.
    /// </summary>
    /// <param name="dataMapper">Data mapper used to turn lines in the CSV into a <see cref="IList{T}"/>.</param>
    /// <exception cref="ArgumentNullException">Data Mapper is null.</exception>
    private static void ValidateDataMapper(ICsvEntityMapper<T>? dataMapper)
    {
        if (dataMapper == null)
        {
            throw new ArgumentNullException(nameof(dataMapper), "Data Mapper is unknown.");
        }
    }

    #endregion Private Methods
}