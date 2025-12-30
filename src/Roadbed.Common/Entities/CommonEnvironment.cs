/*
 * The namespace Roadbed.Common.Entities was removed on purpose and replaced with Roadbed.Common so that no additional using statements are required.
 */

namespace Roadbed.Common;

/// <summary>
/// Entity for CommonEnvironment related operations.
/// </summary>
public static class CommonEnvironment
{
    #region Public Methods

    /// <summary>
    /// Converts a string to a CommonEnvironment enum value.
    /// </summary>
    /// <param name="str">String value to convert.</param>
    /// <returns><see cref="CommonEnvironmentType"/> based on <paramref name="str"/>.</returns>
    public static CommonEnvironmentType GetCommonEnvironment(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return CommonEnvironmentType.Unknown;
        }

        // Clean up the string
        string trimmedString = str.Trim().ToLower();

        // Convert string
        switch (trimmedString)
        {
            case "local":
                return CommonEnvironmentType.Local;

            case "dev":
            case "development":
                return CommonEnvironmentType.Development;

            case "qa":
            case "test":
                return CommonEnvironmentType.Qa;

            case "staging":
                return CommonEnvironmentType.Staging;

            case "pro":
            case "production":
                return CommonEnvironmentType.Production;
        }

        // Unable to determine which environment
        return CommonEnvironmentType.Unknown;
    }

    #endregion Public Methods
}