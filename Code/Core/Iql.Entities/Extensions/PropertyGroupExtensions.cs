using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Extensions
{
    public static class PropertyGroupExtensions
    {
        public static IProperty[] FlattenAllToSimpleProperties(this IEnumerable<IPropertyGroup> propertyGroup)
        {
            var list = new List<IProperty>();
            foreach (var item in propertyGroup)
            {
                item.FlattenToSimplePropertiesInternal(list);
            }
            return list.Distinct().ToArray();
        }

        public static IProperty[] FlattenToSimpleProperties(this IPropertyGroup propertyGroup)
        {
            return new[] { propertyGroup }.FlattenAllToSimpleProperties();
        }

        private static List<IProperty> FlattenToSimplePropertiesInternal(this IPropertyGroup propertyGroup, List<IProperty> properties)
        {
            if (!propertyGroup.Kind.HasFlag(PropertyKind.Property))
            {
                foreach (var property in propertyGroup.GetGroupProperties())
                {
                    properties.AddRange(property.FlattenToSimpleProperties());
                }
            }
            else
            {
                properties.Add(propertyGroup.GetGroupProperties()[0] as IProperty);
            }
            return properties;
        }

        public static IPropertyGroup[] FlattenAll(this IEnumerable<IPropertyGroup> propertyGroup)
        {
            var list = new List<IPropertyGroup>();
            foreach (var item in propertyGroup)
            {
                item.FlattenInternal(list);
            }
            return list.Distinct().ToArray();
        }

        public static IPropertyGroup[] Flatten(this IPropertyGroup propertyGroup)
        {
            return new[] { propertyGroup }.FlattenAll();
        }

        private static List<IPropertyGroup> FlattenInternal(this IPropertyGroup propertyGroup, List<IPropertyGroup> properties)
        {
            if (propertyGroup.Kind.HasFlag(PropertyKind.GroupCollection))
            {
                var coll = propertyGroup as IPropertyCollection;
                foreach (var property in coll.Properties)
                {
                    property.FlattenInternal(properties);
                }
            }
            return properties;
        }
    }
}