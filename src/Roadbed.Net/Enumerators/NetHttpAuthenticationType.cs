/*
 * The namespace Roadbed.Net.Enumerators was removed on purpose and replaced with Roadbed.Net so that no additional using statements are required.
 */

namespace Roadbed.Net;

/// <summary>
/// Types of HTTP Authentication.
/// </summary>
public enum NetHttpAuthenticationType
{
    /// <summary>
    /// Unknown Authentication Type.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Basic Authentication Type.
    /// </summary>
    Basic = 1,

    /// <summary>
    /// Bearer Authentication Type.
    /// </summary>
    Bearer = 2,
}