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
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drip.Protocol
{
    /// <summary>
    /// A serializer based on the one from this blog: http://blog.patrickmriley.net/2014/02/restsharp-using-jsonnet-serializer.html
    /// We use Json.NET to allow for easy naming translations.
    /// </summary>
    public class RestSharpLcaseUnderscoreSerializer : RestSharp.Serializers.ISerializer
    {
        private readonly JsonSerializer serializer;

        public RestSharpLcaseUnderscoreSerializer()
        {
            ContentType = "application/json";
            serializer = new JsonSerializer
            {
                ContractResolver = new LcaseUnderscoreMappingResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public RestSharpLcaseUnderscoreSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            this.serializer = serializer;
        }


        public string ContentType { get; set; }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.None;
                    jsonTextWriter.QuoteChar = '"';
                    serializer.Serialize(jsonTextWriter, obj);
                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }
    }

    /// <summary>
    /// A resolver used by RestSharpLcaseUnderscoreSerializer to convert property names to underscores and lowercases.
    /// From: http://stackoverflow.com/questions/3922874/c-sharp-json-net-convention-that-follows-ruby-property-naming-conventions
    /// </summary>
    public class LcaseUnderscoreMappingResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                propertyName, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();
        }
    }
}
