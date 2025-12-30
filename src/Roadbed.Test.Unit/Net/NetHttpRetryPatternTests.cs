namespace Roadbed.Test.Unit.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Net;

/// <summary>
/// Contains unit tests for verifying the behavior of the NetHttpRetryPattern class.
/// </summary>
[TestClass]
public class NetHttpRetryPatternTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the default constructor initializes properties correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_NoParameters_InitializesWithDefaultValues()
    {
        // Arrange (Given)

        // Act (When)
        var retryPattern = new NetHttpRetryPattern();

        // Assert (Then)
        Assert.AreEqual(
            default(int),
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should be initialized to default value.");
        Assert.AreEqual(
            default(int),
            retryPattern.MaxAttempts,
            "MaxAttempts should be initialized to default value.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds property can be set to large positive value.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetLargePositiveValue_ReturnsLargeValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int largeDelay = 3600;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = largeDelay;

        // Assert (Then)
        Assert.AreEqual(
            largeDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return large value when set to large value.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds can be set to maximum integer value.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetMaxValue_ReturnsMaxValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int maxValue = int.MaxValue;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = maxValue;

        // Assert (Then)
        Assert.AreEqual(
            maxValue,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return int.MaxValue when set to int.MaxValue.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds can be set to minimum integer value.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetMinValue_ReturnsMinValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int minValue = int.MinValue;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = minValue;

        // Assert (Then)
        Assert.AreEqual(
            minValue,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return int.MinValue when set to int.MinValue.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds property can be set to negative value.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetNegativeValue_ReturnsNegativeValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int negativeDelay = -5;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = negativeDelay;

        // Assert (Then)
        Assert.AreEqual(
            negativeDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return negative value when set to negative value.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int expectedDelay = 5;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = expectedDelay;

        // Assert (Then)
        Assert.AreEqual(
            expectedDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that DelayMultiplierInSeconds property can be set to zero.
    /// </summary>
    [TestMethod]
    public void DelayMultiplierInSeconds_SetZero_ReturnsZero()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = 0;

        // Assert (Then)
        Assert.AreEqual(
            0,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return zero when set to zero.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts property can be set to large positive value.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetLargePositiveValue_ReturnsLargeValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int largeAttempts = 100;

        // Act (When)
        retryPattern.MaxAttempts = largeAttempts;

        // Assert (Then)
        Assert.AreEqual(
            largeAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should return large value when set to large value.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts can be set to maximum integer value.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetMaxValue_ReturnsMaxValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int maxValue = int.MaxValue;

        // Act (When)
        retryPattern.MaxAttempts = maxValue;

        // Assert (Then)
        Assert.AreEqual(
            maxValue,
            retryPattern.MaxAttempts,
            "MaxAttempts should return int.MaxValue when set to int.MaxValue.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts can be set to minimum integer value.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetMinValue_ReturnsMinValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int minValue = int.MinValue;

        // Act (When)
        retryPattern.MaxAttempts = minValue;

        // Assert (Then)
        Assert.AreEqual(
            minValue,
            retryPattern.MaxAttempts,
            "MaxAttempts should return int.MinValue when set to int.MinValue.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts property can be set to negative value.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetNegativeValue_ReturnsNegativeValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int negativeAttempts = -1;

        // Act (When)
        retryPattern.MaxAttempts = negativeAttempts;

        // Assert (Then)
        Assert.AreEqual(
            negativeAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should return negative value when set to negative value.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts property can be set and retrieved.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetValidValue_ReturnsSetValue()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int expectedAttempts = 3;

        // Act (When)
        retryPattern.MaxAttempts = expectedAttempts;

        // Assert (Then)
        Assert.AreEqual(
            expectedAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should return the value that was set.");
    }

    /// <summary>
    /// Unit test to verify that MaxAttempts property can be set to zero.
    /// </summary>
    [TestMethod]
    public void MaxAttempts_SetZero_ReturnsZero()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();

        // Act (When)
        retryPattern.MaxAttempts = 0;

        // Assert (Then)
        Assert.AreEqual(
            0,
            retryPattern.MaxAttempts,
            "MaxAttempts should return zero when set to zero.");
    }

    /// <summary>
    /// Unit test to verify that object initializer syntax works correctly.
    /// </summary>
    [TestMethod]
    public void ObjectInitializer_SetBothProperties_InitializesCorrectly()
    {
        // Arrange (Given)
        int expectedDelay = 7;
        int expectedAttempts = 4;

        // Act (When)
        var retryPattern = new NetHttpRetryPattern
        {
            DelayMultiplierInSeconds = expectedDelay,
            MaxAttempts = expectedAttempts,
        };

        // Assert (Then)
        Assert.AreEqual(
            expectedDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should be initialized via object initializer.");
        Assert.AreEqual(
            expectedAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should be initialized via object initializer.");
    }

    /// <summary>
    /// Unit test to verify that properties handle common retry pattern configurations.
    /// </summary>
    [TestMethod]
    public void Properties_CommonRetryConfigurations_StoresCorrectly()
    {
        // Arrange (Given)
        var configurations = new[]
        {
            (MaxAttempts: 1, DelayMultiplier: 0),
            (MaxAttempts: 3, DelayMultiplier: 1),
            (MaxAttempts: 3, DelayMultiplier: 2),
            (MaxAttempts: 3, DelayMultiplier: 5),
            (MaxAttempts: 5, DelayMultiplier: 10),
            (MaxAttempts: 10, DelayMultiplier: 30),
        };

        // Act & Assert (When & Then)
        foreach (var (maxAttempts, delayMultiplier) in configurations)
        {
            var retryPattern = new NetHttpRetryPattern
            {
                MaxAttempts = maxAttempts,
                DelayMultiplierInSeconds = delayMultiplier,
            };

            Assert.AreEqual(
                maxAttempts,
                retryPattern.MaxAttempts,
                $"MaxAttempts should be {maxAttempts}.");
            Assert.AreEqual(
                delayMultiplier,
                retryPattern.DelayMultiplierInSeconds,
                $"DelayMultiplierInSeconds should be {delayMultiplier}.");
        }
    }

    /// <summary>
    /// Unit test to verify that properties work correctly with typical exponential backoff values.
    /// </summary>
    [TestMethod]
    public void Properties_ExponentialBackoffPattern_StoresCorrectly()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();

        // Typical exponential backoff: 2^0 * 2, 2^1 * 2, 2^2 * 2 seconds
        int baseDelay = 2;
        int maxRetries = 3;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = baseDelay;
        retryPattern.MaxAttempts = maxRetries;

        // Assert (Then)
        Assert.AreEqual(
            baseDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should support exponential backoff base value.");
        Assert.AreEqual(
            maxRetries,
            retryPattern.MaxAttempts,
            "MaxAttempts should support typical retry count.");
    }

    /// <summary>
    /// Unit test to verify that properties can be modified multiple times.
    /// </summary>
    [TestMethod]
    public void Properties_ModifyMultipleTimes_ReturnsLatestValues()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int initialDelay = 5;
        int finalDelay = 15;
        int initialAttempts = 3;
        int finalAttempts = 7;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = initialDelay;
        retryPattern.MaxAttempts = initialAttempts;
        retryPattern.DelayMultiplierInSeconds = finalDelay;
        retryPattern.MaxAttempts = finalAttempts;

        // Assert (Then)
        Assert.AreEqual(
            finalDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return the latest value that was set.");
        Assert.AreEqual(
            finalAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should return the latest value that was set.");
    }

    /// <summary>
    /// Unit test to verify that both properties can be set independently.
    /// </summary>
    [TestMethod]
    public void Properties_SetBothIndependently_ReturnCorrectValues()
    {
        // Arrange (Given)
        var retryPattern = new NetHttpRetryPattern();
        int expectedDelay = 10;
        int expectedAttempts = 5;

        // Act (When)
        retryPattern.DelayMultiplierInSeconds = expectedDelay;
        retryPattern.MaxAttempts = expectedAttempts;

        // Assert (Then)
        Assert.AreEqual(
            expectedDelay,
            retryPattern.DelayMultiplierInSeconds,
            "DelayMultiplierInSeconds should return the value that was set.");
        Assert.AreEqual(
            expectedAttempts,
            retryPattern.MaxAttempts,
            "MaxAttempts should return the value that was set.");
    }

    #endregion Public Methods
}