using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drip
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
                    jsonTextWriter.Formatting = Formatting.Indented;
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
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                propertyName, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();
        }
    }
}

