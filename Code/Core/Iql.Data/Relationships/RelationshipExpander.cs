using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Iql.Extensions;

namespace Iql.Data.Relationships
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

        public static T MatchRelationships<T>(
            T toReturn,
            IList source,
            IList target,
            IRelationship relationship)
        {
            new RelationshipExpander().FindMatches(
                source,
                target,
                relationship,
                true);
            return toReturn;
        }

        private static MethodInfo OneToOneMethod { get; }
        private static MethodInfo OneToManyMethod { get; }

        private static MethodInfo GetMethod(string methodName)
        {
            return typeof(RelationshipExpander)
                .GetMethod(methodName);
        }

        public RelationshipMatches FindMatches(
            IList source,
            IList target,
            IRelationship relationship,
            bool assignRelationships
        )
        {
            var result = InvokeFindTargetEntities(
                source,
                target,
                relationship,
                relationship.Kind == RelationshipKind.OneToOne
                    ? OneToOneMethod
                    : OneToManyMethod,
                assignRelationships);
            return result;
        }

        private RelationshipMatches InvokeFindTargetEntities(
            IList source,
            IList target,
            IRelationship relationship,
            MethodInfo method,
            bool assignRelationships)
        {
            return (RelationshipMatches)
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

        public RelationshipMatches FindOneToOneMatchesTyped<TSource, TTarget>(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship,
            bool assignRelationships
        )
        {
            var targetMatches = new List<TTarget>();
            var sourceMatches = new List<TSource>();
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
                        sourceMatches.Add(sourceEntity);
                        targetMatches.Add(targetEntity);
                        if (assignRelationships)
                        {
                            sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                            targetEntity.SetPropertyValue(targetProperty, sourceEntity);
                        }
                        break;
                    }
                }
            }
            return new RelationshipMatches(relationship, sourceMatches, targetMatches);
        }

        public RelationshipMatches FindOneToManyMatchesTyped<TSource, TTarget>(
            List<TSource> source,
            List<TTarget> target,
            IRelationship relationship,
            bool assignRelationships
            )
            where TSource : class
            where TTarget : class
        {
            var targetMatches = new List<TTarget>();
            var sourceMatches = new List<TSource>();
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
                    targetMatches.Add(targetEntity.Item);
                    sourceMatches.AddRange((IList<TSource>)sourceEntry.Value);
                    if (assignRelationships)
                    {
                        for (var index = 0; index < sourceEntry.Value.Count; index++)
                        {
                            var sourceEntity = sourceEntry.Value[index];
                            sourceEntity.SetPropertyValue(
                                relationship.Source.Property,
                                targetEntity.Item
                            );
                            var list = (IList<TSource>)targetEntity.Lists[((IMetadata) relationship.Target.Property).Name];
                            list.Add((TSource)sourceEntity);
                        }
                    }
                }
            }

            if (assignRelationships)
            {
                var targetCountProperty = relationship.Target.EntityConfiguration.FindProperty(
                    $"{((IMetadata) relationship.Target.Property).Name}Count");
                if (targetCountProperty != null && targetCountProperty.Kind.HasFlag(IqlPropertyKind.Count))
                {
                    foreach (var targetEntity in targetDictionary)
                    {
#if !TypeScript
                        if (targetCountProperty.PropertyInfo.PropertyType == typeof(long))
                        {
                            targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                                (long)targetEntity.Value.Lists[((IMetadata) relationship.Target.Property).Name].Count);
                        }
                        else
                        {
#endif
                            targetEntity.Value.Item.SetPropertyValue(targetCountProperty,
                                targetEntity.Value.Lists[((IMetadata) relationship.Target.Property).Name].Count);
#if !TypeScript
                        }
#endif
                    }
                }
            }
            return new RelationshipMatches(relationship, sourceMatches, targetMatches);
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
                var properties = relationship.Constraints;
                var result = new Dictionary<string, EntityRelationships<T>>();
                var sourceDictionary = new Dictionary<string, EntityRelationships<T>>();
                for (var i = 0; i < dataSet.Count; i++)
                {
                    var entity = dataSet[i];
                    var compositeKey = new CompositeKey(relationship.EntityConfiguration.TypeName, properties.Length);
                    for (var j = 0; j < properties.Length; j++)
                    {
                        var property = properties[j];
                        compositeKey.Keys[j] = 
                            new KeyValue(
                                ((IMetadata) property).Name,
                                entity.GetPropertyValue(property),
                                property.TypeDefinition);
                    }

                    var itemAndList = new EntityRelationships<T>(entity);
                    sourceDictionary.Add(compositeKey.AsLegacyKeyString(false),
                        itemAndList);

                    if (relationship.IsCollection)
                    {
                        itemAndList.Lists.Add(((IMetadata) relationship.Property).Name,
                            entity.GetPropertyValueAs<IList>(
                                relationship.Property));
                    }
                    else
                    {
                        itemAndList.Entities.Add(((IMetadata) relationship.Property).Name,
                            entity.GetPropertyValue(
                                relationship.Property));
                    }
                    result.Add(compositeKey.AsLegacyKeyString(false), itemAndList);
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
                    relationship.Constraints;
                for (var i = 0; i < dataSet.Count; i++)
                {
                    var entity = dataSet[i];
                    var compositeKey = new CompositeKey(relationship.EntityConfiguration.TypeName, properties.Length);
                    for (var j = 0; j < properties.Length; j++)
                    {
                        var property = properties[j];
                        compositeKey.Keys[j] = new KeyValue(((IMetadata) property).Name,
                            entity.GetPropertyValue(property),
                            property.TypeDefinition);
                    }

                    if (compositeKey.HasDefaultValue())
                    {
                        continue;
                    }

                    var key = compositeKey.AsLegacyKeyString(false);
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
    }
}