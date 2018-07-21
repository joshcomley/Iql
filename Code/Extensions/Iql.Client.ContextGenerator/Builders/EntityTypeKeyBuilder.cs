using System;
using System.Linq;
using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntityTypeKeyBuilder
    {
        public KeyDefinition Build(
            EntityTypeDefinition type,
            XElement element,
            ODataSchema schema
        )
        {
            var properties = element.ByTagNames("PropertyRef");
            var key = new KeyDefinition();
            foreach (var property in properties)
            {
                var name = property.Attribute("Name").Value;
                var typeProperties = type.Properties.Where(p => p.Name == name).ToList();
                if (typeProperties.Count() > 1)
                {
                    throw new Exception(
                        string.Format("Found more than one property named \"{0}\" on entity type \"{1}\"", name,
                            type.Name));
                }
                key.Properties.Add(typeProperties.Single());
            }
            //property.Dump();
            return key;
            //		property.Elements(
            //		var name = property.Attribute("Name").Value;
            //		var type = property.Attribute("Type").Value;
            //		var nullableAttribute = property.Attribute("Nullable");
            //
            //		var isNullable = true;
            //		if (nullableAttribute != null)
            //		{
            //			isNullable = Convert.ToBoolean(nullableAttribute.Value);
            //		}
            //		PropertyDefinition propertyDefinition;
            //		if (property.Name.LocalName == "NavigationProperty")
            //		{
            //			var navigationPropertyDefinition = new NavigationPropertyDefinition();
            //			propertyDefinition = navigationPropertyDefinition;
            //			navigationPropertyDefinition.EntityType =
            //				new EntityTypeReferenceBuilder().Build(type, schema);
            //		}
            //		else
            //		{
            //			propertyDefinition = new PropertyDefinition();
            //		}
            //		propertyDefinition.Name = name;
            //		propertyDefinition.Type = type;
            //		propertyDefinition.Nullable = isNullable;
            //		return propertyDefinition;
        }
    }
}