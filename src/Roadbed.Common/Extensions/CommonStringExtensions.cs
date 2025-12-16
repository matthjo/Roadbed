/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */
namespace Roadbed
{
    using Roadbed.Common.Services;

    /// <summary>
    /// Extensions for common String operations.
    /// </summary>
    public static class CommonStringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Retrieves the common environment associated with the specified string.
        /// </summary>
        /// <param name="str">The string for which to obtain the common environment.</param>
        /// <returns>A CommonEnvironment instance that represents the environment associated with the specified string.</returns>
        public static CommonEnvironment GetCommonEnvironment(this string str)
        {
            return CommonEnvironmentService.GetCommonEnvironment(str);
        }

        #endregion Public Methods
    }
}