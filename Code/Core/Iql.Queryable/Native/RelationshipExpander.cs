using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Native
{
    public class RelationshipExpander
    {
        static RelationshipExpander()
        {
            OneToOneMethod = GetMethod(nameof(FindOneToOneMatchesTyped));
            OneToManyMethod = GetMethod(nameof(FindOneToManyMatchesTyped));
        }

        public RelationshipExpander()
        {
            ToDictionaryWithListLookup = new Dictionary<IList, Dictionary<IRelationshipDetail, IDictionary>>();
            GroupByRelationshipLookup = new Dictionary<IList, Dictionary<IRelationshipDetail, Dictionary<string, IList>>>();
        }

        private static MethodInfo OneToOneMethod { get; }
        private static MethodInfo OneToManyMethod { get; }

        private static MethodInfo GetMethod(string methodName)
        {
            return typeof(RelationshipExpander)
                .GetMethod(methodName);
        }

        public IList FindTargetEntities(
            IList source,
            IList target,
            IRelationship relationship,
            bool assignRelationships
        )
        {
            source = InvokeFindTargetEntities(
                source,
                target,
                relationship,
                relationship.Kind == RelationshipKind.OneToOne
                    ? OneToOneMethod
                    : OneToManyMethod,
                assignRelationships);
            return source;
        }

        private IList InvokeFindTargetEntities(
            IList source, 
            IList target, 
            IRelationship relationship, 
            MethodInfo method,
            bool assignRelationships)
        {
            return (IList)
                method.InvokeGeneric(
                        this,
                        new object[]
                        {
                            source,
                            target,
                            relationship,
                            assignRelationships
                        },
                        relationship.Source.Type,
                        relationship.Target.Type);
        }

        public List<TTarget> FindOneToOneMatchesTyped<TSource, TTarget>(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship,
            bool assignRelationships
        )
        {
            var matches = new List<TTarget>();
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
                        matches.Add(targetEntity);
                        if (assignRelationships)
                        {
                            sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                            targetEntity.SetPropertyValue(targetProperty, sourceEntity);
                        }
                        break;
                    }
                }
            }
            return matches;
        }

        public List<TTarget> FindOneToManyMatchesTyped<TSource, TTarget>(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship,
            bool assignRelationships
            )
            where TSource : class
            where TTarget : class
        {
            var matches = new List<TTarget>();
            var sourceDictionary = GroupByRelationship(
                source,
                relationship.Source);

            //var action =
            //    assignRelationships
            //        ? (targetEntity =>
            //        {
            //            targetEntity.SetPropertyValue(
            //                targetProperty,
            //                new RelatedList<TTarget, TSource>(targetEntity, targetProperty.Name));
            //        })
            //        : (Action<TTarget>) null;
            var targetDictionary = GroupByRelationshipKey(
                target,
                relationship.Target
                );

            foreach (var sourceEntry in sourceDictionary)
            {
                if (targetDictionary.ContainsKey(sourceEntry.Key))
                {
                    var targetEntity = targetDictionary[sourceEntry.Key];
                    matches.Add(targetEntity.Item);
                    if (assignRelationships)
                    {
                        for (var index = 0; index < sourceEntry.Value.Count; index++)
                        {
                            var sourceEntity = sourceEntry.Value[index];
                            sourceEntity.SetPropertyValue(
                                relationship.Source.Property,
                                targetEntity.Item
                            );
                            var list = (List<TSource>)targetEntity.Lists[relationship.Target.Property.Name];
                            list.Add((TSource)sourceEntity);
                        }
                    }
                }
            }

            if (assignRelationships)
            {
                var targetCountProperty = relationship.Target.Configuration.FindProperty(
                    $"{relationship.Target.Property.Name}Count");
                if (targetCountProperty != null && targetCountProperty.Kind == PropertyKind.Count)
                {
                    foreach (var targetEntity in targetDictionary)
                    {
                        if (targetCountProperty.PropertyInfo.PropertyType == typeof(long))
                        {
                            targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                                (long)targetEntity.Value.Lists[relationship.Target.Property.Name].Count);
                        }
                        else
                        {
                            targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                                targetEntity.Value.Lists[relationship.Target.Property.Name].Count);
                        }
                    }
                }
            }
            return matches;
        }

        private class EntityRelationships<T>
        {
            public T Item { get; }
            public Dictionary<string, IList> Lists { get; }
            public Dictionary<string, object> Entities { get; }

            public EntityRelationships(T item)
            {
                Item = item;
                Lists = new Dictionary<string, IList>();
                Entities = new Dictionary<string, object>();
            }
        }

        private Dictionary<IList, Dictionary<IRelationshipDetail, IDictionary>> ToDictionaryWithListLookup { get; }
        private Dictionary<string, EntityRelationships<T>> GroupByRelationshipKey<T>(
            List<T> dataSet,
            IRelationshipDetail relationship
            //Action<T> action = null
            )
        {
            Dictionary<IRelationshipDetail, IDictionary> root;
            if (!ToDictionaryWithListLookup.ContainsKey(dataSet))
            {
                root = new Dictionary<IRelationshipDetail, IDictionary>();
                ToDictionaryWithListLookup.Add(dataSet, root);
            }
            else
            {
                root = ToDictionaryWithListLookup[dataSet];
            }

            if (!root.ContainsKey(relationship))
            {
                var properties = relationship.Constraints();
                var result = new Dictionary<string, EntityRelationships<T>>();
                var sourceDictionary = new Dictionary<string, EntityRelationships<T>>();
                for (var i = 0; i < dataSet.Count; i++)
                {
                    var entity = dataSet[i];
                    var compositeKey = new CompositeKey();
                    foreach (var property in properties)
                    {
                        compositeKey.Keys.Add(new KeyValue(property.Name,
                            entity.GetPropertyValue(property),
                            property.ElementType));
                    }

                    var itemAndList = new EntityRelationships<T>(entity);
                    sourceDictionary.Add(compositeKey.AsKeyString(false),
                        itemAndList);

                    if (relationship.IsCollection)
                    {
                        itemAndList.Lists.Add(relationship.Property.Name,
                            entity.GetPropertyValueAs<IList>(
                                relationship.Property));
                    }
                    else
                    {
                        itemAndList.Entities.Add(relationship.Property.Name,
                            entity.GetPropertyValue(
                                relationship.Property));
                    }
                    result.Add(compositeKey.AsKeyString(false), itemAndList);
                }
                root.Add(relationship, result);
            }
            return (Dictionary<string, EntityRelationships<T>>)root[relationship];
        }

        private Dictionary<IList, Dictionary<IRelationshipDetail, Dictionary<string, IList>>> GroupByRelationshipLookup { get; }
        private Dictionary<string, IList> GroupByRelationship<T>(
            List<T> dataSet,
            IRelationshipDetail relationship)
        {
            Dictionary<IRelationshipDetail, Dictionary<string, IList>> inner;
            if (!GroupByRelationshipLookup.ContainsKey(dataSet))
            {
                inner = new Dictionary<IRelationshipDetail, Dictionary<string, IList>>();
                GroupByRelationshipLookup.Add(dataSet, inner);
            }
            else
            {
                inner = GroupByRelationshipLookup[dataSet];
            }

            if (!inner.ContainsKey(relationship))
            {
                var grouping = new Dictionary<string, IList>();
                inner.Add(relationship, grouping);
                var properties =
                    relationship.Constraints();
                for (var i = 0; i < dataSet.Count; i++)
                {
                    var entity = dataSet[i];
                    var compositeKey = new CompositeKey();
                    for (var j = 0; j < properties.Length; j++)
                    {
                        var property = properties[j];
                        compositeKey.Keys.Add(new KeyValue(property.Name,
                            entity.GetPropertyValue(property),
                            property.ElementType));
                    }

                    if (compositeKey.HasDefaultValue())
                    {
                        continue;
                    }

                    var key = compositeKey.AsKeyString(false);
                    if (!grouping.ContainsKey(key))
                    {
                        var list = new List<T>();
                        grouping.Add(key, list);
                    }
                    grouping[key].Add(entity);
                }

                return grouping;
            }

            return inner[relationship];
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