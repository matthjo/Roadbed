namespace Roadbed.Test.Unit.Crud.Mocks;

using Roadbed.Crud;

/// <summary>
/// Mock Language Row DTO for unit testing.
/// </summary>
internal record UnitLanguageRow
    : BaseDataTransferObject<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitLanguageRow"/> class.
    /// </summary>
    /// <param name="id">String ID for the Language.</param>
    /// <param name="name">Name of the Language.</param>
    public UnitLanguageRow(string id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the name of the Language.
    /// </summary>
    public string Name
    {
        get;
        internal set;
    }
}
