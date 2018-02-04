using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Native
{
    public class RelationshipExpander<TSource, TTarget>
        where TSource : class where TTarget : class
    {
        public List<TSource> ExpandOneToOne(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship
        )
        {
            var sourceProperty = relationship.Source.Property;
            var targetProperty = relationship.Target.Property;
            var sourceTargetKeyProperty = relationship.Constraints.Select(r => r.SourceKeyProperty).First();
            var targetKeyProperty = relationship.Constraints.Select(r => r.TargetKeyProperty).First();
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < source.Count; i++)
            {
                var sourceEntity = source[i];
                var targetKey = sourceEntity.GetPropertyValue(sourceTargetKeyProperty);
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var j = 0; j < target.Count; j++)
                {
                    var targetEntity = target[j];
                    if (Equals(targetEntity.GetPropertyValue(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                        targetEntity.SetPropertyValue(targetProperty, sourceEntity);
                        break;
                    }
                }
            }

            return source;
        }

        public List<TSource> ExpandOneToMany(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship)
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
                    for (var index = 0; index < sourceEntry.Value.Count; index++)
                    {
                        var sourceEntity = sourceEntry.Value[index];
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
                    if (targetCountProperty.PropertyInfo.PropertyType == typeof(long))
                    {
                        targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                            (long)targetEntity.Value.List.Count);
                    }
                    else
                    {
                        targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                            targetEntity.Value.List.Count);
                    }
                }
            }
            return source;
        }

        private class ItemAndList<T, TListItem>
        {
            public T Item { get; }
            public List<TListItem> List { get; }

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
                for (var j = 0; j < properties.Length; j++)
                {
                    var property = properties[j];
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

        //public static void ExpandManyToMany(
        //    IEnumerable source,
        //    Type sourceType,
        //    Type targetType,
        //    IEnumerable target,
        //    IEnumerable pivot,
        //    string pivotSourceKeyProperty,
        //    string pivotTargetKeyPropery,
        //    string sourceProperty,
        //    string targetProperty,
        //    string sourceKeyProperty,
        //    string targetKeyProperty)
        //{
        //    for (var i = 0; i < source.Count(); i++)
        //    {
        //        var sourceEntity = source.ItemAt(i);
        //        if (Equals(sourceEntity.GetPropertyValueByName(sourceProperty), null))
        //        {
        //            sourceEntity.SetPropertyValueByName(sourceProperty,
        //                Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType)));
        //        }
        //        for (var j = 0; j < target.Count(); j++)
        //        {
        //            var targetEntity = target.ItemAt(j);
        //            if (Equals(targetEntity.GetPropertyValueByName(targetProperty), null))
        //            {
        //                targetEntity.SetPropertyValueByName(targetProperty,
        //                    Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
        //            }
        //            for (var k = 0; k < pivot.Count(); k++)
        //            {
        //                var pivotEntity = pivot.ItemAt(k);
        //                if (Equals(pivotEntity.GetPropertyValueByName(pivotSourceKeyProperty),
        //                        sourceEntity.GetPropertyValueByName(sourceKeyProperty)) &&
        //                    Equals(pivotEntity.GetPropertyValueByName(pivotTargetKeyPropery),
        //                        targetEntity.GetPropertyValueByName(targetKeyProperty)))
        //                {
        //                    targetEntity.GetPropertyValueByNameAs<IList>(targetProperty).Add(sourceEntity);
        //                    sourceEntity.GetPropertyValueByNameAs<IList>(sourceProperty).Add(targetEntity);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}