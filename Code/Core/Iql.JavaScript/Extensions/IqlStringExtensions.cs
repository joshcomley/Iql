﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TypeSharp.Extensions;

namespace Iql.JavaScript.Extensions
{
    [DoNotConvert]
    class NumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double?) || objectType == typeof(float) || objectType == typeof(float?) || objectType == typeof(decimal) || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return Decimal.Parse(token.ToString(),
                    System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                 token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (Equals(value, null))
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteRawValue(value.ToString());
            }
        }
    }
    public static class IqlStringExtensions
    {

        public static string Clean(this string str)
        {
            str = str.Replace("\r", "");
#if !TypeScript
            str = str.Replace("\"Key\":null,", "");
#endif
            //str = Regex.Replace(str, @"\d\d\d\d-\d\d-\d\dT\d\d:\d\d:\d\d\.\d*\+\d\d:\d\d",
            //    @"0001-01-01T00:00:00.0+00:00");
            return str;
        }

        public static string NormalizeJson(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString().Clean();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
            return jObject.ToString(Formatting.None, new NumberConverter()).Clean();
#endif
        }

        public static string NormalizeJsonNoNulls(this string json)
        {
#if TypeScript
            return JObject.Parse(json).ToString().Clean();
#else
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            var jObject = JToken.Load(reader);
            var newJson = JsonConvert.SerializeObject(jObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new[] { new NumberConverter() }
            }).Clean();
            return newJson;
#endif
        }
    }
}