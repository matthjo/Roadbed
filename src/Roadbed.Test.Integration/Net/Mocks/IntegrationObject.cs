namespace Roadbed.Test.Integration.Net.Mocks;

using Microsoft.Extensions.Logging.Abstractions;
using Roadbed.Crud;

/// <summary>
/// Mock Integration Object entity for integration testing.
/// </summary>
internal class IntegrationObject
    : BaseEntityWithCrudl<IntegrationObject, IntegrationObjectRow, string>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationObject"/> class.
    /// </summary>
    public IntegrationObject()
        : base(
            new IntegrationObjectRepository(),
            NullLoggerFactory.Instance)
    {
    }

    #endregion Public Constructors
}
