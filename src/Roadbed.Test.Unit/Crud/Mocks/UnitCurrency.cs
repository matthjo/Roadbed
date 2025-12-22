namespace Roadbed.Test.Unit.Crud.Mocks;

using Microsoft.Extensions.Logging.Abstractions;
using Roadbed.Crud;

/// <summary>
/// Mock Langauge entity for unit testing.
/// </summary>
internal class UnitCurrency
    : BaseEntityWithCrudl<UnitCurrency, UnitCurrencyRow, long>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitCurrency"/> class.
    /// </summary>
    public UnitCurrency()
        : base(
            new UnitCurrencyRepository(),
            NullLoggerFactory.Instance)
    {
    }

    #endregion Public Constructors
}
