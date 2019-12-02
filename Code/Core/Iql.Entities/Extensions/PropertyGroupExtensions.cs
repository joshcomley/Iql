using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Geography;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;

namespace Iql.Entities.Extensions
{
    public static class PropertyGroupExtensions
    {
        //public static IPropertyGroup Filter(this IPropertyGroup propertyGroups, DisplayConfigurationKind kind)
        //{
        //    return propertyGroups.FilterInternal(kind, new List<IPropertyGroup>());
        //}

        //private static IPropertyGroup FilterInternal(this IPropertyGroup propertyGroup, DisplayConfigurationKind kind, List<IPropertyGroup> filtered)
        //{
        //    var result = new List<IPropertyGroup>();
        //    foreach (var group in propertyGroup.GetGroupProperties())
        //    {
        //        if (group is ISimpleProperty)
        //        {
        //            switch (kind)
        //            {
        //                case DisplayConfigurationKind.Edit:
        //                    if((group as ISimpleProperty).EditKind == )
        //            }
        //        }
        //    }
        //}

        public static string ResolveGroupKey(this IPropertyGroup propertyGroup)
        {
            return $"__{string.Join("_", propertyGroup.GetGroupProperties().Select(_ => _.Name))}__";
        }

        //public static IEnumerable<IPropertyGroup> PrioritizeForEditing(this IEnumerable<IPropertyGroup> properties)
        //{
        //    if (properties == null)
        //    {
        //        return properties;
        //    }
        //    var propertiesArray = properties as IPropertyGroup[] ?? properties.ToArray();

        //    return propertiesArray.OrderBy(property =>
        //    {
        //        if (property is IProperty)
        //        {
        //            var simpleProperty = property as IProperty;
        //            if (simpleProperty.Kind.HasFlag(IqlPropertyKind.Key))
        //            {
        //                return 0;
        //            }
        //            if (simpleProperty.SearchKind == IqlPropertySearchKind.Primary)
        //            {
        //                return 10;
        //            }
        //            if (simpleProperty.SearchKind == IqlPropertySearchKind.Secondary)
        //            {
        //                return 20;
        //            }
        //        }

        //        if (property is RelationshipDetailBase)
        //        {
        //            var relationship = property as RelationshipDetailBase;
        //            if (relationship.IsCollection)
        //            {
        //                return 50;
        //            }

        //            return 40;
        //        }

        //        return 30;
        //    });
        //}

        public static IEnumerable<IPropertyGroup> PrioritizeForReading(this IEnumerable<IPropertyGroup> properties)
        {
            if (properties == null)
            {
                return properties;
            }
            var propertiesArray = properties as IPropertyGroup[] ?? properties.ToArray();
            double increment = 0.0001;
            return propertiesArray.OrderByWithIndex((property, i) =>
            {
                double index = (i + 1) * increment;
                var order = property.ResolveDisplayOrderKey() + index;
                return order;
            });
        }

        public static int ResolveDisplayOrderKey(this IPropertyContainer property)
        {
            if (property is IGeographicPoint)
            {
                return 0;
            }

            if (property is IFile)
            {
                return 1;
            }

            if (property is IProperty simpleProperty)
            {
                if (simpleProperty.Kind.HasFlag(IqlPropertyKind.Key))
                {
                    return 2;
                }

                if (simpleProperty.EntityConfiguration.PreviewProperty == property)
                {
                    return 3;
                }

                if (simpleProperty.EntityConfiguration.TitleProperty == property)
                {
                    return 4;
                }

                if (simpleProperty.SearchKind == IqlPropertySearchKind.Primary)
                {
                    if (simpleProperty.Matches("title", "name"))
                    {
                        return 30;
                    }

                    return 35;
                }

                if (simpleProperty.SearchKind == IqlPropertySearchKind.Secondary)
                {
                    if (simpleProperty.Matches("firstname", "forename"))
                    {
                        return 40;
                    }

                    if (simpleProperty.Matches("lastname", "surname"))
                    {
                        return 41;
                    }

                    return 45;
                }
            }

            if (property is RelationshipDetailBase)
            {
                var relationship = property as RelationshipDetailBase;
                if (relationship.IsCollection)
                {
                    return 60;
                }

                return 50;
            }

            return 100;
        }
        //    var index = 0;
        //    int GetOrder(IPropertyGroup property)
        //    {
        //        if (property.EntityConfiguration.Key.Properties.Any(p => p == property))
        //        {
        //            return -1;
        //        }
        //        if (property is IProperty simpleProperty)
        //        {
        //            switch (simpleProperty.SearchKind)
        //            {
        //                case IqlPropertySearchKind.Primary:
        //                    if (simpleProperty.Matches("title", "name"))
        //                    {
        //                        return 9;
        //                    }
        //                    return 10;
        //                case IqlPropertySearchKind.Secondary:
        //                    if (simpleProperty.Matches("firstname", "forename"))
        //                    {
        //                        return 20;
        //                    }
        //                    if (simpleProperty.Matches("lastname", "surname"))
        //                    {
        //                        return 21;
        //                    }
        //                    return 22;
        //            }
        //        }
        //        return index + 10000;

        //    }

        //    var ordered = properties.Select(property =>
        //        {
        //            index++;
        //            return new OrderedProperty(property, GetOrder(property));
        //        })
        //        .OrderBy(_ => _.Order);
        //    return ordered.Select(_ => _.Property)
        //        .ToArray(); ;
        //}
        public static ISimpleProperty[] FlattenAllToSimpleProperties(this IEnumerable<IPropertyGroup> propertyGroup)
        {
            var list = new List<ISimpleProperty>();
            foreach (var item in propertyGroup)
            {
                item.FlattenToSimplePropertiesInternal(list);
            }
            return list.Distinct().ToArray();
        }

        public static ISimpleProperty[] FlattenToSimpleProperties(this IPropertyGroup propertyGroup)
        {
            return new[] { propertyGroup }.FlattenAllToSimpleProperties();
        }

        private static List<ISimpleProperty> FlattenToSimplePropertiesInternal(this IPropertyGroup propertyGroup, List<ISimpleProperty> properties)
        {
            if (propertyGroup is ISimpleProperty)
            {
                properties.Add(propertyGroup as ISimpleProperty);
            }
            else
            {
                foreach (var property in propertyGroup.GetGroupProperties())
                {
                    properties.AddRange(property.FlattenToSimpleProperties());
                }
            }
            return properties;
        }


        public static IProperty[] FlattenAllToValueProperties(this IEnumerable<IPropertyGroup> propertyGroup)
        {
            var list = new List<IProperty>();
            foreach (var item in propertyGroup)
            {
                item.FlattenToValuePropertiesInternal(list);
            }
            return list.Distinct().ToArray();
        }

        public static IProperty[] FlattenToValueProperties(this IPropertyGroup propertyGroup)
        {
            return new[] { propertyGroup }.FlattenAllToValueProperties();
        }

        private static List<IProperty> FlattenToValuePropertiesInternal(this IPropertyGroup propertyGroup, List<IProperty> properties)
        {
            if (!propertyGroup.Kind.HasFlag(IqlPropertyKind.Property))
            {
                foreach (var property in propertyGroup.GetGroupProperties())
                {
                    properties.AddRange(property.FlattenToValueProperties());
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
            if (propertyGroup.Kind.HasFlag(IqlPropertyKind.GroupCollection))
            {
                var coll = propertyGroup as IPropertyCollection;
                foreach (var property in coll.GetGroupProperties())
                {
                    property.FlattenInternal(properties);
                }
            }
            return properties;
        }
    }
}