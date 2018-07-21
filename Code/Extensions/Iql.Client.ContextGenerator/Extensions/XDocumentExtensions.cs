using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Iql.OData.TypeScript.Generator.Extensions
{
    internal static class XDocumentExtensions
    {
        public static string ODataNamespace(this XElement element)
        {
            var @namespace = element.Ancestors().Single(a => a.Name.LocalName == "Schema").Attribute("Namespace").Value;
            return @namespace;
        }

        public static IEnumerable<XElement> ByTagNames(this XContainer doc, params string[] tagNames)
        {
            return doc.Descendants().Where(d => tagNames.Contains(d.Name.LocalName));
        }
    }
}