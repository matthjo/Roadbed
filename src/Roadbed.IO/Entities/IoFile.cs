/*
 * The namespace Roadbed.IO.Entities was removed on purpose and replaced with Roadbed.IO so that no additional using statements are required.
 */

namespace Roadbed.IO;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Base Entity for IO File related operations.
/// </summary>
public abstract class IoFile
{
    #region Protected Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IoFile"/> class.
    /// </summary>
    protected IoFile()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IoFile"/> class.
    /// </summary>
    /// <param name="fileInfo">System information about the file.</param>
    protected IoFile(IoFileInfo fileInfo)
    {
        this.FileInfo = fileInfo;
    }

    #endregion Protected Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the File Info.
    /// </summary>
    public IoFileInfo? FileInfo
    {
        get;
        set;
    }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Saves the file content to the file path specified in <see cref="IoFile.IoFile(IoFileInfo)"/>.
    /// </summary>
    /// <param name="fileContent">Content of the file to be saved.</param>
    /// <returns>Path to the file that was saved.</returns>
    public string Save(string fileContent)
    {
        // Validate "In" Properties
        ValidateFileInfo(this.FileInfo!);

        if (string.IsNullOrEmpty(fileContent))
        {
            return string.Empty;
        }

        using (StreamWriter streamWriter = new StreamWriter(this.FileInfo!.FullPath!))
        {
            streamWriter.WriteLine(fileContent);
        }

        return this.FileInfo!.FullPath!;
    }

    #endregion Public Methods

    #region Internal Methods

    /// <summary>
    /// Validates the file info for CSV files.
    /// </summary>
    /// <param name="fileInfo">File information to validate.</param>
    /// <exception cref="ArgumentNullException">File info or file extension is null or empty.</exception>
    /// <exception cref="ArgumentException">File extension isn't 'CSV'.</exception>
    internal static void ValidateFileInfo(IoFileInfo fileInfo)
    {
        if ((fileInfo == null) ||
            string.IsNullOrEmpty(fileInfo.Extension))
        {
            throw new ArgumentNullException(nameof(fileInfo), "File info or file extension is null or empty.");
        }
    }

    #endregion Internal Methods
}