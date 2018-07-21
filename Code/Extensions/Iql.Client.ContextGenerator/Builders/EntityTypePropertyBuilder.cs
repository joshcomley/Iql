using System;
using System.Linq;
using System.Xml.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntityTypePropertyBuilder
    {
        public PropertyDefinition Build(XElement property, ODataSchema schema)
        {
            //property.Dump();
            var name = property.Attribute("Name").Value;
            var type = property.Attribute("Type").Value;
            var nullableAttribute = property.Attribute("Nullable");

            var isNullable = true;
            if (nullableAttribute != null)
            {
                isNullable = Convert.ToBoolean(nullableAttribute.Value);
            }
            PropertyDefinition propertyDefinition;
            if (property.Name.LocalName == "NavigationProperty")
            {
                var navigationPropertyDefinition = new NavigationPropertyDefinition();
                var constraints = property.ByTagNames("ReferentialConstraint");
                if (constraints.Any())
                {
                    var constraint = constraints.First();
                    navigationPropertyDefinition.Constraint = new ReferentialConstraint();
                    navigationPropertyDefinition.Constraint.LocalIdProperty = constraint.Attribute("Property").Value;
                    navigationPropertyDefinition.Constraint.RemoteIdProperty =
                        constraint.Attribute("ReferencedProperty").Value;
                }
                navigationPropertyDefinition.Partner = property.Attribute("Partner")?.Value;
                propertyDefinition = navigationPropertyDefinition;
                navigationPropertyDefinition.EntityType =
                    new EntityTypeReferenceBuilder().Build(new TypeInfo() { EdmType = type }, schema);
            }
            else
            {
                propertyDefinition = new PropertyDefinition();
            }
            propertyDefinition.Name = name;

            propertyDefinition.TypeInfo.EdmType = type;
            propertyDefinition.TypeInfo.IsEnum = schema.EnumTypes.Any(t => t.FullName == type);
            propertyDefinition.TypeInfo.Nullable = isNullable;
            return propertyDefinition;
        }
    }
}