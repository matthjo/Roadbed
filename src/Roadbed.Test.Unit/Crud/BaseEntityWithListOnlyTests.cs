namespace Roadbed.Test.Unit.Crud;

using Roadbed.Test.Unit.Crud.Mocks;

/// <summary>
/// Contains unit tests for verifying the behavior of the BaseEntityWithListOnly class.
/// </summary>
[TestClass]
public class BaseEntityWithListOnly
{
    #region Public Properties

    /// <summary>
    /// Gets or sets object used to store information that is provided to unit tests.
    /// </summary>
    public TestContext TestContext { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Unit test to verify that BaseEntityWithListOnly lists existing rows correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithListOnly_ListOperation_TotalCount()
    {
        // Arrange (Given)
        UnitLangauge entity = new UnitLangauge();
        int expectedCount = 5;

        // Act (When)
        var row = await entity.ListAsync(this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.HasCount(
            expectedCount,
            row,
            "Language count doesn't match.");
    }

    #endregion Public Methods
}
