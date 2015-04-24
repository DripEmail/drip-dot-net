/*
 The MIT License (MIT)
 
 Copyright (c) 2015 Drip
 
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

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
