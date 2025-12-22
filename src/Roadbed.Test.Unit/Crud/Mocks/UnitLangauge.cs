namespace Roadbed.Test.Unit.Crud.Mocks;

using Microsoft.Extensions.Logging.Abstractions;
using Roadbed.Crud;

/// <summary>
/// Mock Langauge entity for unit testing.
/// </summary>
internal class UnitLangauge
    : BaseEntityWithListOnly<UnitLangauge, UnitLanguageRow, string>
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitLangauge"/> class.
    /// </summary>
    public UnitLangauge()
        : base(
            new UnitLanguageRepository(),
            NullLoggerFactory.Instance)
    {
    }

    #endregion Public Constructors
}
