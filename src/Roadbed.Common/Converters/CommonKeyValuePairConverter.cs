namespace Roadbed.Common.Converters;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Custom Newtonsoft.Json converter for <see cref="CommonKeyValuePair{TKey, TValue}"/>.
/// </summary>
/// <typeparam name="TKey">Key data type in pair.</typeparam>
/// <typeparam name="TValue">Value data type in pair.</typeparam>
public class CommonKeyValuePairConverter<TKey, TValue>
    : JsonConverter<CommonKeyValuePair<TKey, TValue>>
{
    #region Public Methods

    /// <summary>
    /// Logic for deserializing the custom KeyValuePair structure.
    /// </summary>
    /// <param name="reader">Newtonsoft.Json reader.</param>
    /// <param name="objectType">Type of object to read.</param>
    /// <param name="existingValue">Object to read.</param>
    /// <param name="hasExistingValue">Indication of the obejct haveing a value.</param>
    /// <param name="serializer">Newtonsoft.Json serializer.</param>
    /// <returns>Object created from the JSON.</returns>
    public override CommonKeyValuePair<TKey, TValue> ReadJson(JsonReader reader, Type objectType, CommonKeyValuePair<TKey, TValue>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        var property = jsonObject.Properties().FirstOrDefault();

        if (property != null)
        {
            TKey? key = property.Name != null ? (TKey)Convert.ChangeType(property.Name, typeof(TKey)) : default;
            TValue? value = serializer.Deserialize<TValue>(property.Value.CreateReader());

            return new CommonKeyValuePair<TKey, TValue> { Key = key, Value = value };
        }

        return default!;
    }

    /// <summary>
    /// Logic for serializing the custom KeyValuePair structure.
    /// </summary>
    /// <param name="writer">Newtonsoft.Json writer.</param>
    /// <param name="value">Name/value pair.</param>
    /// <param name="serializer">Newtonsoft.Json serializer.</param>
    public override void WriteJson(JsonWriter writer, CommonKeyValuePair<TKey, TValue>? value, JsonSerializer serializer)
    {
        if (value == null || value.Key is null)
        {
            return;
        }

        string keyString = value.Key.ToString() ?? string.Empty;

        if (!string.IsNullOrEmpty(keyString))
        {
            writer.WriteStartObject();

            // Use the Key as the JSON property name
            writer.WritePropertyName(keyString);

            // Use the Value as the JSON property value
            serializer.Serialize(writer, value.Value);

            writer.WriteEndObject();
        }
    }

    #endregion Public Methods
}