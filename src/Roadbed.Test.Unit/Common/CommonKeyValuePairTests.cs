namespace Roadbed.Test.Unit;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Common;

/// <summary>
/// Unit tests for the <see cref="CommonKeyValuePair{TKey, TValue}"/> class.
/// </summary>
[TestClass]
public class CommonKeyValuePairTests
{
    #region Public Methods

    /// <summary>
    /// Tests that the default constructor creates an instance with null key and value.
    /// </summary>
    [TestMethod]
    public void Constructor_Default_ShouldCreateInstanceWithNullValues()
    {
        // Arrange & Act
        var pair = new CommonKeyValuePair<string, string>();

        // Assert
        Assert.IsNotNull(pair);
        Assert.IsNull(pair.Key);
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Tests that the parameterized constructor correctly sets the key and value properties.
    /// </summary>
    [TestMethod]
    public void Constructor_WithKeyAndValue_ShouldSetProperties()
    {
        // Arrange
        const string expectedKey = "TestKey";
        const string expectedValue = "TestValue";

        // Act
        var pair = new CommonKeyValuePair<string, string>(expectedKey, expectedValue);

        // Assert
        Assert.AreEqual(expectedKey, pair.Key);
        Assert.AreEqual(expectedValue, pair.Value);
    }

    /// <summary>
    /// Tests that the constructor allows null values for both key and value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithNullKeyAndValue_ShouldAllowNulls()
    {
        // Arrange & Act
        var pair = new CommonKeyValuePair<string?, string?>(null, null);

        // Assert
        Assert.IsNull(pair.Key);
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Tests that two instances with null keys and values are considered equal.
    /// </summary>
    [TestMethod]
    public void Equals_BothWithNullValues_ShouldReturnTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string?, string?>(null, null);
        var pair2 = new CommonKeyValuePair<string?, string?>(null, null);

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests that two instances with different keys are not considered equal.
    /// </summary>
    [TestMethod]
    public void Equals_DifferentKeys_ShouldReturnFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key1", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key2", "Value");

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests that two instances with different values are not considered equal.
    /// </summary>
    [TestMethod]
    public void Equals_DifferentValues_ShouldReturnFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value1");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value2");

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests that two instances with equal keys and values are considered equal.
    /// </summary>
    [TestMethod]
    public void Equals_EqualKeyValuePairs_ShouldReturnTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests that an instance does not equal null.
    /// </summary>
    [TestMethod]
    public void Equals_NullOther_ShouldReturnFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair.Equals((CommonKeyValuePair<string, string>?)null);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests the object overload of Equals with a different type.
    /// </summary>
    [TestMethod]
    public void Equals_Object_WithDifferentType_ShouldReturnFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");
        object otherObject = "Some String";

        // Act
        bool result = pair.Equals(otherObject);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests the object overload of Equals with an equal pair.
    /// </summary>
    [TestMethod]
    public void Equals_Object_WithEqualPair_ShouldReturnTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        object pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests the object overload of Equals with null.
    /// </summary>
    [TestMethod]
    public void Equals_Object_WithNull_ShouldReturnFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair.Equals((object?)null);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests that an instance equals itself (reference equality).
    /// </summary>
    [TestMethod]
    public void Equals_SameInstance_ShouldReturnTrue()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair.Equals(pair);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests that the Key property can be set and retrieved correctly.
    /// </summary>
    [TestMethod]
    public void Key_SetAndGet_ShouldWorkCorrectly()
    {
        // Arrange
        var pair = new CommonKeyValuePair<int, string>();
        const int expectedKey = 42;

        // Act
        pair.Key = expectedKey;

        // Assert
        Assert.AreEqual(expectedKey, pair.Key);
    }

    /// <summary>
    /// Tests the equality operator when both operands are null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_BothNull_ShouldReturnTrue()
    {
        // Arrange
        CommonKeyValuePair<string, string>? pair1 = null;
        CommonKeyValuePair<string, string>? pair2 = null;

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests the equality operator with two different pairs.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_DifferentPairs_ShouldReturnFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key1", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key2", "Value");

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests the equality operator with two equal pairs.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_EqualPairs_ShouldReturnTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests the equality operator when the left operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_LeftNull_ShouldReturnFalse()
    {
        // Arrange
        CommonKeyValuePair<string, string>? pair1 = null;
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests the equality operator when the right operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_RightNull_ShouldReturnFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        CommonKeyValuePair<string, string>? pair2 = null;

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests that the Value property can be set and retrieved correctly.
    /// </summary>
    [TestMethod]
    public void Value_SetAndGet_ShouldWorkCorrectly()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, int>();
        const int expectedValue = 100;

        // Act
        pair.Value = expectedValue;

        // Assert
        Assert.AreEqual(expectedValue, pair.Value);
    }

    #endregion Public Methods
}