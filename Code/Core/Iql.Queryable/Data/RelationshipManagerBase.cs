using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data
{
    public class RelationshipManagerBase
    {
        public static IRelationshipManager GetRelationshipManager(IRelationship relationship, IDataContext dataContext)
        {
            var getRelationshipManagerMethod =
                typeof(RelationshipManagerBase).GetMethod(nameof(GetRelatioshipManagerGeneric),
                        BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(relationship.Source.Type, relationship.Target.Type);
            return (IRelationshipManager)getRelationshipManagerMethod.Invoke(null,
                new object[] { relationship, dataContext });
        }

        private static RelationshipManager<TSource, TTarget> GetRelatioshipManagerGeneric<TSource, TTarget>(IRelationship relationship, IDataContext dataContext)
        {
            return new RelationshipManager<TSource, TTarget>(relationship, dataContext);
        }

        public static void DeleteRelationships(object entity, Type entityType, IDataContext dataContext)
        {
            foreach (var relationship in dataContext.EntityConfigurationContext.GetEntityByType(entityType)
                .AllRelationships())
            {
                var relationshipManager = GetRelationshipManager(relationship.Relationship, dataContext);
                switch (relationship.Relationship.Type)
                {
                    case RelationshipType.OneToMany:
                        if (relationship.ThisIsTarget)
                        {

                        }
                        else
                        {

                        }
                        break;
                    case RelationshipType.OneToOne:
                        if (relationship.ThisIsTarget)
                        {
                            var referenceValue =
                                entity.GetPropertyValue(relationship.Relationship.Target.Property.PropertyName);
                            if (referenceValue != null)
                            {
                                relationshipManager.SourceTrackingSet.SilentlyChangeEntity(referenceValue, () =>
                                {
                                    referenceValue.SetPropertyValue(
                                        relationship.Relationship.Source.Property.PropertyName,
                                        null);
                                });
                            }
                        }
                        else
                        {
                            var referenceValue =
                                entity.GetPropertyValue(relationship.Relationship.Source.Property.PropertyName);
                            if (referenceValue != null)
                            {
                                var constraints = relationship.Relationship.Target.Constraints();
                                var isNullable = true;
                                foreach (var constraint in constraints)
                                {
                                    var constraintProperty =
                                        relationship.Relationship.Target.Configuration.FindProperty(constraint
                                            .PropertyName);
                                    if (!constraintProperty.Nullable)
                                    {
                                        isNullable = false;
                                        break;
                                    }
                                }
                                if (!isNullable)
                                {
                                    relationshipManager.TargetTrackingSet.SilentlyChangeEntity(entity, () =>
                                    {
                                        entity.SetPropertyValue(
                                            relationship.Relationship.Source.Property.PropertyName,
                                            null);
                                    });
                                    RemoveEntity(referenceValue, dataContext
#if TypeScript
                                        , relationship.Relationship.Source.Type
#endif
                                        );
                                }
                                else
                                {
                                    relationshipManager.TargetTrackingSet.SilentlyChangeEntity(entity, () =>
                                    {
                                        foreach (var constraint in constraints)
                                        {
                                            entity.SetPropertyValue(
                                                constraint.PropertyName,
                                                null);
                                        }
                                    });
                                }
                            }
                        }
                        break;
                }
            }
        }

        private static void RemoveEntity(object referenceValue, IDataContext dataContext
#if TypeScript
            , Type entityType
#endif
            )
        {
            dataContext.DeleteEntity(referenceValue
#if TypeScript
                                        , entityType
#endif
            );
            // We don't want to actually perform this delete
            // as we trust the backing data provider to correcly
            // cascade the delete itself
            dataContext.DataStore.Queue.RemoveAll(r =>
                r.Type == QueuedOperationType.Delete &&
                (r.Operation as IEntityCrudOperationBase).Entity == referenceValue);
        }

        public static void TrackRelationships(object entity, Type entityType, IDataContext dataContext)
        {
#pragma warning disable 4014
            TrackRelationshipsInternal(entity, entityType, dataContext, false);
#pragma warning restore 4014
        }
        public static async Task TrackAndRefreshRelationships(object entity, Type entityType, IDataContext dataContext)
        {
            await TrackRelationshipsInternal(entity, entityType, dataContext, true);
        }
        private static List<object> _refreshQueue = new List<object>();
        private static async Task TrackRelationshipsInternal(object entity, Type entityType, IDataContext dataContext, bool allowRefresh)
        {
            foreach (var relationship in dataContext.EntityConfigurationContext.GetEntityByType(entityType).AllRelationships())
            {
                var relationshipManager = GetRelationshipManager(relationship.Relationship, dataContext);
                switch (relationship.Relationship.Type)
                {
                    case RelationshipType.OneToMany:
                        if (relationship.ThisIsTarget)
                        {
                            // e.g.
                            // We are ClientType
                            // With a list of Clients
                            // And each client has a TypeId
                            // 
                            // Iterate through each entity in the clients list and
                            // set the TypeId appropriately

                            var relatedList = entity.GetPropertyValue(
                                    relationship.Relationship.Target.Property.PropertyName)
                                as IRelatedList;
                            if (relatedList == null)
                            {
                                throw new RelatedListHasNoValueException();
                            }
                            foreach (var item in relatedList)
                            {
                                relationshipManager.SourceTrackingSet.SilentlyChangeEntity(item, () =>
                                {
                                    foreach (var constraint in relationship.Relationship.Constraints)
                                    {
                                        item.SetPropertyValue(constraint.SourceKeyProperty.PropertyName,
                                            entity.GetPropertyValue(constraint.TargetKeyProperty.PropertyName));
                                    }
                                    item.SetPropertyValue(relationship.Relationship.Source.Property.PropertyName,
                                        entity);
                                });
                            }
                        }
                        else
                        {
                            // e.g.
                            // We are Client
                            // With a Type and TypeId
                            // Make sure we exist in the Type's list of Clients
                            // And ensure the key and value match
                        }
                        break;
                    case RelationshipType.OneToOne:
                        if (relationship.ThisIsTarget)
                        {
                            var referenceValue =
                                entity.GetPropertyValue(relationship.Relationship.Target.Property.PropertyName);
                            var keyValue =
                                relationship.ThisEnd.GetCompositeKey(entity);
                            var keyValueInverse =
                                relationship.ThisEnd.GetCompositeKey(entity, true);

                            // If the reference value is set, and the key value is set
                            // make sure the key from the reference value matches the key
                            // from the reference key value
                            var keyIsSet = !keyValueInverse.HasDefaultValue();
                            if (referenceValue != null &&
                                keyIsSet &&
                                !dataContext.EntityPropertiesMatch(referenceValue, keyValueInverse))
                            {
                                throw new InconsistentRelationshipAssignmentException();
                            }
                            if (keyIsSet && referenceValue == null)
                            {
                                relationshipManager.ProcessOneToOneKeyChange(entity);
                            }
                            else if (referenceValue != null)
                            {
                                relationshipManager.ProcessOneToOneReferenceChange(entity);
                            }
                        }
                        else
                        {
                            // Find all target relationships that have the same key value
                            if (!dataContext.IsEntityNew(entity, relationship.Relationship.Target.Type))
                            {
                                var key = relationship.Relationship.Source.GetCompositeKey(entity, true);
                                var ourTarget =
                                    entity.GetPropertyValue(relationship.Relationship.Source.Property.PropertyName);
                                foreach (var target in relationshipManager.TargetTrackingSet.TrackedEntites())
                                {
                                    if (dataContext.EntityPropertiesMatch(target, key))
                                    {
                                        if (ourTarget != null && ourTarget != target)
                                        {
                                            // We need to refresh this entity...
                                            if (allowRefresh && !_refreshQueue.Contains(target))
                                            {
                                                _refreshQueue.Add(target);
                                                await dataContext.RefreshEntity(target, relationship.Relationship.Target.Type);
                                                _refreshQueue.Remove(target);
                                                if (!dataContext.EntityPropertiesMatch(target, key))
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                        relationshipManager.SourceTrackingSet.SilentlyChangeEntity(entity, () =>
                                        {
                                            entity.SetPropertyValue(
                                                relationship.Relationship.Source.Property.PropertyName,
                                                target);
                                        });
                                        relationshipManager.TargetTrackingSet.SilentlyChangeEntity(target, () =>
                                        {
                                            target.SetPropertyValue(relationship.Relationship.Target.Property.PropertyName,
                                                entity);
                                        });
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            // Here we need to fake the 
            //foreach (var relationship in DataContext.EntityConfigurationContext.GetEntityByType(entityType).AllRelationships())
            //{
            //    // We are:
            //    // One-To-Many, on the one side
            //    // - We inspect each child in the collection property
            //    // - If the child's parent key or value match the parent, ensure both key and value are set
            //    // - If the child's parent key and value are empty, set to that of the parent
            //    // - If the child's parent key and value to not match the parent, throw an InconsistentRelationshipException
            //    //
            //    // One-To-Many, on the many side
            //    // - If the entity has no relationship key, do nothing
            //    // - If the entity has a relationship key, then look up the related entity and insert this entity into the 
            //    //   related entity's relationship collection property
            //    // - If the entity has a relationship key and a relationship value, then ensure the relationship key and value's key match,
            //    //   otherwise throw an InconsistentRelationshipException.
            //    // - If the entity has a relationship key and a relationship value, and they are consistent, ensure this entity
            //    //   is in the child entity's relationship collection property, and remove it from any collections for this 
            //    //   relationship that it currently belongs to
            //    //
            //    // One-To-One, on either side
            //    // - If the entity has a relationship key, then look up the related entity and assign this entity into the 
            //    //   related entity's relationship property
            //    if (!relationship.PartnerIsSource)
            //    {
            //        continue;
            //    }
            //    var key = relationship.ThisEnd.GetCompositeKey(entity);
            //    //var inverseKey = relationship.ThisEnd.GetCompositeKey(entity);
            //    if (key.HasDefaultValue())
            //    {
            //        continue;
            //    }
            //    var set = DataContext.DataStore.GetTracking().TrackingSet(relationship.OtherEnd.Type);
            //    if (relationship.Relationship.Type == RelationshipType.OneToMany)
            //    {
            //        if (relationship.ThisEnd.IsCollection)
            //        {
            //            var collection = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName) as IList;
            //            if (collection != null)
            //            {
            //                foreach (var child in collection)
            //                {
            //                    foreach (var keyPart in key.Keys)
            //                    {
            //                        child.SetPropertyValue(keyPart.Name, keyPart.Value);
            //                    }
            //                    child.SetPropertyValue(relationship.OtherEnd.Property.PropertyName, entity);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (var relatedEntity in set.TrackedEntites())
            //            {
            //                if (DataContext.EntityPropertiesMatch(relatedEntity, key))
            //                {
            //                    PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
            //                    PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var ourKey = new CompositeKey();
            //        foreach (var constraint in relationship.Relationship.Constraints)
            //        {
            //            ourKey.Keys.Add(new KeyValue(constraint.SourceKeyProperty.PropertyName,
            //                entity.GetPropertyValue(constraint.TargetKeyProperty.PropertyName),
            //                relationship.Relationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
            //        }
            //        foreach (var relatedEntity in set.TrackedEntites())
            //        {
            //            //var constraints = relationship.Relationship.InverseRelationship.Constraints;
            //            //var invertedKey = new CompositeKey();
            //            //foreach (var constraint in constraints)
            //            //{
            //            //    invertedKey.Keys.Add(new KeyValue(constraint.TargetKeyProperty.PropertyName, 
            //            //        relatedEntity.GetPropertyValue(constraint.SourceKeyProperty.PropertyName),
            //            //        relationship.Relationship.InverseRelationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
            //            //}
            //            if (DataContext.EntityPropertiesMatch(relatedEntity, ourKey))
            //            {
            //                PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
            //                PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
            //            }
            //        }
            //    }
            //}
        }
    }
}