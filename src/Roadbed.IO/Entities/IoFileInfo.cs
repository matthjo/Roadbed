/*
 * The namespace Roadbed.IO.Dtos was removed on purpose and replaced with Roadbed.IO so that no additional using statements are required.
 */

namespace Roadbed.IO;

using System.IO;

/// <summary>
/// System File Data Transfer Object (DTO).
/// </summary>
/// <remarks>
/// This is a simplified version of <see cref="FileInfo"/> for data transfer purposes.
/// </remarks>
public class IoFileInfo
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IoFileInfo"/> class.
    /// </summary>
    public IoFileInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IoFileInfo"/> class.
    /// </summary>
    /// <param name="path">Full Path of the file.</param>
    public IoFileInfo(string path)
    {
        this.FullPath = path;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets the Extension of the file.
    /// </summary>
    public string? Extension
    {
        get
        {
            if (string.IsNullOrEmpty(this.FullPath) ||
                (this.FileInfo == null))
            {
                return null;
            }

            return this.FileInfo.Extension;
        }
    }

    /// <summary>
    /// Gets the full version of <see cref="FileInfo"/>.
    /// </summary>
    public FileInfo? FileInfo
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets or sets the Full Path of the file.
    /// </summary>
    public string? FullPath
    {
        get
        {
            return this.FileInfo?.FullName;
        }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                this.FileInfo = null;
            }
            else
            {
                this.FileInfo = new FileInfo(value);
            }
        }
    }

    #endregion Public Properties
}