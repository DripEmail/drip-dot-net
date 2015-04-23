using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Drip.Protocol
{
    public class UntouchableKeysConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var genericDictionaryType = objectType.GetInterfaces().Where(x =>
              x.IsGenericType &&
              x.GetGenericTypeDefinition() == typeof(IDictionary<,>)).FirstOrDefault();

            if (null == genericDictionaryType)
            {
                return false;
            }

            var arguments = genericDictionaryType.GetGenericArguments();

            return (arguments[0] == typeof(string));
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new InvalidOperationException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var objectType = value.GetType();

            var genericDictionaryType = objectType.GetInterfaces().Where(x =>
              x.IsGenericType &&
              x.GetGenericTypeDefinition() == typeof(IDictionary<,>)).FirstOrDefault();

            if (null == genericDictionaryType)
            {
                serializer.Serialize(writer, value);
                return;
            }

            var arguments = genericDictionaryType.GetGenericArguments();

            if (arguments[0] != typeof(string))
            {
                serializer.Serialize(writer, value);
                return;
            }

            IDictionary dictionary = (IDictionary)value;

            writer.WriteStartObject();

            foreach (DictionaryEntry entry in dictionary)
            {
                string key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
                writer.WritePropertyName(key);
                serializer.Serialize(writer, entry.Value);
            }

            writer.WriteEndObject();
        }
    }
}
