﻿using System.Linq;
using System.Xml.Linq;

namespace Iql.OData.TypeScript.Generator.EntityConfiguration
{
    public static class XDocumentExtensions
    {
        public static XElement RemoveAllNamespaces(this XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }
    }
}