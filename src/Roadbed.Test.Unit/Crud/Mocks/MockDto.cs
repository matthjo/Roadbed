namespace Roadbed.Test.Unit.Crud.Mocks;

using System.Collections.Generic;
using Roadbed.Crud;

/// <summary>
/// Mock DTO for testing.
/// </summary>
public class MockDto
    : IDataTransferObject<int>
{
    #region Public Properties

    /// <inheritdoc />
    public List<string>? Errors
    {
        get;
        internal set;
    }

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public virtual int Id
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    public string? Name { get; set; }

    #endregion Public Properties
}