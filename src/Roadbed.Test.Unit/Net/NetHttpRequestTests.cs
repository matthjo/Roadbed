namespace Roadbed.Test.Unit.Net;

using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpRequest class.
/// </summary>
[TestClass]
public class NetHttpRequestTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that Authentication property can be set to null.
    /// </summary>
    [TestMethod]
    public void Authentication_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var request = new NetHttpRequest
        {
            Authentication = new NetHttpAuthentication(),
        };

        // Act (When)
        request.Authentication = null;

        // Assert (Then)
        Assert.IsNull(
            request.Authentication,
            "Authentication should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that Authentication property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void Authentication_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedAuth = new NetHttpAuthentication
        {
            AuthenticationType = NetHttpAuthenticationType.Bearer,
            Value = "test-token",
        };

        // Act (When)
        request.Authentication = expectedAuth;

        // Assert (Then)
        Assert.AreSame(
            expectedAuth,
            request.Authentication,
            "Authentication should return the same instance that was set.");
    }

    /// <summary>
    /// Unit test to verify that default timeout constant value is correct.
    /// </summary>
    [TestMethod]
    public void Constructor_DefaultTimeout_MatchesConstantValue()
    {
        // Arrange (Given)
        const int expectedDefaultTimeout = 15;

        // Act (When)
        var request = new NetHttpRequest();

        // Assert (Then)
        Assert.AreEqual(
            expectedDefaultTimeout,
            request.TimeoutInSecondsPerAttempt,
            "Default timeout should match the constant value of 15 seconds.");
    }

    /// <summary>
    /// Unit test to verify that the constructor initializes properties with default values.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithDefaultValues()
    {
        // Arrange (Given)

        // Act (When)
        var request = new NetHttpRequest();

        // Assert (Then)
        Assert.IsNotNull(
            request.Method,
            "Method should not be null.");
        Assert.AreEqual(
            HttpMethod.Get,
            request.Method,
            "Method should be initialized to HttpMethod.Get.");
        Assert.IsTrue(
            request.EnableCompression,
            "EnableCompression should be initialized to true.");
        Assert.AreEqual(
            15,
            request.TimeoutInSecondsPerAttempt,
            "TimeoutInSecondsPerAttempt should be initialized to 15 seconds.");
        Assert.IsNotNull(
            request.RetryPattern,
            "RetryPattern should not be null.");
        Assert.AreEqual(
            3,
            request.RetryPattern.MaxAttempts,
            "RetryPattern.MaxAttempts should be initialized to 3.");
        Assert.AreEqual(
            5,
            request.RetryPattern.DelayMultiplierInSeconds,
            "RetryPattern.DelayMultiplierInSeconds should be initialized to 5.");
        Assert.IsNotNull(
            request.HttpHeaders,
            "HttpHeaders should not be null.");
        Assert.IsEmpty(
            request.HttpHeaders,
            "HttpHeaders should be initialized as empty list.");
        Assert.IsNull(
            request.Authentication,
            "Authentication should be initialized to null.");
        Assert.IsNull(
            request.Content,
            "Content should be initialized to null.");
        Assert.IsNull(
            request.HttpEndPoint,
            "HttpEndPoint should be initialized to null.");
    }

    /// <summary>
    /// Unit test to verify that Content accepts different HttpContent types.
    /// </summary>
    [TestMethod]
    public void Content_SetDifferentContentTypes_AcceptsAllTypes()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var stringContent = new StringContent("test");
        var byteContent = new ByteArrayContent(new byte[] { 1, 2, 3 });
        var formContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("key", "value") });

        // Act & Assert (When & Then)
        request.Content = stringContent;
        Assert.IsInstanceOfType(
            request.Content,
            typeof(StringContent),
            "Content should accept StringContent.");

        request.Content = byteContent;
        Assert.IsInstanceOfType(
            request.Content,
            typeof(ByteArrayContent),
            "Content should accept ByteArrayContent.");

        request.Content = formContent;
        Assert.IsInstanceOfType(
            request.Content,
            typeof(FormUrlEncodedContent),
            "Content should accept FormUrlEncodedContent.");
    }

    /// <summary>
    /// Unit test to verify that Content property can be set to null.
    /// </summary>
    [TestMethod]
    public void Content_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var request = new NetHttpRequest
        {
            Content = new StringContent("test"),
        };

        // Act (When)
        request.Content = null;

        // Assert (Then)
        Assert.IsNull(
            request.Content,
            "Content should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that Content property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void Content_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedContent = new StringContent("test content");

        // Act (When)
        request.Content = expectedContent;

        // Assert (Then)
        Assert.AreSame(
            expectedContent,
            request.Content,
            "Content should return the same instance that was set.");
    }

    /// <summary>
    /// Unit test to verify that EnableCompression property can be set to false.
    /// </summary>
    [TestMethod]
    public void EnableCompression_SetFalse_ReturnsFalse()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.EnableCompression = false;

        // Assert (Then)
        Assert.IsFalse(
            request.EnableCompression,
            "EnableCompression should return false when set to false.");
    }

    /// <summary>
    /// Unit test to verify that EnableCompression property can be set to true.
    /// </summary>
    [TestMethod]
    public void EnableCompression_SetTrue_ReturnsTrue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest
        {
            EnableCompression = false,
        };

        // Act (When)
        request.EnableCompression = true;

        // Assert (Then)
        Assert.IsTrue(
            request.EnableCompression,
            "EnableCompression should return true when set to true.");
    }

    /// <summary>
    /// Unit test to verify that HttpEndPoint property can be set to null.
    /// </summary>
    [TestMethod]
    public void HttpEndPoint_SetNull_ReturnsNull()
    {
        // Arrange (Given)
        var request = new NetHttpRequest
        {
            HttpEndPoint = new Uri("https://example.com"),
        };

        // Act (When)
        request.HttpEndPoint = null;

        // Assert (Then)
        Assert.IsNull(
            request.HttpEndPoint,
            "HttpEndPoint should return null when set to null.");
    }

    /// <summary>
    /// Unit test to verify that HttpEndPoint property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void HttpEndPoint_SetValidUri_ReturnsSetValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedUri = new Uri("https://example.com/api/test");

        // Act (When)
        request.HttpEndPoint = expectedUri;

        // Assert (Then)
        Assert.AreSame(
            expectedUri,
            request.HttpEndPoint,
            "HttpEndPoint should return the same instance that was set.");
    }

    /// <summary>
    /// Unit test to verify that HttpEndPoint accepts various URI schemes.
    /// </summary>
    [TestMethod]
    public void HttpEndPoint_SetVariousUriSchemes_AcceptsAllSchemes()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var httpUri = new Uri("http://example.com");
        var httpsUri = new Uri("https://example.com");

        // Act & Assert (When & Then)
        request.HttpEndPoint = httpUri;
        Assert.AreEqual(
            "http",
            request.HttpEndPoint.Scheme,
            "HttpEndPoint should accept http scheme.");

        request.HttpEndPoint = httpsUri;
        Assert.AreEqual(
            "https",
            request.HttpEndPoint.Scheme,
            "HttpEndPoint should accept https scheme.");
    }

    /// <summary>
    /// Unit test to verify that HttpHeaders property can be modified.
    /// </summary>
    [TestMethod]
    public void HttpHeaders_AddHeader_ContainsAddedHeader()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedHeader = new NetHttpHeader("Content-Type", "application/json");

        // Act (When)
        request.HttpHeaders.Add(expectedHeader);

        // Assert (Then)
        Assert.HasCount(
            1,
            request.HttpHeaders,
            "HttpHeaders should contain one header.");
        Assert.AreSame(
            expectedHeader,
            request.HttpHeaders[0],
            "HttpHeaders should contain the added header.");
    }

    /// <summary>
    /// Unit test to verify that HttpHeaders list is mutable after initialization.
    /// </summary>
    [TestMethod]
    public void HttpHeaders_AddMultipleHeaders_ContainsAllHeaders()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var header1 = new NetHttpHeader("Content-Type", "application/json");
        var header2 = new NetHttpHeader("Authorization", "Bearer token");
        var header3 = new NetHttpHeader("Accept", "application/json");

        // Act (When)
        request.HttpHeaders.Add(header1);
        request.HttpHeaders.Add(header2);
        request.HttpHeaders.Add(header3);

        // Assert (Then)
        Assert.HasCount(3, request.HttpHeaders, "HttpHeaders should contain three headers.");
        Assert.Contains(header1, request.HttpHeaders, "HttpHeaders should contain header1.");
        Assert.Contains(header2, request.HttpHeaders, "HttpHeaders should contain header2.");
        Assert.Contains(header3, request.HttpHeaders, "HttpHeaders should contain header3.");
    }

    /// <summary>
    /// Unit test to verify that HttpHeaders property can be replaced with new list.
    /// </summary>
    [TestMethod]
    public void HttpHeaders_SetNewList_ReturnsNewList()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var newHeaders = new List<NetHttpHeader>
        {
            new NetHttpHeader("Content-Type", "application/json"),
            new NetHttpHeader("Authorization", "Bearer token"),
        };

        // Act (When)
        request.HttpHeaders = newHeaders;

        // Assert (Then)
        Assert.AreSame(
            newHeaders,
            request.HttpHeaders,
            "HttpHeaders should return the new list that was set.");
        Assert.HasCount(
            2,
            request.HttpHeaders,
            "HttpHeaders should contain two headers.");
    }

    /// <summary>
    /// Unit test to verify that Method property can be set to Delete.
    /// </summary>
    [TestMethod]
    public void Method_SetDelete_ReturnsDelete()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.Method = HttpMethod.Delete;

        // Assert (Then)
        Assert.AreEqual(
            HttpMethod.Delete,
            request.Method,
            "Method should return HttpMethod.Delete when set to Delete.");
    }

    /// <summary>
    /// Unit test to verify that Method property can be set to Patch.
    /// </summary>
    [TestMethod]
    public void Method_SetPatch_ReturnsPatch()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.Method = HttpMethod.Patch;

        // Assert (Then)
        Assert.AreEqual(
            HttpMethod.Patch,
            request.Method,
            "Method should return HttpMethod.Patch when set to Patch.");
    }

    /// <summary>
    /// Unit test to verify that Method property can be set to Post.
    /// </summary>
    [TestMethod]
    public void Method_SetPost_ReturnsPost()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.Method = HttpMethod.Post;

        // Assert (Then)
        Assert.AreEqual(
            HttpMethod.Post,
            request.Method,
            "Method should return HttpMethod.Post when set to Post.");
    }

    /// <summary>
    /// Unit test to verify that Method property can be set to Put.
    /// </summary>
    [TestMethod]
    public void Method_SetPut_ReturnsPut()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.Method = HttpMethod.Put;

        // Assert (Then)
        Assert.AreEqual(
            HttpMethod.Put,
            request.Method,
            "Method should return HttpMethod.Put when set to Put.");
    }

    /// <summary>
    /// Unit test to verify that all properties can be set independently.
    /// </summary>
    [TestMethod]
    public void Properties_SetAllIndependently_ReturnsCorrectValues()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedAuth = new NetHttpAuthentication();
        var expectedContent = new StringContent("test");
        var expectedUri = new Uri("https://example.com");
        var expectedHeaders = new List<NetHttpHeader> { new NetHttpHeader("Test", "Value") };
        var expectedPattern = new NetHttpRetryPattern { MaxAttempts = 10 };

        // Act (When)
        request.Authentication = expectedAuth;
        request.Content = expectedContent;
        request.EnableCompression = false;
        request.HttpEndPoint = expectedUri;
        request.HttpHeaders = expectedHeaders;
        request.Method = HttpMethod.Post;
        request.RetryPattern = expectedPattern;
        request.TimeoutInSecondsPerAttempt = 60;

        // Assert (Then)
        Assert.AreSame(expectedAuth, request.Authentication, "Authentication should match.");
        Assert.AreSame(expectedContent, request.Content, "Content should match.");
        Assert.IsFalse(request.EnableCompression, "EnableCompression should be false.");
        Assert.AreSame(expectedUri, request.HttpEndPoint, "HttpEndPoint should match.");
        Assert.AreSame(expectedHeaders, request.HttpHeaders, "HttpHeaders should match.");
        Assert.AreEqual(HttpMethod.Post, request.Method, "Method should be Post.");
        Assert.AreSame(expectedPattern, request.RetryPattern, "RetryPattern should match.");
        Assert.AreEqual(60, request.TimeoutInSecondsPerAttempt, "TimeoutInSecondsPerAttempt should be 60.");
    }

    /// <summary>
    /// Unit test to verify that RetryPattern property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void RetryPattern_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        var expectedPattern = new NetHttpRetryPattern
        {
            MaxAttempts = 5,
            DelayMultiplierInSeconds = 10,
        };

        // Act (When)
        request.RetryPattern = expectedPattern;

        // Assert (Then)
        Assert.AreSame(
            expectedPattern,
            request.RetryPattern,
            "RetryPattern should return the same instance that was set.");
        Assert.AreEqual(
            5,
            request.RetryPattern.MaxAttempts,
            "RetryPattern.MaxAttempts should be 5.");
        Assert.AreEqual(
            10,
            request.RetryPattern.DelayMultiplierInSeconds,
            "RetryPattern.DelayMultiplierInSeconds should be 10.");
    }

    /// <summary>
    /// Unit test to verify that TimeoutInSecondsPerAttempt property can be set to negative value.
    /// </summary>
    [TestMethod]
    public void TimeoutInSecondsPerAttempt_SetNegativeValue_ReturnsNegativeValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        int negativeTimeout = -1;

        // Act (When)
        request.TimeoutInSecondsPerAttempt = negativeTimeout;

        // Assert (Then)
        Assert.AreEqual(
            negativeTimeout,
            request.TimeoutInSecondsPerAttempt,
            "TimeoutInSecondsPerAttempt should return negative value when set to negative value.");
    }

    /// <summary>
    /// Unit test to verify that TimeoutInSecondsPerAttempt property can be set to positive value.
    /// </summary>
    [TestMethod]
    public void TimeoutInSecondsPerAttempt_SetPositiveValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();
        int expectedTimeout = 30;

        // Act (When)
        request.TimeoutInSecondsPerAttempt = expectedTimeout;

        // Assert (Then)
        Assert.AreEqual(
            expectedTimeout,
            request.TimeoutInSecondsPerAttempt,
            "TimeoutInSecondsPerAttempt should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that TimeoutInSecondsPerAttempt property can be set to zero.
    /// </summary>
    [TestMethod]
    public void TimeoutInSecondsPerAttempt_SetZero_ReturnsZero()
    {
        // Arrange (Given)
        var request = new NetHttpRequest();

        // Act (When)
        request.TimeoutInSecondsPerAttempt = 0;

        // Assert (Then)
        Assert.AreEqual(
            0,
            request.TimeoutInSecondsPerAttempt,
            "TimeoutInSecondsPerAttempt should return zero when set to zero.");
    }

    #endregion Public Methods
}