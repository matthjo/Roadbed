/*
 * The namespace Roadbed.Common.Dtos was removed on purpose and replaced with Roadbed.Common so that no additional using statements are required.
 */

namespace Roadbed.Common;

/// <summary>
/// Embedded Resource Response.
/// </summary>
public class CommonEmbeddedResourceResponse
{
    #region Internal Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonEmbeddedResourceResponse"/> class.
    /// </summary>
    /// <param name="isSuccess">Flag indicating whether the response was successful.</param>
    /// <param name="value">Object returned from the API.</param>
    /// <param name="errorMessage">Error message returned from the API.</param>
    internal CommonEmbeddedResourceResponse(bool isSuccess, string value, string errorMessage)
    {
        this.IsReadSuccessful = isSuccess;
        this.ErrorMessage = errorMessage;
        this.Data = value;
    }

    #endregion Internal Constructors

    #region Public Properties

    /// <summary>
    /// Gets the content returned from reading the embedded resource.
    /// </summary>
    public string Data
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets the Message related to the read attempt.
    /// </summary>
    public string? ErrorMessage
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets a value indicating whether the reading of the resource was successful.
    /// </summary>
    public bool IsReadSuccessful
    {
        get;
        internal set;
    }

    #endregion Public Properties

    #region Internal Methods

    /// <summary>
    /// Create a failed response.
    /// </summary>
    /// <param name="errorMessage">Error message returned from the API.</param>
    /// <returns><see cref="CommonEmbeddedResourceResponse"/> indicating a failed read attempt.</returns>
    internal static CommonEmbeddedResourceResponse Failure(string errorMessage) =>
        new CommonEmbeddedResourceResponse(false, default!, errorMessage);

    /// <summary>
    /// Create a successful response.
    /// </summary>
    /// <param name="value">Object returned from the API.</param>
    /// <returns><see cref="CommonEmbeddedResourceResponse"/> indicating a successful.</returns>
    internal static CommonEmbeddedResourceResponse Success(string value) =>
        new CommonEmbeddedResourceResponse(true, value, string.Empty);

    #endregion Internal Methods
}