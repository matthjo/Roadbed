namespace Roadbed.Test.Unit.Crud.Mocks;

using Roadbed.Crud;

/// <summary>
/// Mock Currency Row DTO for unit testing.
/// </summary>
internal record UnitCurrencyRow
    : BaseDataTransferObject<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitCurrencyRow"/> class.
    /// </summary>
    /// <param name="id">Numeric ID for the Currency.</param>
    /// <param name="name">Name of the Currency.</param>
    /// <param name="code">Code of the Currency.</param>
    public UnitCurrencyRow(long id, string name, string code)
    {
        this.Id = id;
        this.Name = name;
        this.Code = code;
    }

    /// <summary>
    /// Gets or sets the name of the Currency.
    /// </summary>
    public string Name
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets or sets the code of the Currency.
    /// </summary>
    public string Code
    {
        get;
        internal set;
    }
}
