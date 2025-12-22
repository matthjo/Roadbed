namespace Roadbed.Test.Unit.Crud.Mocks;

using Microsoft.Extensions.Logging.Abstractions;
using Roadbed.Crud;

/// <summary>
/// Mock Country entity for unit testing.
/// </summary>
internal class UnitCountry
    : BaseEntityWithCrud<UnitCountry, UnitCountryRow, int>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitCountry"/> class.
    /// </summary>
    public UnitCountry()
        : base(
            new UnitCountryRepository(),
            NullLoggerFactory.Instance)
    {
    }

    #endregion Public Constructors
}