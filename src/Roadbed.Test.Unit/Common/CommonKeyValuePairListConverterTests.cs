namespace Roadbed.Test.Unit.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Roadbed.Common;
using Roadbed.Common.Converters;

/// <summary>
/// Unit tests for the <see cref="CommonKeyValuePairListConverter{TKey, TValue}"/> class.
/// </summary>
[TestClass]
public class CommonKeyValuePairListConverterTests
{
    #region Public Methods

    /// <summary>
    /// Verifies that the converter works with JsonSerializerSettings.
    /// </summary>
    [TestMethod]
    public void Integration_WithCustomSerializerSettings_ShouldWorkCorrectly()
    {
        // Arrange
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
        };

        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Key", "Value"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj, settings);
        var deserialized = JsonConvert.DeserializeObject<TestObject>(json, settings);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Data);
        Assert.HasCount(1, deserialized.Data);
        Assert.AreEqual("Key", deserialized.Data[0].Key);
        Assert.AreEqual("Value", deserialized.Data[0].Value);
    }

    /// <summary>
    /// Verifies that ReadJson works with boolean values.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithBooleanValues_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""IsActive"":true,""IsDeleted"":false}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObjectWithBoolValue>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("IsActive", result.Data[0].Key);
        Assert.IsTrue(result.Data[0].Value);
        Assert.AreEqual("IsDeleted", result.Data[1].Key);
        Assert.IsFalse(result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson works with decimal values.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithDecimalValues_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""Price"":19.99,""Tax"":1.50}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObjectWithDecimalValue>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Price", result.Data[0].Key);
        Assert.AreEqual(19.99m, result.Data[0].Value);
        Assert.AreEqual("Tax", result.Data[1].Key);
        Assert.AreEqual(1.50m, result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson handles empty JSON object correctly.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithEmptyObject_ShouldReturnEmptyList()
    {
        // Arrange
        string json = @"{""data"":{}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.IsEmpty(result.Data);
    }

    /// <summary>
    /// Verifies that ReadJson works with integer keys.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithIntegerKeys_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""1"":""First"",""2"":""Second""}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObjectWithIntKey>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual(1, result.Data[0].Key);
        Assert.AreEqual("First", result.Data[0].Value);
        Assert.AreEqual(2, result.Data[1].Key);
        Assert.AreEqual("Second", result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson works with integer values.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithIntegerValues_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""Age"":30,""Count"":100}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObjectWithIntValue>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Age", result.Data[0].Key);
        Assert.AreEqual(30, result.Data[0].Value);
        Assert.AreEqual("Count", result.Data[1].Key);
        Assert.AreEqual(100, result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson handles null data correctly.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithNullData_ShouldReturnNull()
    {
        // Arrange
        string json = @"{""data"":null}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNull(result.Data);
    }

    /// <summary>
    /// Verifies that ReadJson handles null values correctly.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithNullValue_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""Key1"":""Value1"",""Key2"":null}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Key1", result.Data[0].Key);
        Assert.AreEqual("Value1", result.Data[0].Value);
        Assert.AreEqual("Key2", result.Data[1].Key);
        Assert.IsNull(result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson handles special characters in keys.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithSpecialCharactersInKeys_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""Key-With-Dashes"":""Value1"",""Key_With_Underscores"":""Value2""}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Key-With-Dashes", result.Data[0].Key);
        Assert.AreEqual("Value1", result.Data[0].Value);
        Assert.AreEqual("Key_With_Underscores", result.Data[1].Key);
        Assert.AreEqual("Value2", result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson handles Unicode characters correctly.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithUnicodeCharacters_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""名前"":""太郎"",""città"":""Roma""}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("名前", result.Data[0].Key);
        Assert.AreEqual("太郎", result.Data[0].Value);
        Assert.AreEqual("città", result.Data[1].Key);
        Assert.AreEqual("Roma", result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that ReadJson correctly converts JSON object to list of pairs.
    /// </summary>
    [TestMethod]
    public void ReadJson_WithValidJson_ShouldDeserializeCorrectly()
    {
        // Arrange
        string json = @"{""data"":{""Color"":""Red"",""Year"":""2024""}}";

        // Act
        var result = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(2, result.Data);
        Assert.AreEqual("Color", result.Data[0].Key);
        Assert.AreEqual("Red", result.Data[0].Value);
        Assert.AreEqual("Year", result.Data[1].Key);
        Assert.AreEqual("2024", result.Data[1].Value);
    }

    /// <summary>
    /// Verifies that serialization followed by deserialization produces equivalent data.
    /// </summary>
    [TestMethod]
    public void RoundTrip_SerializeAndDeserialize_ShouldPreserveData()
    {
        // Arrange
        var original = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Color", "Blue"),
                new CommonKeyValuePair<string, string>("Size", "Large"),
                new CommonKeyValuePair<string, string>("Year", "2025"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(original);
        var deserialized = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Data);
        Assert.HasCount(original.Data.Count, deserialized.Data);

        for (int i = 0; i < original.Data.Count; i++)
        {
            Assert.AreEqual(original.Data[i].Key, deserialized.Data[i].Key);
            Assert.AreEqual(original.Data[i].Value, deserialized.Data[i].Value);
        }
    }

    /// <summary>
    /// Verifies round-trip with integer types.
    /// </summary>
    [TestMethod]
    public void RoundTrip_WithIntegerTypes_ShouldPreserveData()
    {
        // Arrange
        var original = new TestObjectWithIntValue
        {
            Data = new List<CommonKeyValuePair<string, int>>
            {
                new CommonKeyValuePair<string, int>("Count", 42),
                new CommonKeyValuePair<string, int>("Age", 25),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(original);
        var deserialized = JsonConvert.DeserializeObject<TestObjectWithIntValue>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Data);
        Assert.HasCount(original.Data.Count, deserialized.Data);

        for (int i = 0; i < original.Data.Count; i++)
        {
            Assert.AreEqual(original.Data[i].Key, deserialized.Data[i].Key);
            Assert.AreEqual(original.Data[i].Value, deserialized.Data[i].Value);
        }
    }

    /// <summary>
    /// Verifies round-trip with null values.
    /// </summary>
    [TestMethod]
    public void RoundTrip_WithNullValues_ShouldPreserveNulls()
    {
        // Arrange
        var original = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Key1", "Value1"),
                new CommonKeyValuePair<string, string>("Key2", null!),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(original);
        var deserialized = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Data);
        Assert.HasCount(original.Data.Count, deserialized.Data);
        Assert.AreEqual("Key1", deserialized.Data[0].Key);
        Assert.AreEqual("Value1", deserialized.Data[0].Value);
        Assert.AreEqual("Key2", deserialized.Data[1].Key);
        Assert.IsNull(deserialized.Data[1].Value);
    }

    /// <summary>
    /// Verifies that WriteJson works with boolean values.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithBooleanValues_ShouldSerializeCorrectly()
    {
        // Arrange
        var obj = new TestObjectWithBoolValue
        {
            Data = new List<CommonKeyValuePair<string, bool>>
            {
                new CommonKeyValuePair<string, bool>("IsActive", true),
                new CommonKeyValuePair<string, bool>("IsDeleted", false),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.IsTrue((bool)result.data.IsActive);
        Assert.IsFalse((bool)result.data.IsDeleted);
    }

    /// <summary>
    /// Verifies that WriteJson works with decimal values.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithDecimalValues_ShouldSerializeCorrectly()
    {
        // Arrange
        var obj = new TestObjectWithDecimalValue
        {
            Data = new List<CommonKeyValuePair<string, decimal>>
            {
                new CommonKeyValuePair<string, decimal>("Price", 19.99m),
                new CommonKeyValuePair<string, decimal>("Tax", 1.50m),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual(19.99m, (decimal)result.data.Price);
        Assert.AreEqual(1.50m, (decimal)result.data.Tax);
    }

    /// <summary>
    /// Verifies that WriteJson handles duplicate keys by keeping the last occurrence.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithDuplicateKeys_ShouldSerializeAllOccurrences()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Key", "FirstValue"),
                new CommonKeyValuePair<string, string>("Key", "SecondValue"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        // JSON objects cannot have duplicate keys, so the last value wins
        Assert.AreEqual("SecondValue", (string)result.data.Key);
    }

    /// <summary>
    /// Verifies that WriteJson handles empty list correctly.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithEmptyList_ShouldSerializeAsEmptyObject()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>(),
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.IsNotNull(result.data);
        Assert.AreEqual(0, ((Newtonsoft.Json.Linq.JObject)result.data).Properties().Count());
    }

    /// <summary>
    /// Verifies that WriteJson works with Guid keys.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithGuidKeys_ShouldSerializeCorrectly()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var obj = new TestObjectWithGuidKey
        {
            Data = new List<CommonKeyValuePair<Guid, string>>
            {
                new CommonKeyValuePair<Guid, string>(guid1, "Value1"),
                new CommonKeyValuePair<Guid, string>(guid2, "Value2"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("Value1", (string)result.data[guid1.ToString()]);
        Assert.AreEqual("Value2", (string)result.data[guid2.ToString()]);
    }

    /// <summary>
    /// Verifies that WriteJson works with integer keys.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithIntegerKeys_ShouldSerializeCorrectly()
    {
        // Arrange
        var obj = new TestObjectWithIntKey
        {
            Data = new List<CommonKeyValuePair<int, string>>
            {
                new CommonKeyValuePair<int, string>(1, "First"),
                new CommonKeyValuePair<int, string>(2, "Second"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("First", (string)result.data["1"]);
        Assert.AreEqual("Second", (string)result.data["2"]);
    }

    /// <summary>
    /// Verifies that WriteJson works with integer values.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithIntegerValues_ShouldSerializeCorrectly()
    {
        // Arrange
        var obj = new TestObjectWithIntValue
        {
            Data = new List<CommonKeyValuePair<string, int>>
            {
                new CommonKeyValuePair<string, int>("Age", 30),
                new CommonKeyValuePair<string, int>("Count", 100),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual(30, (int)result.data.Age);
        Assert.AreEqual(100, (int)result.data.Count);
    }

    /// <summary>
    /// Verifies that WriteJson handles keys with ToString returning empty string.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithKeyToStringReturningEmpty_ShouldSerializeWithEmptyKey()
    {
        // Arrange
        var obj = new TestObjectWithCustomKey
        {
            Data = new List<CommonKeyValuePair<CustomKeyWithEmptyToString, string>>
            {
                new CommonKeyValuePair<CustomKeyWithEmptyToString, string>(new CustomKeyWithEmptyToString(), "Value1"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("Value1", (string)result.data[string.Empty]);
    }

    /// <summary>
    /// Verifies that WriteJson handles keys with ToString returning null.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithKeyToStringReturningNull_ShouldSerializeWithEmptyKey()
    {
        // Arrange
        var obj = new TestObjectWithCustomKeyNull
        {
            Data = new List<CommonKeyValuePair<CustomKeyWithNullToString, string>>
            {
                new CommonKeyValuePair<CustomKeyWithNullToString, string>(new CustomKeyWithNullToString(), "Value1"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("Value1", (string)result.data[string.Empty]);
    }

    /// <summary>
    /// Verifies that WriteJson skips pairs with null keys.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithNullKey_ShouldSkipPair()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("ValidKey", "ValidValue"),
                new CommonKeyValuePair<string, string>(null!, "ValueWithNullKey"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("ValidValue", (string)result.data.ValidKey);
        Assert.AreEqual(1, ((Newtonsoft.Json.Linq.JObject)result.data).Properties().Count());
    }

    /// <summary>
    /// Verifies that WriteJson handles null list correctly.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithNullList_ShouldSerializeAsNull()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = null,
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);

        // Assert
        Assert.Contains("\"data\":null", json);
    }

    /// <summary>
    /// Verifies that WriteJson handles pairs with null values correctly.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithNullValue_ShouldSerializeNullValue()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Key1", "Value1"),
                new CommonKeyValuePair<string, string>("Key2", null!),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.AreEqual("Value1", (string)result.data.Key1);
        Assert.IsNull((string)result.data.Key2);
    }

    /// <summary>
    /// Verifies that WriteJson produces the correct JSON structure with string key-value pairs.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithStringPairs_ShouldSerializeCorrectly()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Color", "Red"),
                new CommonKeyValuePair<string, string>("Year", "2024"),
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        dynamic result = JsonConvert.DeserializeObject(json) !;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Red", (string)result.data.Color);
        Assert.AreEqual("2024", (string)result.data.Year);
    }

    #endregion Public Methods

    #region Private Classes

    /// <summary>
    /// Verifies that WriteJson correctly serializes when pair Value property is explicitly null.
    /// </summary>
    [TestMethod]
    public void WriteJson_WithPairValueNull_ShouldSerializeNullCorrectly()
    {
        // Arrange
        var obj = new TestObject
        {
            Data = new List<CommonKeyValuePair<string, string>>
            {
                new CommonKeyValuePair<string, string>("Key1", "Value1"),
                new CommonKeyValuePair<string, string>("Key2", null!),
                new CommonKeyValuePair<string, string>("Key3", "Value3"),
                null!,
            },
        };

        // Act
        string json = JsonConvert.SerializeObject(obj);
        var deserialized = JsonConvert.DeserializeObject<TestObject>(json);

        // Assert
        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Data);
        Assert.HasCount(3, deserialized.Data);
        Assert.AreEqual("Key1", deserialized.Data[0].Key);
        Assert.AreEqual("Value1", deserialized.Data[0].Value);
        Assert.AreEqual("Key2", deserialized.Data[1].Key);
        Assert.IsNull(deserialized.Data[1].Value);
        Assert.AreEqual("Key3", deserialized.Data[2].Key);
        Assert.AreEqual("Value3", deserialized.Data[2].Value);

        // Verify the JSON contains the null value
        Assert.Contains("\"Key2\":null", json);
    }

    /// <summary>
    /// Custom key class that returns empty string from ToString.
    /// </summary>
    private class CustomKeyWithEmptyToString
    {
        #region Public Methods

        /// <summary>
        /// Returns an empty string.
        /// </summary>
        /// <returns>Empty string.</returns>
        public override string ToString() => string.Empty;

        #endregion Public Methods
    }

    /// <summary>
    /// Custom key class that returns null from ToString.
    /// </summary>
    private class CustomKeyWithNullToString
    {
        #region Public Methods

        /// <summary>
        /// Returns null.
        /// </summary>
        /// <returns>Null value.</returns>
        public override string? ToString() => null;

        #endregion Public Methods
    }

    /// <summary>
    /// Test class that uses the converter for serialization/deserialization.
    /// </summary>
    private class TestObject
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with string key and string value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<string, string>))]
        public IList<CommonKeyValuePair<string, string>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with boolean values.
    /// </summary>
    private class TestObjectWithBoolValue
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with string key and boolean value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<string, bool>))]
        public IList<CommonKeyValuePair<string, bool>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with custom key type returning empty ToString.
    /// </summary>
    private class TestObjectWithCustomKey
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with custom key and string value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<CustomKeyWithEmptyToString, string>))]
        public IList<CommonKeyValuePair<CustomKeyWithEmptyToString, string>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with custom key type returning null ToString.
    /// </summary>
    private class TestObjectWithCustomKeyNull
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with custom key and string value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<CustomKeyWithNullToString, string>))]
        public IList<CommonKeyValuePair<CustomKeyWithNullToString, string>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with decimal values.
    /// </summary>
    private class TestObjectWithDecimalValue
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with string key and decimal value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<string, decimal>))]
        public IList<CommonKeyValuePair<string, decimal>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with Guid keys.
    /// </summary>
    private class TestObjectWithGuidKey
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with Guid key and string value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<Guid, string>))]
        public IList<CommonKeyValuePair<Guid, string>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with integer keys.
    /// </summary>
    private class TestObjectWithIntKey
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with integer key and string value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<int, string>))]
        public IList<CommonKeyValuePair<int, string>>? Data { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Test class that uses the converter with integer values.
    /// </summary>
    private class TestObjectWithIntValue
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the data property with string key and integer value.
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(CommonKeyValuePairListConverter<string, int>))]
        public IList<CommonKeyValuePair<string, int>>? Data { get; set; }

        #endregion Public Properties
    }
    #endregion Private Classes
}