namespace Roadbed.Test.Integration;

using System.Net.Http;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetService class.
/// </summary>
[TestClass]
public class NetServiceTests
{
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

            var netService = await NetHttpClient.MakeRequestAsync<string>(request, token);

            actualResponse = netService.Data;
        }

        // Assert (Then)
        Assert.AreEqual(
            expectedResponse,
            actualResponse,
            "The response from the external service did not match the expected response.");
    }
}
