using System.Text.Json;

namespace SyntaxeDotNet
{
    internal class SyntaxeJsonHandler
    {
        /// <summary>
        /// Parse JSON string into a nested dictionary
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns></returns>
        public Dictionary<string, object> ParseJson(string json)
        {
            using(JsonDocument document = JsonDocument.Parse(json))
            {
                return ParseJsonElement(document.RootElement);
            }
        }

        /// <summary>
        /// Recursively convert JsonElement to Dictionary<string, object>
        /// </summary>
        /// <param name="element">JSON element</param>
        /// <returns></returns>
        private Dictionary<string, object> ParseJsonElement(JsonElement element)
        {
            var dictionary = new Dictionary<string, object>();

            foreach(JsonProperty property in element.EnumerateObject())
            {
                dictionary[property.Name] = (property.Value.ValueKind == JsonValueKind.Object)
                    ? ParseJsonElement(property.Value) : JsonElementToValue(property.Value);
            }

            return dictionary;
        }

        /// <summary>
        /// Convert JsonElement into a corresponding .NET type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private object JsonElementToValue(JsonElement element)
        {
            switch(element.ValueKind)
            {
                case JsonValueKind.String:
                    return element.GetString()!;
                case JsonValueKind.Number:
                    if (element.TryGetInt32(out int intValue))
                        return intValue;
                    if (element.TryGetInt64(out long longValue))
                        return longValue;
                    if (element.TryGetDouble(out double doubleValue))
                        return doubleValue;
                    return element.GetDecimal()!;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.Null:
                    return null!;
                default:
                    return element.ToString();
            }
        }
    }
}
