using System;
using Newtonsoft.Json;

namespace JsonConverters
{
    public class IntToBooleanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var boolValue = (bool) value;
            writer.WriteValue(boolValue ? "1" : "0");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var intValue = int.Parse(reader.Value.ToString());
            return intValue != 0;
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(int);
    }
}