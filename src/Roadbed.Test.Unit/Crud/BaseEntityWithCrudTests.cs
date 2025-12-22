namespace Roadbed.Test.Unit.Crud;

using Roadbed.Test.Unit.Crud.Mocks;

/// <summary>
/// Contains unit tests for verifying the behavior of the BaseEntityWithCrud class.
/// </summary>
[TestClass]
public class BaseEntityWithCrudTests
{
    #region Public Properties

    /// <summary>
    /// Gets or sets object used to store information that is provided to unit tests.
    /// </summary>
    public TestContext TestContext { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrud updates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrud_UpdateOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCountry entity = new UnitCountry();
        int expectedId = 752;
        string expectedName = "New Sweden";
        string expectedCode = "SE";

        // Act (When)
        await entity.UpdateAsync(
            new UnitCountryRow(expectedId, expectedName, expectedCode),
            this.TestContext.CancellationToken);
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Country Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Country Name doesn't match.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrud creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrud_CreateOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCountry entity = new UnitCountry();
        int expectedId = 1000;
        string expectedName = "New World";
        string expectedCode = "NW";

        // Act (When)
        var newid = await entity.CreateAsync(
            new UnitCountryRow(expectedId, expectedName, expectedCode),
            this.TestContext.CancellationToken);
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.AreEqual(
            expectedId,
            newid,
            "Country was inserted.");
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Country Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Country Name doesn't match.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrud reads a Valid ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrud_ReadOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCountry entity = new UnitCountry();
        int expectedId = 724; // Spain
        string expectedName = "Spain";
        string expectedCode = "ES";

        // Act (When)
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Country Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Country Name doesn't match.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrud reads a Bad ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrud_ReadOperation_MissingRow()
    {
        // Arrange (Given)
        UnitCountry entity = new UnitCountry();
        int expectedId = 2000; // Unknown

        // Act (When)
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNull(
            row,
            "A row was returned.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrud delete a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrud_DeleteOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCountry entity = new UnitCountry();
        int expectedId = 756; // Switzerland

        // Act (When)
        await entity.DeleteAsync(
            expectedId,
            this.TestContext.CancellationToken);

        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNull(
            row,
            "A row was returned.");
    }

    #endregion Public Methods
}
