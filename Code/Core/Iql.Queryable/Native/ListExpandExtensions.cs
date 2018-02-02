using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Native
{
    public static class ListExpandExtensions
    {
        public static int Count(this IEnumerable source)
        {
            if (source is IList)
            {
                return (source as IList).Count;
            }
            int i = 0;
            foreach (var item in source)
            {
                i++;
            }
            return i;
        }

        public static object ItemAt(this IEnumerable source, int index)
        {
            if (source is IList)
            {
                return (source as IList)[index];
            }
            long i = 0;
            foreach (var item in source)
            {
                if (i == index)
                {
                    return item;
                }
            }
            throw new IndexOutOfRangeException();
        }

        public static void ExpandOneToOne(
            this IEnumerable source,
            IEnumerable target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty
        )
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var sourceEntity = source.ItemAt(i);
                var targetKey = sourceEntity.GetPropertyValueByName(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValueByName(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValueByName(sourceProperty, targetEntity);
                        targetEntity.SetPropertyValueByName(targetProperty, sourceEntity);
                        break;
                    }
                }
            }
        }

        public static void ExpandOneToMany(
            this IList source,
            Type sourceType,
            IList target,
            Type targetType,
            IRelationship relationship)
        {
            typeof(ListExpandExtensions)
                .GetMethod(nameof(ExpandOneToManyTyped))
                .MakeGenericMethod(sourceType, targetType)
                .Invoke(null, new object[]
                {
                    source,
                    sourceType,
                    target,
                    targetType,
                    relationship
#if TypeScript // The type info
                        ,sourceType,targetType
#endif
                });
        }

        public static void ExpandOneToManyTyped<TSource, TTarget>(
            this List<TSource> source,
            Type sourceType,
            List<TTarget> target,
            Type targetType,
            IRelationship relationship) where TSource : class where TTarget : class
        {
            var targetProperty = relationship.Target.Property;
            var sourceDictionary = ToListDictionary(
                source,
                relationship.Constraints.Select(c => c.SourceKeyProperty).ToArray());

            var targetDictionary = ToDictionaryWithList<TTarget, TSource>(
                target,
                relationship.Constraints.Select(c => c.TargetKeyProperty).ToArray(),
                relationship.Target.Property,
                targetEntity =>
                {
                    targetEntity.SetPropertyValue(targetProperty,
                        new RelatedList<TTarget, TSource>(targetEntity, targetProperty.Name));
                });

            foreach (var sourceEntry in sourceDictionary)
            {
                if (targetDictionary.ContainsKey(sourceEntry.Key))
                {
                    ItemAndList<TTarget, TSource> targetEntity = targetDictionary[sourceEntry.Key];
                    foreach (var sourceEntity in sourceEntry.Value)
                    {
                        sourceEntity.SetPropertyValue(relationship.Source.Property, targetEntity.Item);
                        targetEntity.List.Add(sourceEntity);
                    }
                }
            }

            var targetCountProperty = relationship.Target.Configuration.FindProperty(
                $"{relationship.Target.Property.Name}Count");
            if (targetCountProperty != null && targetCountProperty.Kind == PropertyKind.Count)
            {
                foreach (var targetEntity in targetDictionary)
                {
                    targetEntity.Value.Item.SetPropertyValueByName(targetCountProperty.Name,
                        targetEntity.Value.List.Count);
                }
            }
        }

        private class ItemAndList<T, TListItem>
        {
            public T Item { get; set; }
            public List<TListItem> List { get; set; }

            public ItemAndList(T item, List<TListItem> list)
            {
                Item = item;
                List = list;
            }
        }
        private static Dictionary<string, ItemAndList<T, TListItem>> ToDictionaryWithList<T, TListItem>(
            List<T> dataSet,
            IProperty[] properties,
            IProperty listProperty,
            Action<T> action = null)
        {
            var sourceDictionary = new Dictionary<string, ItemAndList<T, TListItem>>();
            for (var i = 0; i < dataSet.Count; i++)
            {
                var entity = dataSet[i];
                if (action != null)
                {
                    action(entity);
                }
                var compositeKey = new CompositeKey();
                foreach (var property in properties)
                {
                    compositeKey.Keys.Add(new KeyValue(property.Name,
                        entity.GetPropertyValue(property),
                        property.ElementType));
                }

                sourceDictionary.Add(compositeKey.AsKeyString(false), 
                    new ItemAndList<T, TListItem>(entity,
                        entity.GetPropertyValueAs<List<TListItem>>(listProperty)));
            }

            return sourceDictionary;
        }

        private static Dictionary<string, List<T>> ToListDictionary<T>(
            List<T> dataSet,
            IProperty[] properties,
            Action<T> action = null)
        {
            var sourceDictionary = new Dictionary<string, List<T>>();
            for (var i = 0; i < dataSet.Count; i++)
            {
                var entity = dataSet[i];
                if (action != null)
                {
                    action(entity);
                }
                var compositeKey = new CompositeKey();
                foreach (var property in properties)
                {
                    compositeKey.Keys.Add(new KeyValue(property.Name,
                        entity.GetPropertyValue(property),
                        property.ElementType));
                }

                var key = compositeKey.AsKeyString(false);
                if (!sourceDictionary.ContainsKey(key))
                {
                    var list = new List<T>();
                    sourceDictionary.Add(key, list);
                }
                sourceDictionary[key].Add(entity);
            }

            return sourceDictionary;
        }

        public static void ExpandManyToMany(
            this IEnumerable source,
            Type sourceType,
            Type targetType,
            IEnumerable target,
            IEnumerable pivot,
            string pivotSourceKeyProperty,
            string pivotTargetKeyPropery,
            string sourceProperty,
            string targetProperty,
            string sourceKeyProperty,
            string targetKeyProperty)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var sourceEntity = source.ItemAt(i);
                if (Equals(sourceEntity.GetPropertyValueByName(sourceProperty), null))
                {
                    sourceEntity.SetPropertyValueByName(sourceProperty,
                        Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType)));
                }
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValueByName(targetProperty), null))
                    {
                        targetEntity.SetPropertyValueByName(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    for (var k = 0; k < pivot.Count(); k++)
                    {
                        var pivotEntity = pivot.ItemAt(k);
                        if (Equals(pivotEntity.GetPropertyValueByName(pivotSourceKeyProperty),
                                sourceEntity.GetPropertyValueByName(sourceKeyProperty)) &&
                            Equals(pivotEntity.GetPropertyValueByName(pivotTargetKeyPropery),
                                targetEntity.GetPropertyValueByName(targetKeyProperty)))
                        {
                            targetEntity.GetPropertyValueByNameAs<IList>(targetProperty).Add(sourceEntity);
                            sourceEntity.GetPropertyValueByNameAs<IList>(sourceProperty).Add(targetEntity);
                        }
                    }
                }
            }
        }
    }
}