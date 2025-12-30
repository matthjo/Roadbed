namespace Roadbed.Common.Converters;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Custom Newtonsoft.Json converter for the <see cref="IList{T}"/>.
/// </summary>
/// <typeparam name="TKey">Key data type in pair.</typeparam>
/// <typeparam name="TValue">Value data type in pair.</typeparam>
public class CommonKeyValuePairListConverter<TKey, TValue>
    : JsonConverter<IList<CommonKeyValuePair<TKey, TValue>>>
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
    public override IList<CommonKeyValuePair<TKey, TValue>>? ReadJson(
        JsonReader reader,
        Type objectType,
        IList<CommonKeyValuePair<TKey, TValue>>? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var result = new List<CommonKeyValuePair<TKey, TValue>>();
        var obj = JObject.Load(reader);

        foreach (var property in obj.Properties())
        {
            // Convert the property name to TKey
            TKey? key = (TKey?)Convert.ChangeType(property.Name, typeof(TKey));

            if (key is not null)
            {
                // Deserialize the value to TValue
                TValue? value = property.Value.ToObject<TValue>(serializer);

                result.Add(new CommonKeyValuePair<TKey, TValue>(key, value!));
            }
        }

        return result;
    }

    /// <summary>
    /// Logic for serializing the custom KeyValuePair structure.
    /// </summary>
    /// <param name="writer">Newtonsoft.Json writer.</param>
    /// <param name="value">Name/value pair.</param>
    /// <param name="serializer">Newtonsoft.Json serializer.</param>
    public override void WriteJson(
        JsonWriter writer,
        IList<CommonKeyValuePair<TKey, TValue>>? value,
        JsonSerializer serializer)
    {
        writer.WriteStartObject();

        if (value is not null)
        {
            foreach (var pair in value)
            {
                if ((pair is not null) && (pair.Key is not null))
                {
                    string keyString = pair.Key.ToString() ?? string.Empty;

                    writer.WritePropertyName(keyString);
                    serializer.Serialize(writer, pair.Value);
                }
            }
        }

        writer.WriteEndObject();
    }

    #endregion Public Methods
}