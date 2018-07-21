using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Iql.OData.TypeScript.Generator.EntityConfiguration
{
    public static class StringEx
    {
        public static T DeserializeFromXml<T>(this string xml, string @namespace = null)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }
            return (T)DeserializeFromXml(typeof(T), xml, @namespace);
        }

        public static object DeserializeFromXml(Type type, string xml, string ns = null)
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;

            var serializer = new XmlSerializer(type, ns);

            var settings = new XmlReaderSettings();
            // No settings need modifying here

            using (var textReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(textReader, settings))
                {
                    return serializer.Deserialize(xmlReader);
                }
            }
        }
    }
}