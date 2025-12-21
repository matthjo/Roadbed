namespace Roadbed.Test.Unit;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Roadbed.Net;

/// <summary>
/// Tests for the NetServiceClient class.
/// </summary>
[TestClass]
public class NetServiceClientTests
{
    /// <summary>
    /// Mock Http Request for testing.
    /// </summary>
    private NetHttpRequest mockRequest = new NetHttpRequest
    {
        EnableCompression = true,
        Authentication = null,
        RetryPattern = new NetHttpRetryPattern
        {
            MaxAttempts = 3,
            DelayMultiplierInSeconds = 2,
        },
        Content = null,
        HttpEndPoint = new Uri("https://example.com/api/data"),
        HttpHeaders = new List<NetHttpHeader>
            {
                new NetHttpHeader { Name = "Accept", Value = "application/json" },
                new NetHttpHeader { Name = "User-Agent", Value = "RoadbedClient/1.0" },
            },
        Method = HttpMethod.Get,
        TimeoutInSecondsPerAttempt = 10,
    };


    /// <summary>
    /// Method to test the NetServiceClient method.
    /// </summary>
    [TestMethod]
    public void PrivateStaticMethod_ShouldReturnExpectedValue()
    {
        // Arrange
        PrivateType privateType = new PrivateType(typeof(NetService));
        object[] parameters = { request, mockHandler }; // if your method takes parameters

        // Act
        // InvokeStatic handles the reflection internally
        string result = (string)privateType.InvokeStatic("CreateHttpClient", parameters);


        var instance = new CreateHttpClient();
        MethodInfo method = typeof(ClassWithPrivateMethod).GetMethod("PrivateMethodName", BindingFlags.NonPublic | BindingFlags.Instance);
        object result = method.Invoke(instance, new object[] { param1, param2 });

        // Assert the result
        Assert.AreEqual(expectedValue, result);

    }
}



using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MyClassTests
{
    [TestMethod]
    public void PrivateStaticMethod_WithPrivateType_ShouldWork()
    {
        // Arrange
        PrivateType privateType = new PrivateType(typeof(ClassWithPrivateStaticMethod));
        var expected = 55;
        object[] parameters = { 33, 22 }; // if your method takes parameters

        // Act
        // InvokeStatic handles the reflection internally
        string result = (string)privateType.InvokeStatic("MyMethodToTest", parameters);

        // Assert
        Assert.IsTrue(result.Contains(expected.ToString()));
    }
}
