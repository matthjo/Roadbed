namespace Roadbed.Test.Unit.Crud.Mocks;

using Roadbed.Crud;

/// <summary>
/// Mock Country Row DTO for unit testing.
/// </summary>
internal record UnitCountryRow
    : BaseDataTransferObject<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitCountryRow"/> class.
    /// </summary>
    /// <param name="id">Numeric ID for the Country.</param>
    /// <param name="name">Name of the Country.</param>
    /// <param name="code">Alpha-2 Code for the Country.</param>
    public UnitCountryRow(int id, string name, string code)
    {
        this.Id = id;
        this.Name = name;
        this.Code = code;
    }

    /// <summary>
    /// Gets or sets the name of the Country.
    /// </summary>
    public string Name
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets or sets the code of the entity.
    /// </summary>
    public string Code
    {
        get;
        internal set;
    }
}
