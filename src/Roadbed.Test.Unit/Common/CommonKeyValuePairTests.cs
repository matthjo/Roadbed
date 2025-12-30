namespace Roadbed.Test.Unit.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roadbed.Common;

/// <summary>
/// Unit tests for the CommonKeyValuePair class.
/// </summary>
[TestClass]
public class CommonKeyValuePairTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that the default constructor creates a non-null instance.
    /// </summary>
    [TestMethod]
    public void Constructor_Default_CreatesNonNullInstance()
    {
        // Act
        var pair = new CommonKeyValuePair<string, string>();

        // Assert
        Assert.IsNotNull(pair);
    }

    /// <summary>
    /// Verifies that the default constructor creates an instance with null Key.
    /// </summary>
    [TestMethod]
    public void Constructor_Default_SetsKeyToNull()
    {
        // Act
        var pair = new CommonKeyValuePair<string, string>();

        // Assert
        Assert.IsNull(pair.Key);
    }

    /// <summary>
    /// Verifies that the default constructor creates an instance with null Value.
    /// </summary>
    [TestMethod]
    public void Constructor_Default_SetsValueToNull()
    {
        // Act
        var pair = new CommonKeyValuePair<string, string>();

        // Assert
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters accepts null for both key and value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_AcceptsNullForBoth()
    {
        // Arrange
        string? key = null;
        string? value = null;

        // Act
        var pair = new CommonKeyValuePair<string, string>(key!, value!);

        // Assert
        Assert.IsNull(pair.Key);
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters accepts null for key.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_AcceptsNullKey()
    {
        // Arrange
        string? key = null;
        string value = "TestValue";

        // Act
        var pair = new CommonKeyValuePair<string, string>(key!, value);

        // Assert
        Assert.IsNull(pair.Key);
        Assert.AreEqual(value, pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters accepts null for value.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_AcceptsNullValue()
    {
        // Arrange
        string key = "TestKey";
        string? value = null;

        // Act
        var pair = new CommonKeyValuePair<string, string>(key, value!);

        // Assert
        Assert.AreEqual(key, pair.Key);
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters sets the Key property correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_SetsKeyProperty()
    {
        // Arrange
        string key = "TestKey";
        string value = "TestValue";

        // Act
        var pair = new CommonKeyValuePair<string, string>(key, value);

        // Assert
        Assert.AreEqual(key, pair.Key);
    }

    /// <summary>
    /// Verifies that the constructor with parameters sets the Value property correctly.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_SetsValueProperty()
    {
        // Arrange
        string key = "TestKey";
        string value = "TestValue";

        // Act
        var pair = new CommonKeyValuePair<string, string>(key, value);

        // Assert
        Assert.AreEqual(value, pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters works with different generic types.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_WorksWithDifferentGenericTypes()
    {
        // Arrange
        string key = "Count";
        int value = 42;

        // Act
        var pair = new CommonKeyValuePair<string, int>(key, value);

        // Assert
        Assert.AreEqual(key, pair.Key);
        Assert.AreEqual(value, pair.Value);
    }

    /// <summary>
    /// Verifies that the constructor with parameters works with integer types.
    /// </summary>
    [TestMethod]
    public void Constructor_WithParameters_WorksWithIntegerTypes()
    {
        // Arrange
        int key = 42;
        int value = 100;

        // Act
        var pair = new CommonKeyValuePair<int, int>(key, value);

        // Assert
        Assert.AreEqual(key, pair.Key);
        Assert.AreEqual(value, pair.Value);
    }

    /// <summary>
    /// Verifies that Equals returns true for two instances with both null keys and values.
    /// </summary>
    [TestMethod]
    public void Equals_WithBothNullKeysAndValues_ReturnsTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>(null!, null!);
        var pair2 = new CommonKeyValuePair<string, string>(null!, null!);

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that Equals returns false for two instances with different keys.
    /// </summary>
    [TestMethod]
    public void Equals_WithDifferentKeys_ReturnsFalse()
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
    /// Verifies that Equals returns false for two instances with different values.
    /// </summary>
    [TestMethod]
    public void Equals_WithDifferentValues_ReturnsFalse()
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
    /// Verifies that Equals returns true for two instances with identical key and value.
    /// </summary>
    [TestMethod]
    public void Equals_WithIdenticalKeyAndValue_ReturnsTrue()
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
    /// Verifies that Equals returns true for two instances with identical keys and null values.
    /// </summary>
    [TestMethod]
    public void Equals_WithIdenticalKeysAndNullValues_ReturnsTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", null!);
        var pair2 = new CommonKeyValuePair<string, string>("Key", null!);

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that Equals works correctly with integer types.
    /// </summary>
    [TestMethod]
    public void Equals_WithIntegerTypes_WorksCorrectly()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<int, int>(42, 100);
        var pair2 = new CommonKeyValuePair<int, int>(42, 100);
        var pair3 = new CommonKeyValuePair<int, int>(42, 200);

        // Act & Assert
        Assert.IsTrue(pair1.Equals(pair2));
        Assert.IsFalse(pair1.Equals(pair3));
    }

    /// <summary>
    /// Verifies that Equals returns false when comparing with null.
    /// </summary>
    [TestMethod]
    public void Equals_WithNull_ReturnsFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that Equals returns true for two instances with null keys and identical values.
    /// </summary>
    [TestMethod]
    public void Equals_WithNullKeysAndIdenticalValues_ReturnsTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>(null!, "Value");
        var pair2 = new CommonKeyValuePair<string, string>(null!, "Value");

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that Equals returns true when comparing an instance with itself.
    /// </summary>
    [TestMethod]
    public void Equals_WithSameReference_ReturnsTrue()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair.Equals(pair);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that Equals (object overload) returns false when comparing with different type.
    /// </summary>
    [TestMethod]
    public void EqualsObject_WithDifferentType_ReturnsFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");
        object differentType = "NotAPair";

        // Act
        bool result = pair.Equals(differentType);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that Equals (object overload) returns true for two instances with identical key and value.
    /// </summary>
    [TestMethod]
    public void EqualsObject_WithIdenticalKeyAndValue_ReturnsTrue()
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
    /// Verifies that Equals (object overload) returns false when comparing with null.
    /// </summary>
    [TestMethod]
    public void EqualsObject_WithNull_ReturnsFalse()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");
        object? nullObject = null;

        // Act
        bool result = pair.Equals(nullObject);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that Equals (object overload) returns false when comparing with wrong generic type.
    /// </summary>
    [TestMethod]
    public void EqualsObject_WithWrongGenericType_ReturnsFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        object pair2 = new CommonKeyValuePair<int, int>(1, 2);

        // Act
        bool result = pair1.Equals(pair2);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that GetHashCode returns the same value when called multiple times on the same instance.
    /// </summary>
    [TestMethod]
    public void GetHashCode_CalledMultipleTimes_ReturnsConsistentValue()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        int hash1 = pair.GetHashCode();
        int hash2 = pair.GetHashCode();
        int hash3 = pair.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
        Assert.AreEqual(hash2, hash3);
    }

    /// <summary>
    /// Verifies that GetHashCode returns different values for instances with different keys.
    /// </summary>
    [TestMethod]
    public void GetHashCode_WithDifferentKeys_ReturnsDifferentHashCodes()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key1", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key2", "Value");

        // Act
        int hash1 = pair1.GetHashCode();
        int hash2 = pair2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }

    /// <summary>
    /// Verifies that GetHashCode returns different values for instances with different values.
    /// </summary>
    [TestMethod]
    public void GetHashCode_WithDifferentValues_ReturnsDifferentHashCodes()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value1");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value2");

        // Act
        int hash1 = pair1.GetHashCode();
        int hash2 = pair2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }

    /// <summary>
    /// Verifies that GetHashCode returns the same value for instances with identical key and value.
    /// </summary>
    [TestMethod]
    public void GetHashCode_WithIdenticalKeyAndValue_ReturnsSameHashCode()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        int hash1 = pair1.GetHashCode();
        int hash2 = pair2.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
    }

    /// <summary>
    /// Verifies that GetHashCode works correctly with integer types.
    /// </summary>
    [TestMethod]
    public void GetHashCode_WithIntegerTypes_ReturnsValidHashCode()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<int, int>(42, 100);
        var pair2 = new CommonKeyValuePair<int, int>(42, 100);
        var pair3 = new CommonKeyValuePair<int, int>(43, 100);

        // Act
        int hash1 = pair1.GetHashCode();
        int hash2 = pair2.GetHashCode();
        int hash3 = pair3.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
        Assert.AreNotEqual(hash1, hash3);
    }

    /// <summary>
    /// Verifies that equal instances produce the same hash code (hash code consistency).
    /// </summary>
    [TestMethod]
    public void Integration_EqualInstancesHaveSameHashCode_MaintainsConsistency()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act & Assert
        Assert.IsTrue(pair1.Equals(pair2));
        Assert.AreEqual(pair1.GetHashCode(), pair2.GetHashCode());
    }

    /// <summary>
    /// Verifies that the class works correctly in a Dictionary as a key.
    /// </summary>
    [TestMethod]
    public void Integration_UsedAsDictionaryKey_WorksCorrectly()
    {
        // Arrange
        var dictionary = new System.Collections.Generic.Dictionary<CommonKeyValuePair<string, string>, string>();
        var key1 = new CommonKeyValuePair<string, string>("Key1", "Value1");
        var key2 = new CommonKeyValuePair<string, string>("Key1", "Value1");
        var key3 = new CommonKeyValuePair<string, string>("Key2", "Value2");

        // Act
        dictionary[key1] = "Data1";
        dictionary[key2] = "Data2"; // Should overwrite Data1
        dictionary[key3] = "Data3";

        // Assert
        Assert.HasCount(2, dictionary);
        Assert.AreEqual("Data2", dictionary[key1]);
        Assert.AreEqual("Data3", dictionary[key3]);
    }

    /// <summary>
    /// Verifies that the class works correctly in a HashSet (uses both Equals and GetHashCode).
    /// </summary>
    [TestMethod]
    public void Integration_UsedInHashSet_WorksCorrectly()
    {
        // Arrange
        var set = new System.Collections.Generic.HashSet<CommonKeyValuePair<string, string>>();
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair3 = new CommonKeyValuePair<string, string>("Key2", "Value2");

        // Act
        bool added1 = set.Add(pair1);
        bool added2 = set.Add(pair2); // Should not be added (duplicate)
        bool added3 = set.Add(pair3);

        // Assert
        Assert.IsTrue(added1);
        Assert.IsFalse(added2);
        Assert.IsTrue(added3);
        Assert.HasCount(2, set);
    }

    /// <summary>
    /// Verifies that the Key property can be set to null.
    /// </summary>
    [TestMethod]
    public void KeyProperty_SetToNull_AcceptsNull()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("InitialKey", "InitialValue");

        // Act
        pair.Key = null;

        // Assert
        Assert.IsNull(pair.Key);
    }

    /// <summary>
    /// Verifies that the Key property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void KeyProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("InitialKey", "InitialValue");
        string newKey = "UpdatedKey";

        // Act
        pair.Key = newKey;

        // Assert
        Assert.AreEqual(newKey, pair.Key);
    }

    /// <summary>
    /// Verifies that operator == returns true when both operands are null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_WithBothNull_ReturnsTrue()
    {
        // Arrange
        CommonKeyValuePair<string, string> pair1 = null!;
        CommonKeyValuePair<string, string> pair2 = null!;

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that operator == returns false for two instances with different keys.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_WithDifferentKeys_ReturnsFalse()
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
    /// Verifies that operator == returns true for two instances with identical key and value.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_WithIdenticalKeyAndValue_ReturnsTrue()
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
    /// Verifies that operator == returns false when left operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_WithLeftNull_ReturnsFalse()
    {
        // Arrange
        CommonKeyValuePair<string, string> pair1 = null!;
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that operator == returns false when right operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorEquals_WithRightNull_ReturnsFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        CommonKeyValuePair<string, string> pair2 = null!;

        // Act
        bool result = pair1 == pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that operator != returns false when both operands are null.
    /// </summary>
    [TestMethod]
    public void OperatorNotEquals_WithBothNull_ReturnsFalse()
    {
        // Arrange
        CommonKeyValuePair<string, string> pair1 = null!;
        CommonKeyValuePair<string, string> pair2 = null!;

        // Act
        bool result = pair1 != pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that operator != returns true for two instances with different keys.
    /// </summary>
    [TestMethod]
    public void OperatorNotEquals_WithDifferentKeys_ReturnsTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key1", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key2", "Value");

        // Act
        bool result = pair1 != pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that operator != returns false for two instances with identical key and value.
    /// </summary>
    [TestMethod]
    public void OperatorNotEquals_WithIdenticalKeyAndValue_ReturnsFalse()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1 != pair2;

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that operator != returns true when left operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorNotEquals_WithLeftNull_ReturnsTrue()
    {
        // Arrange
        CommonKeyValuePair<string, string> pair1 = null!;
        var pair2 = new CommonKeyValuePair<string, string>("Key", "Value");

        // Act
        bool result = pair1 != pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that operator != returns true when right operand is null.
    /// </summary>
    [TestMethod]
    public void OperatorNotEquals_WithRightNull_ReturnsTrue()
    {
        // Arrange
        var pair1 = new CommonKeyValuePair<string, string>("Key", "Value");
        CommonKeyValuePair<string, string> pair2 = null!;

        // Act
        bool result = pair1 != pair2;

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies that the Value property can be set to null.
    /// </summary>
    [TestMethod]
    public void ValueProperty_SetToNull_AcceptsNull()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("InitialKey", "InitialValue");

        // Act
        pair.Value = null;

        // Assert
        Assert.IsNull(pair.Value);
    }

    /// <summary>
    /// Verifies that the Value property can be updated after construction.
    /// </summary>
    [TestMethod]
    public void ValueProperty_SetValue_UpdatesProperty()
    {
        // Arrange
        var pair = new CommonKeyValuePair<string, string>("InitialKey", "InitialValue");
        string newValue = "UpdatedValue";

        // Act
        pair.Value = newValue;

        // Assert
        Assert.AreEqual(newValue, pair.Value);
    }

    #endregion Public Methods
}