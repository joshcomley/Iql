using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Iql.DotNet.Serialization
{
    internal static class DotNetObjectExtensions
    {
        public static string SerializeToXml<T>(this T toSerialize, params Type[] types)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType(), types);
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T DeserializeFromXml<T>(this string xml, params Type[] types)
        {
            using (var reader = XmlReader.Create(new StringReader(xml)))
            {
                if (types == null || !types.Any())
                {
                    types = new[] {typeof(T)};
                }
                foreach (var t in types)
                {
                    var serializer = new XmlSerializer(t, types);
                    if (serializer.CanDeserialize(reader))
                    {
                        return (T) serializer.Deserialize(reader);
                    }
                }
            }
            return default(T);
        }
    }
}