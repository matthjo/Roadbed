/*
 * The namespace Roadbed.Net.Dtos was removed on purpose and replaced with Roadbed.Net so that no additional using statements are required.
 */

namespace Roadbed.Net;

/// <summary>
/// Http Header used in the HttpClient.
/// </summary>
public class NetHttpHeader
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the Http Header Name.
    /// </summary>
    public string? Name
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Http Header Value.
    /// </summary>
    public string? Value
    {
        get;
        set;
    }

    #endregion Public Properties
}