using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters
{
    public class SelectSubTokenConverter : JsonConverter
    {
        private string[] _subtokens;
        
        public SelectSubTokenConverter(string subtoken)
        {
            _subtokens = subtoken.Split('.');
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WriteSubtoken(0,writer,value,serializer);
        }

        private void WriteSubtoken(int index, JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (index >= _subtokens.Length)
            {
                serializer.Serialize(writer,value);
            } else
            {
                writer.WriteStartObject();
                writer.WritePropertyName(_subtokens[index]);
                WriteSubtoken(index +1, writer,value,serializer);
                writer.WriteEndObject();
            }
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            foreach (var subtoken in _subtokens)
            {
                token = token.SelectToken(subtoken);
            }

            return token.ToObject(objectType);

        }

        public override bool CanConvert(Type objectType) => true;
    }
}