namespace Roadbed.Test.Unit.Crud;

using Roadbed.Test.Unit.Crud.Mocks;

/// <summary>
/// Contains unit tests for verifying the behavior of the BaseEntityWithCrudl class.
/// </summary>
[TestClass]
public class BaseEntityWithCrudlTests
{
    #region Public Properties

    /// <summary>
    /// Gets or sets object used to store information that is provided to unit tests.
    /// </summary>
    public TestContext TestContext { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl updates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrudl_UpdateOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCurrency entity = new UnitCurrency();
        int expectedId = 826;
        string expectedName = "England Pound";
        string expectedCode = "EP";
        int expectedCount = 4;

        // Act (When)
        await entity.UpdateAsync(
            new UnitCurrencyRow(expectedId, expectedName, expectedCode),
            this.TestContext.CancellationToken);

        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);
        var list = await entity.ListAsync(this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Currency Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Currency Name doesn't match.");
        Assert.HasCount(
            expectedCount,
            list,
            $"There should only be {expectedCount} rows left in the collection.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrudl_CreateOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCurrency entity = new UnitCurrency();
        int expectedId = 100;
        string expectedName = "Bit Coin";
        string expectedCode = "BC";
        int expectedCount = 5;

        // Act (When)
        var newid = await entity.CreateAsync(
            new UnitCurrencyRow(expectedId, expectedName, expectedCode),
            this.TestContext.CancellationToken);

        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);
        var list = await entity.ListAsync(this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.AreEqual(
            expectedId,
            newid,
            "Currency was inserted.");
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Currency Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Currency Name doesn't match.");
        Assert.HasCount(
            expectedCount,
            list,
            $"There should only be {expectedCount} rows left in the collection.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl reads a Valid ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrudl_ReadOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCurrency entity = new UnitCurrency();
        int expectedId = 840; // Dollar
        string expectedName = "United States Dollar";
        string expectedCode = "USD";

        // Act (When)
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            expectedName,
            row.Name,
            "Currency Name doesn't match.");
        Assert.AreEqual(
            expectedCode,
            row.Code,
            "Currency Name doesn't match.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl reads a Bad ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrudl_ReadOperation_MissingRow()
    {
        // Arrange (Given)
        UnitCurrency entity = new UnitCurrency();
        int expectedId = 2000; // Unknown

        // Act (When)
        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNull(
            row,
            "A row was returned.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl delete a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task BaseEntityWithCrudl_DeleteOperation_ExistingRow()
    {
        // Arrange (Given)
        UnitCurrency entity = new UnitCurrency();
        int expectedId = 36; // Australian Dollar
        int expectedCount = 3;

        // Act (When)
        await entity.DeleteAsync(
            expectedId,
            this.TestContext.CancellationToken);

        var row = await entity.ReadAsync(expectedId, this.TestContext.CancellationToken);
        var list = await entity.ListAsync(this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsNull(
            row,
            "A row was returned.");
        Assert.HasCount(
            expectedCount,
            list,
            $"There should only be {expectedCount} rows left in the collection.");
    }

    #endregion Public Methods
}
