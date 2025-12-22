/*
 * The namespace Roadbed.Common was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

/// <summary>
/// Application Environment Types.
/// </summary>
public enum CommonEnvironmentType
{
    /// <summary>
    /// Unknown Application Environment.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Local Application Environment.
    /// </summary>
    Local = 1,

    /// <summary>
    /// Development Application Environment.
    /// </summary>
    Development = 3,

    /// <summary>
    /// QA Application Environment.
    /// </summary>
    Qa = 5,

    /// <summary>
    /// Staging Application Environment.
    /// </summary>
    Staging = 7,

    /// <summary>
    /// Production Application Environment.
    /// </summary>
    Production = 9,
}