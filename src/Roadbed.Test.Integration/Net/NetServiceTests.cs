namespace Roadbed.Test.Integration;

using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roadbed.Common;
using Roadbed.Net;
using Roadbed.Net.Installers;
using Roadbed.Test.Integration.Net.Mocks;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetService class.
/// </summary>
[TestClass]
public class NetServiceTests
{
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NetServiceTests"/> class.
    /// </summary>
    public NetServiceTests()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var installer = new InstallNetHttpClient();

        // Install HTTP Client Factory
        installer.ConfigureServices(services, configuration);
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets object used to store information that is provided to unit tests.
    /// </summary>
    public TestContext TestContext { get; set; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task NetService_CreateOperation_NewRow()
    {
        // Arrange (Given)
        IList<CommonKeyValuePair<string, string>> expectedAttributes = new List<CommonKeyValuePair<string, string>>()
        {
            { new CommonKeyValuePair<string, string>("Color", "Red") },
            { new CommonKeyValuePair<string, string>("Year", "2024") },
        };
        IntegrationObjectRow expectedDto = new IntegrationObjectRow("Test Object", expectedAttributes);
        IntegrationObject entity = new IntegrationObject();

        // Create New Record for Unit Test
        var newid = await entity.CreateAsync(
            expectedDto,
            this.TestContext.CancellationToken);

        // Verify Record Created
        Assert.IsNotNull(
            newid,
            "Integration Object was not inserted.");

        // Read the Record to Double Check
        var row = await entity.ReadAsync(newid, this.TestContext.CancellationToken);

        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            newid,
            row.Id,
            "ID values between the create and read operations don't match.");
        Assert.AreEqual(
            expectedDto.Name,
            row.Name,
            "Currency Name doesn't match.");
        Assert.HasCount(
            expectedDto.Attributes.Count,
            row.Attributes,
            $"There should be {expectedDto.Attributes.Count} rows in the collection.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task NetService_DeleteOperation_NewRow()
    {
        // Arrange (Given)
        IList<CommonKeyValuePair<string, string>> expectedAttributes = new List<CommonKeyValuePair<string, string>>()
        {
            { new CommonKeyValuePair<string, string>("Call Capable", "True") },
            { new CommonKeyValuePair<string, string>("Text Capable", "True") },
        };
        IntegrationObjectRow expectedDto = new IntegrationObjectRow("Bell 1776", expectedAttributes);
        IntegrationObject entity = new IntegrationObject();

        // Create New Record for Unit Test
        string? newid = await entity.CreateAsync(
            expectedDto,
            this.TestContext.CancellationToken);

        // Verify Record Created
        Assert.IsNotNull(
            newid,
            "The returned row is null.");

        // Delete the Record
        await entity.DeleteAsync(
            newid,
            this.TestContext.CancellationToken);

        // Try to Read the Deleted Record
        var row = await entity.ReadAsync(newid, this.TestContext.CancellationToken);

        // Verify that the Record was Deleted
        Assert.IsNull(
            row,
            "A row was returned after deletion.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task NetService_ListOperation_RowsReturned()
    {
        // Arrange (Given)
        IntegrationObject entity = new IntegrationObject();

        // Act (When)
        var list = await entity.ListAsync(this.TestContext.CancellationToken);

        // Assert (Then)
        Assert.IsGreaterThan(
            0,
            list.Count,
            "No rows were returned.");
    }

    /// <summary>
    /// Unit test to verify that content extracted correctly from embedded resource.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task NetService_MakeRequestAsync_GetValidJsonResponse()
    {
        // Arrange (Given)
        string expectedResponse = "{\"id\":\"4\",\"name\":\"Apple iPhone 11, 64GB\",\"data\":{\"price\":389.99,\"color\":\"Purple\"}}";
        NetHttpRequest request = new NetHttpRequest
        {
            Method = HttpMethod.Get,
            HttpEndPoint = new Uri("https://api.restful-api.dev/objects/4"),
        };

        // Act (When)
        string actualResponse = string.Empty;

        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Get the token to pass to async methods
            CancellationToken token = cts.Token;

            var netService = await NetHttpClientService.MakeRequestAsync<string>(request, token);

            actualResponse = netService.Data;
        }

        // Assert (Then)
        Assert.AreEqual(
            expectedResponse,
            actualResponse,
            "The response from the external service did not match the expected response.");
    }

    /// <summary>
    /// Unit test to verify that BaseEntityWithCrudl creates a row with the ID correctly.
    /// </summary>
    /// <returns>A unit of work representing when operation has been completed.</returns>
    [TestMethod]
    public async Task NetService_UpdateOperation_NewRow()
    {
        // Arrange (Given)
        IList<CommonKeyValuePair<string, string>> expectedAttributes = new List<CommonKeyValuePair<string, string>>()
        {
            { new CommonKeyValuePair<string, string>("Size", "Medium") },
            { new CommonKeyValuePair<string, string>("Network", "5G") },
        };
        IntegrationObjectRow createDto = new IntegrationObjectRow("e-Flip", expectedAttributes);
        IntegrationObject entity = new IntegrationObject();

        // Create New Record for Unit Test
        string? newid = await entity.CreateAsync(
            createDto,
            this.TestContext.CancellationToken);

        // Verify Record Created
        Assert.IsNotNull(
            newid,
            "The returned row is null.");

        // Add ID to new DTO for Update
        IntegrationObjectRow expectedDto = new IntegrationObjectRow(newid, "eFlip", expectedAttributes);
        await entity.UpdateAsync(
            expectedDto,
            this.TestContext.CancellationToken);

        // Read the Updated Record
        var row = await entity.ReadAsync(newid, this.TestContext.CancellationToken);

        // Verify the Updated
        Assert.IsNotNull(
            row,
            "The returned row is null.");
        Assert.AreEqual(
            newid,
            row.Id,
            "ID values between the create and read operations don't match.");
        Assert.AreEqual(
            expectedDto.Name,
            row.Name,
            "Name doesn't match.");
    }

    #endregion Public Methods
}