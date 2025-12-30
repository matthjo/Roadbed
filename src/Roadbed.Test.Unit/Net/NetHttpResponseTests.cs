namespace Roadbed.Test.Unit.Net;

using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpResponse record class.
/// </summary>
[TestClass]
public class NetHttpResponseTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that common HTTP status codes are handled correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_CommonHttpStatusCodes_InitializesCorrectly()
    {
        // Arrange (Given)
        var statusCodes = new[]
        {
            (200, "OK", true),
            (201, "Created", true),
            (204, "No Content", true),
            (400, "Bad Request", false),
            (401, "Unauthorized", false),
            (403, "Forbidden", false),
            (404, "Not Found", false),
            (500, "Internal Server Error", false),
        };

        // Act & Assert (When & Then)
        foreach (var (code, description, isSuccess) in statusCodes)
        {
            var response = CreateResponse(code, description, isSuccess, "data", string.Empty);

            Assert.AreEqual(
                code,
                response.HttpStatusCode,
                $"HttpStatusCode should be {code}.");
            Assert.AreEqual(
                description,
                response.HttpStatusCodeDescription,
                $"HttpStatusCodeDescription should be '{description}'.");
            Assert.AreEqual(
                isSuccess,
                response.IsSuccessStatusCode,
                $"IsSuccessStatusCode should be {isSuccess} for status code {code}.");
        }
    }

    /// <summary>
    /// Unit test to verify that the constructor handles empty error string.
    /// </summary>
    [TestMethod]
    public void Constructor_EmptyErrorString_InitializesEmptyErrorsList()
    {
        // Arrange (Given)
        int statusCode = 200;
        string statusDescription = "OK";
        bool isSuccess = true;
        string data = "test";
        string emptyError = string.Empty;

        // Act (When)
        var response = CreateResponse(statusCode, statusDescription, isSuccess, data, emptyError);

        // Assert (Then)
        Assert.IsNotNull(
            response.Errors,
            "Errors list should not be null.");
        Assert.IsEmpty(
            response.Errors,
            "Errors list should be empty when empty error string is provided.");
    }

    /// <summary>
    /// Unit test to verify that the constructor handles null error string.
    /// </summary>
    [TestMethod]
    public void Constructor_NullErrorString_InitializesEmptyErrorsList()
    {
        // Arrange (Given)
        int statusCode = 200;
        string statusDescription = "OK";
        bool isSuccess = true;
        string data = "test";
        string? nullError = null;

        // Act (When)
        var response = CreateResponse(statusCode, statusDescription, isSuccess, data, nullError!);

        // Assert (Then)
        Assert.IsNotNull(
            response.Errors,
            "Errors list should not be null.");
        Assert.IsEmpty(
            response.Errors,
            "Errors list should be empty when null error string is provided.");
    }

    /// <summary>
    /// Unit test to verify that the constructor handles null status description.
    /// </summary>
    [TestMethod]
    public void Constructor_NullStatusDescription_InitializesWithNull()
    {
        // Arrange (Given)
        int statusCode = 200;
        string? nullDescription = null;
        bool isSuccess = true;
        string data = "test";
        string error = string.Empty;

        // Act (When)
        var response = CreateResponse(statusCode, nullDescription, isSuccess, data, error);

        // Assert (Then)
        Assert.IsNull(
            response.HttpStatusCodeDescription,
            "HttpStatusCodeDescription should be null when null is provided.");
    }

    /// <summary>
    /// Unit test to verify that the constructor initializes all properties correctly with valid values.
    /// </summary>
    [TestMethod]
    public void Constructor_ValidParameters_InitializesAllProperties()
    {
        // Arrange (Given)
        int expectedStatusCode = 200;
        string expectedStatusDescription = "OK";
        bool expectedIsSuccess = true;
        string expectedData = "test data";
        string expectedError = "test error";

        // Act (When)
        var response = CreateResponse(
            expectedStatusCode,
            expectedStatusDescription,
            expectedIsSuccess,
            expectedData,
            expectedError);

        // Assert (Then)
        Assert.AreEqual(
            expectedStatusCode,
            response.HttpStatusCode,
            "HttpStatusCode should be initialized to the provided value.");
        Assert.AreEqual(
            expectedStatusDescription,
            response.HttpStatusCodeDescription,
            "HttpStatusCodeDescription should be initialized to the provided value.");
        Assert.AreEqual(
            expectedIsSuccess,
            response.IsSuccessStatusCode,
            "IsSuccessStatusCode should be initialized to the provided value.");
        Assert.AreEqual(
            expectedData,
            response.Data,
            "Data should be initialized to the provided value.");
        Assert.IsNotNull(
            response.Errors,
            "Errors list should not be null.");
        Assert.HasCount(
            1,
            response.Errors,
            "Errors list should contain one error.");
        Assert.AreEqual(
            expectedError,
            response.Errors[0],
            "Error message should match the provided value.");
    }

    /// <summary>
    /// Unit test to verify that the constructor handles whitespace error string.
    /// </summary>
    [TestMethod]
    public void Constructor_WhitespaceErrorString_AddsErrorToList()
    {
        // Arrange (Given)
        int statusCode = 400;
        string statusDescription = "Bad Request";
        bool isSuccess = false;
        string data = "test";
        string whitespaceError = "   ";

        // Act (When)
        var response = CreateResponse(statusCode, statusDescription, isSuccess, data, whitespaceError);

        // Assert (Then)
        Assert.HasCount(
            1,
            response.Errors,
            "Errors list should contain one error when whitespace string is provided.");
        Assert.AreEqual(
            whitespaceError,
            response.Errors[0],
            "Error should match the whitespace string provided.");
    }

    /// <summary>
    /// Unit test to verify that Data property can be modified via internal setter.
    /// </summary>
    [TestMethod]
    public void Data_SetViaInternalSetter_ReturnsNewValue()
    {
        // Arrange (Given)
        var response = InvokeSuccessMethod(200, "OK", "original");
        string newData = "modified";

        // Act (When)
        SetDataProperty(response, newData);

        // Assert (Then)
        Assert.AreEqual(
            newData,
            response.Data,
            "Data should return the new value after being modified.");
    }

    /// <summary>
    /// Unit test to verify that two responses with different status codes are not equal.
    /// </summary>
    [TestMethod]
    public void Equality_DifferentStatusCodes_ReturnsFalse()
    {
        // Arrange (Given)
        var response1 = InvokeSuccessMethod(200, "OK", "data");
        var response2 = InvokeSuccessMethod(201, "OK", "data");

        // Act (When)
        bool areEqual = response1.Equals(response2);

        // Assert (Then)
        Assert.IsFalse(
            areEqual,
            "Responses with different status codes should not be equal.");
    }

    /// <summary>
    /// Unit test to verify that Errors list can be modified after initialization.
    /// </summary>
    [TestMethod]
    public void Errors_AddMultipleErrors_ContainsAllErrors()
    {
        // Arrange (Given)
        var response = InvokeSuccessMethod(200, "OK", "data");
        string error1 = "Error 1";
        string error2 = "Error 2";
        string error3 = "Error 3";

        // Act (When)
        response.Errors.Add(error1);
        response.Errors.Add(error2);
        response.Errors.Add(error3);

        // Assert (Then)
        Assert.HasCount(
            3,
            response.Errors,
            "Errors list should contain three errors.");
        Assert.Contains(error1, response.Errors, "Errors list should contain error1.");
        Assert.Contains(error2, response.Errors, "Errors list should contain error2.");
        Assert.Contains(error3, response.Errors, "Errors list should contain error3.");
    }

    /// <summary>
    /// Unit test to verify that HttpStatusCode property can be modified via internal setter.
    /// </summary>
    [TestMethod]
    public void HttpStatusCode_SetViaInternalSetter_ReturnsNewValue()
    {
        // Arrange (Given)
        var response = InvokeSuccessMethod(200, "OK", "data");
        int newStatusCode = 201;

        // Act (When)
        SetHttpStatusCodeProperty(response, newStatusCode);

        // Assert (Then)
        Assert.AreEqual(
            newStatusCode,
            response.HttpStatusCode,
            "HttpStatusCode should return the new value after being modified.");
    }

    /// <summary>
    /// Unit test to verify that HttpStatusCodeDescription property can be modified via internal setter.
    /// </summary>
    [TestMethod]
    public void HttpStatusCodeDescription_SetViaInternalSetter_ReturnsNewValue()
    {
        // Arrange (Given)
        var response = InvokeSuccessMethod(200, "OK", "data");
        string newDescription = "Created";

        // Act (When)
        SetHttpStatusCodeDescriptionProperty(response, newDescription);

        // Assert (Then)
        Assert.AreEqual(
            newDescription,
            response.HttpStatusCodeDescription,
            "HttpStatusCodeDescription should return the new value after being modified.");
    }

    /// <summary>
    /// Unit test to verify that Success method handles default value data.
    /// </summary>
    [TestMethod]
    public void Success_DefaultValueData_CreatesResponseWithDefaultValue()
    {
        // Arrange (Given)
        int statusCode = 204;
        string statusDescription = "No Content";
        string? defaultData = default;

        // Act (When)
        var response = InvokeSuccessMethod(statusCode, statusDescription, defaultData!);

        // Assert (Then)
        Assert.IsTrue(
            response.IsSuccessStatusCode,
            "IsSuccessStatusCode should be true.");
        Assert.IsNull(
            response.Data,
            "Data should be default (null) when default value is provided.");
    }

    /// <summary>
    /// Unit test to verify that Success method handles null status description.
    /// </summary>
    [TestMethod]
    public void Success_NullStatusDescription_CreatesResponseWithNullDescription()
    {
        // Arrange (Given)
        int statusCode = 201;
        string? nullDescription = null;
        string data = "created";

        // Act (When)
        var response = InvokeSuccessMethod(statusCode, nullDescription, data);

        // Assert (Then)
        Assert.IsTrue(
            response.IsSuccessStatusCode,
            "IsSuccessStatusCode should be true.");
        Assert.IsNull(
            response.HttpStatusCodeDescription,
            "HttpStatusCodeDescription should be null when null is provided.");
    }

    /// <summary>
    /// Unit test to verify that response works with reference type data.
    /// </summary>
    [TestMethod]
    public void Success_ReferenceTypeData_StoresCorrectly()
    {
        // Arrange (Given)
        var expectedData = new List<string> { "item1", "item2", "item3" };

        // Act (When)
        var response = InvokeSuccessMethod(200, "OK", expectedData);

        // Assert (Then)
        Assert.AreSame(
            expectedData,
            response.Data,
            "Data should be the same reference for reference types.");
        Assert.HasCount(
            3,
            response.Data,
            "Data should contain three items.");
    }

    /// <summary>
    /// Unit test to verify that Success method creates response with success status.
    /// </summary>
    [TestMethod]
    public void Success_ValidParameters_CreatesSuccessResponse()
    {
        // Arrange (Given)
        int expectedStatusCode = 200;
        string expectedStatusDescription = "OK";
        string expectedData = "success data";

        // Act (When)
        var response = InvokeSuccessMethod(expectedStatusCode, expectedStatusDescription, expectedData);

        // Assert (Then)
        Assert.IsTrue(
            response.IsSuccessStatusCode,
            "IsSuccessStatusCode should be true for success response.");
        Assert.AreEqual(
            expectedStatusCode,
            response.HttpStatusCode,
            "HttpStatusCode should match the provided value.");
        Assert.AreEqual(
            expectedStatusDescription,
            response.HttpStatusCodeDescription,
            "HttpStatusCodeDescription should match the provided value.");
        Assert.AreEqual(
            expectedData,
            response.Data,
            "Data should match the provided value.");
        Assert.IsEmpty(
            response.Errors,
            "Errors list should be empty for success response.");
    }

    /// <summary>
    /// Unit test to verify that response works with value type data.
    /// </summary>
    [TestMethod]
    public void Success_ValueTypeData_StoresCorrectly()
    {
        // Arrange (Given)
        int expectedData = 42;

        // Act (When)
        var response = InvokeSuccessMethod<int>(200, "OK", expectedData);

        // Assert (Then)
        Assert.AreEqual(
            expectedData,
            response.Data,
            "Data should match the value for value types.");
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Helper method to create a NetHttpResponse instance using reflection.
    /// </summary>
    private static NetHttpResponse<T> CreateResponse<T>(
        int statusCode,
        string? statusDescription,
        bool isSuccess,
        T value,
        string error)
    {
        var constructor = typeof(NetHttpResponse<T>).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            new[] { typeof(int), typeof(string), typeof(bool), typeof(T), typeof(string) },
            null);

        return (NetHttpResponse<T>)constructor!.Invoke(new object?[] { statusCode, statusDescription, isSuccess, value, error });
    }

    /// <summary>
    /// Helper method to invoke the Failure static method using reflection.
    /// </summary>
    private static NetHttpResponse<T> InvokeFailureMethod<T>(int statusCode, string? statusDescription, string error)
    {
        var method = typeof(NetHttpResponse<T>).GetMethod(
            "Failure",
            BindingFlags.NonPublic | BindingFlags.Static);

        return (NetHttpResponse<T>)method!.Invoke(null, new object?[] { statusCode, statusDescription, error }) !;
    }

    /// <summary>
    /// Helper method to invoke the Success static method using reflection.
    /// </summary>
    private static NetHttpResponse<T> InvokeSuccessMethod<T>(int statusCode, string? statusDescription, T value)
    {
        var method = typeof(NetHttpResponse<T>).GetMethod(
            "Success",
            BindingFlags.NonPublic | BindingFlags.Static);

        return (NetHttpResponse<T>)method!.Invoke(null, new object?[] { statusCode, statusDescription, value }) !;
    }

    /// <summary>
    /// Helper method to set the Data property using reflection.
    /// </summary>
    private static void SetDataProperty<T>(NetHttpResponse<T> response, T value)
    {
        var property = typeof(NetHttpResponse<T>).GetProperty("Data");
        property!.SetValue(response, value);
    }

    /// <summary>
    /// Helper method to set the HttpStatusCodeDescription property using reflection.
    /// </summary>
    private static void SetHttpStatusCodeDescriptionProperty<T>(NetHttpResponse<T> response, string? value)
    {
        var property = typeof(NetHttpResponse<T>).GetProperty("HttpStatusCodeDescription");
        property!.SetValue(response, value);
    }

    /// <summary>
    /// Helper method to set the HttpStatusCode property using reflection.
    /// </summary>
    private static void SetHttpStatusCodeProperty<T>(NetHttpResponse<T> response, int value)
    {
        var property = typeof(NetHttpResponse<T>).GetProperty("HttpStatusCode");
        property!.SetValue(response, value);
    }

    #endregion Private Methods
}