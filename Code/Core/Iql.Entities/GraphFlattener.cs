using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;

namespace Iql.Entities
{
    public static class GraphFlattener
    {
        public static Dictionary<Type, IList> FlattenObjectGraphInternal(
            IEntityConfigurationBuilder builder,
            object entity,
            Type entityType)
        {
            var result = new Dictionary<Type, IList>();
            FlattenObjectGraphRecursive(
                builder,
                entity,
                entityType,
                new Dictionary<Type, Dictionary<string, object>>(),
                result,
                new Dictionary<object, object>(),
                false,
                null
            );
            return result;
        }

        public static Dictionary<Type, IList> FlattenObjectGraphsInternal(
            IEntityConfigurationBuilder builder,
            Type entityType, 
            IEnumerable entities)
        {
            var dictionary = new Dictionary<Type, Dictionary<string, object>>();
            var recursionLookup = new Dictionary<object, object>();
            var result = new Dictionary<Type, IList>();
            foreach (var entity in entities)
            {
                FlattenObjectGraphRecursive(
                    builder,
                    entity,
                    entityType,
                    dictionary,
                    result,
                    recursionLookup,
                    false,
                    null
                );
            }
            return result;
        }
        public static Dictionary<Type, IList> FlattenDependencyGraphInternal(
            IEntityConfigurationBuilder builder,
            object entity,
            Type entityType,
            Func<object, IRelationshipDetail, object> resolveRelationshipValue)
        {
            var result = new Dictionary<Type, IList>();
            FlattenObjectGraphRecursive(
                builder,
                entity,
                entityType,
                new Dictionary<Type, Dictionary<string, object>>(),
                result,
                new Dictionary<object, object>(),
                true,
                resolveRelationshipValue
            );
            return result;
        }

        public static Dictionary<Type, IList> FlattenDependencyGraphsInternal(
            IEntityConfigurationBuilder builder,
            Type entityType,
            IEnumerable entities,
            Func<object, IRelationshipDetail, object> resolveRelationshipValue)
        {
            var dictionary = new Dictionary<Type, Dictionary<string, object>>();
            var recursionLookup = new Dictionary<object, object>();
            var result = new Dictionary<Type, IList>();
            foreach (var entity in entities)
            {
                FlattenObjectGraphRecursive(
                    builder,
                    entity,
                    entityType,
                    dictionary,
                    result,
                    recursionLookup,
                    true,
                    resolveRelationshipValue
                );
            }
            return result;
        }

        private static void FlattenObjectGraphRecursive(
            IEntityConfigurationBuilder builder,
            object objectGraphRoot,
            Type entityType,
            Dictionary<Type, Dictionary<string, object>> dictionary,
            Dictionary<Type, IList> result,
            Dictionary<object, object> recursionLookup,
            bool dependenciesOnly,
            Func<object, IRelationshipDetail, object> resolveRelationshipValue
            )
        {
            resolveRelationshipValue = resolveRelationshipValue ??
                                       ((o, detail) => detail.Property.GetValue(o)); 
            if (!dictionary.ContainsKey(entityType))
            {
                dictionary.Add(entityType, new Dictionary<string, object>());
                result.Add(entityType, ListOfType(entityType));
            }

            var typeGroup = dictionary[entityType];
            if (recursionLookup.ContainsKey(objectGraphRoot))
            {
                // Prevent infinite recursion
                return;
            }
            recursionLookup.Add(objectGraphRoot, objectGraphRoot);
            var graphEntityConfiguration = builder.GetEntityByType(entityType);
            var compositeKey = graphEntityConfiguration.GetCompositeKey(objectGraphRoot);
            var keyString = compositeKey.AsLegacyKeyString();
            if (compositeKey.HasDefaultValue())
            {
                keyString += Guid.NewGuid().ToString();
            }

            if (typeGroup.ContainsKey(keyString))
            {
                return;
            }

            result[entityType].Add(objectGraphRoot);
            typeGroup.Add(
                keyString,
                objectGraphRoot);

            for (var i = 0; i < graphEntityConfiguration.AllRelationships.Length; i++)
            {
                var relationship = graphEntityConfiguration.AllRelationships[i];
                if (relationship.ThisIsTarget && dependenciesOnly)
                {
                    continue;
                }

                var relationshipValue = resolveRelationshipValue(objectGraphRoot, relationship.ThisEnd);
                var childType = relationship.OtherEnd.Type;
                if (relationshipValue != null)
                {
                    var isArray = relationshipValue is IEnumerable && !(relationshipValue is string);
                    if (isArray)
                    {
                        var list = (IList) relationshipValue;
                        foreach (var item in list)
                        {
                            FlattenObjectGraphRecursive(builder, item, childType, dictionary, result, recursionLookup,
                                dependenciesOnly, resolveRelationshipValue);
                        }
                    }
                    else
                    {
                        FlattenObjectGraphRecursive(builder, relationshipValue, childType, dictionary, result,
                            recursionLookup, dependenciesOnly, resolveRelationshipValue);
                    }
                }
            }
        }

        private static IList ListOfType(Type entityType)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(entityType));
        }
    }
}