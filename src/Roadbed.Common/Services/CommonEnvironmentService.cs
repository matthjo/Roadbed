namespace Roadbed.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Service for CommonEnvironment related operations.
    /// </summary>
    public static class CommonEnvironmentService
    {
        /// <summary>
        /// Converts a string to a CommonEnvironment enum value.
        /// </summary>
        /// <param name="str">String value to convert.</param>
        /// <returns><see cref="CommonEnvironment"/> based on <paramref name="str"/>.</returns>
        public static CommonEnvironment GetCommonEnvironment(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return CommonEnvironment.Unknown;
            }

            // Clean up the string
            string trimmedString = str.Trim().ToLower();

            // Convert string
            switch (trimmedString)
            {
                case "local":
                    return CommonEnvironment.Local;
                case "dev":
                case "development":
                    return CommonEnvironment.Development;
                case "qa":
                case "test":
                    return CommonEnvironment.Qa;
                case "staging":
                    return CommonEnvironment.Staging;
                case "pro":
                case "production":
                    return CommonEnvironment.Production;
            }

            // Unable to determine which environment
            return CommonEnvironment.Unknown;
        }
    }
}
