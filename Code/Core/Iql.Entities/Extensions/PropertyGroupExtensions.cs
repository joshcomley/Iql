using System.Collections.Generic;

namespace Iql.Entities.Extensions
{
    public static class PropertyGroupExtensions
    {
        public static IProperty[] Flatten(this IPropertyGroup propertyGroup)
        {
            return propertyGroup.FlattenInternal(new List<IProperty>()).ToArray();
        }
        private static List<IProperty> FlattenInternal(this IPropertyGroup propertyGroup, List<IProperty> properties)
        {
            if (propertyGroup is IProperty)
            {
                var p = propertyGroup as IProperty;
                if (!properties.Contains(p))
                {
                    properties.Add(p);
                }

                return properties;
            }

            foreach (var property in propertyGroup.GetGroupProperties())
            {
                property.FlattenInternal(properties);
            }

            return properties;
        }
    }
}