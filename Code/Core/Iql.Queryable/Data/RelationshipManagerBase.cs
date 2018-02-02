using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Exceptions;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data
{
//    public class RelationshipManagerBase
//    {
//        public static IRelationshipManager GetRelationshipManager(IRelationship relationship, IDataContext dataContext)
//        {
//            var getRelationshipManagerMethod =
//                typeof(RelationshipManagerBase).GetMethod(nameof(GetRelatioshipManagerGeneric),
//                        BindingFlags.NonPublic | BindingFlags.Static)
//                    .MakeGenericMethod(relationship.Source.Type, relationship.Target.Type);
//            return (IRelationshipManager)getRelationshipManagerMethod.Invoke(null,
//                new object[]
//                {
//                    relationship, dataContext
//#if TypeScript
//                    , relationship.Source.Type, relationship.Target.Type
//#endif
//                });
//        }

//        private static RelationshipManager<TSource, TTarget> GetRelatioshipManagerGeneric<TSource, TTarget>(IRelationship relationship, IDataContext dataContext)
//        {
//            return new RelationshipManager<TSource, TTarget>(relationship, dataContext);
//        }

//        public static void DeleteRelationships(object entity, Type entityType, IDataContext dataContext)
//        {
//            foreach (var relationship in dataContext.EntityConfigurationContext.GetEntityByType(entityType)
//                .AllRelationships())
//            {
//                var relationshipManager = GetRelationshipManager(relationship.Relationship, dataContext);
//                switch (relationship.Relationship.Type)
//                {
//                    case RelationshipType.OneToMany:
//                        if (relationship.ThisIsTarget)
//                        {
//                            // All items in the list must be deleted or nulled

//                            // Not sure we have to do anything here
//                            var referenceList =
//                                entity.GetPropertyValue(relationship.Relationship.Target.Property)
//                                as IRelatedList;
//                            var sourceConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(
//                                relationship.OtherEnd.Type);
//                            var nullable = relationship.Relationship.Constraints.All(c =>
//                                c.SourceKeyProperty.Nullable
//                            );
//                            // The source collection will be modified, so make
//                            // a copy to work with
//                            var copy = referenceList.Cast<object>().ToList();
//                            foreach (var child in copy)
//                            {
//                                if (nullable)
//                                {
//                                    foreach (var constraint in relationship.Relationship.Constraints)
//                                    {
//                                        child.SetPropertyValue(constraint.SourceKeyProperty, null);
//                                    }
//                                }
//                                else
//                                {
//                                    dataContext.CascadeDeleteEntity(child, entity, relationship.Relationship
//#if TypeScript
//                                    , relationship.Relationship.Source.Type
//                                    , relationship.Relationship.Target.Type
//#endif
//                                        );
//                                }
//                            }
//                        }
//                        else
//                        {
//                            var referenceValue =
//                                entity.GetPropertyValue(relationship.Relationship.Source.Property);
//                            if (referenceValue == null)
//                            {
//                                var key = relationship.Relationship.Source.GetCompositeKey(entity, true);
//                                var trackedReferenceValue =
//                                    relationshipManager.TargetTrackingSet.FindTrackedEntityByKey(key);
//                                if (trackedReferenceValue != null)
//                                {
//                                    referenceValue = trackedReferenceValue;
//                                }
//                            }
//                            if (referenceValue != null)
//                            {
//                                var referenceList =
//                                    referenceValue.GetPropertyValue(relationship.Relationship.Target.Property)
//                                        as IRelatedList;
//                                referenceList.Remove(entity);
//                            }
//                        }
//                        break;
//                    case RelationshipType.OneToOne:
//                        if (relationship.ThisIsTarget)
//                        {
//                            var referenceValue =
//                                entity.GetPropertyValue(relationship.Relationship.Target.Property);
//                            if (referenceValue != null)
//                            {
//                                relationshipManager.SourceTrackingSet.SilentlyChangeEntity(referenceValue, () =>
//                                {
//                                    referenceValue.SetPropertyValue(
//                                        relationship.Relationship.Source.Property,
//                                        null);
//                                });
//                            }
//                        }
//                        else
//                        {
//                            var referenceValue =
//                                entity.GetPropertyValue(relationship.Relationship.Source.Property);
//                            if (referenceValue != null)
//                            {
//                                var constraints = relationship.Relationship.Target.Constraints();
//                                var isNullable = true;
//                                foreach (var constraint in constraints)
//                                {
//                                    if (!constraint.Nullable)
//                                    {
//                                        isNullable = false;
//                                        break;
//                                    }
//                                }
//                                if (!isNullable)
//                                {
//                                    // Cascade deletion
//                                    relationshipManager.SourceTrackingSet.SilentlyChangeEntity(entity, () =>
//                                    {
//                                        entity.SetPropertyValue(
//                                            relationship.Relationship.Source.Property,
//                                            null);
//                                    });
//                                    //                                    RemoveEntity(referenceValue, dataContext
//                                    //#if TypeScript
//                                    //                                        , relationship.Relationship.Source.Type
//                                    //#endif
//                                    //                                        );
//                                    dataContext.CascadeDeleteEntity(referenceValue,
//                                        entity,
//                                        relationship.Relationship
//#if TypeScript
//                                        , relationship.Relationship.Target.Type
//                                        , relationship.Relationship.Source.Type
//#endif
//                                        );
//                                    //relationshipManager.TargetTrackingSet.Untrack(referenceValue);
//                                }
//                                else
//                                {
//                                    relationshipManager.TargetTrackingSet.SilentlyChangeEntity(entity, () =>
//                                    {
//                                        foreach (var constraint in constraints)
//                                        {
//                                            entity.SetPropertyValue(
//                                                constraint,
//                                                null);
//                                        }
//                                    });
//                                }
//                            }
//                        }
//                        break;
//                }
//            }
//        }

//        private static void RemoveEntity(object referenceValue, IDataContext dataContext
//#if TypeScript
//            , Type entityType
//#endif
//            )
//        {
//            dataContext.DeleteEntity(referenceValue
//#if TypeScript
//                                        , entityType
//#endif
//            );
//            // We don't want to actually perform this delete
//            // as we trust the backing data provider to correcly
//            // cascade the delete itself
//            //dataContext.DataStore.GetQueue().RemoveAll(r =>
//            //    r.Type == QueuedOperationType.Delete &&
//            //    (r.Operation as IEntityCrudOperationBase).Entity == referenceValue);
//        }

//        public static void TrackRelationships(object entity, Type entityType, IDataContext dataContext)
//        {
//#pragma warning disable 4014
//            TrackRelationshipsInternal(entity, entityType, dataContext, false);
//#pragma warning restore 4014
//        }
//        public static async Task TrackAndRefreshRelationships(object entity, Type entityType, IDataContext dataContext)
//        {
//            await TrackRelationshipsInternal(entity, entityType, dataContext, true);
//        }
//        private static readonly List<object> RefreshQueue = new List<object>();
//        private static async Task TrackRelationshipsInternal(object entity, Type entityType, IDataContext dataContext, bool allowRefresh)
//        {
//            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
//            var trackingSet = dataContext.DataStore.GetTracking().TrackingSetByType(entityType);
//            await trackingSet.ChangeEntityAsync(entity, async () =>
//            {
//                foreach (var relationship in entityConfiguration.AllRelationships())
//                {
//                    var relationshipManager = GetRelationshipManager(relationship.Relationship, dataContext);
//                    switch (relationship.Relationship.Type)
//                    {
//                        case RelationshipType.OneToMany:
//                            if (relationship.ThisIsTarget)
//                            {
//                                // e.g.
//                                // We are ClientType
//                                // With a list of Clients
//                                // And each client has a TypeId
//                                // 
//                                // Iterate through each entity in the clients list and
//                                // set the TypeId appropriately

//                                var relatedList = entity.GetPropertyValue(
//                                        relationship.Relationship.Target.Property)
//                                    as IRelatedList;
//                                if (relatedList == null)
//                                {
//                                    throw new RelatedListHasNoValueException();
//                                }
//                                var items = relatedList.Cast<object>().ToArray();
//                                foreach (var item in items)
//                                {
//                                    relationshipManager.SourceTrackingSet.SilentlyChangeEntity(item, () =>
//                                    {
//                                        foreach (var constraint in relationship.Relationship.Constraints)
//                                        {
//                                            item.SetPropertyValue(constraint.SourceKeyProperty,
//                                                entity.GetPropertyValue(constraint.TargetKeyProperty));
//                                        }
//                                        item.SetPropertyValue(relationship.Relationship.Source.Property,
//                                            entity);
//                                    });
//                                }
//                            }
//                            else
//                            {
//                                // e.g.
//                                // We are Client
//                                // With a Type and TypeId
//                                // Make sure we exist in the Type's list of Clients
//                                // And ensure the key and value match
//                                var referenceValue =
//                                    entity.GetPropertyValue(relationshipManager.Relationship.Source.Property);
//                                var keyValueInverse =
//                                    relationship.ThisEnd.GetCompositeKey(entity, true);
//                                var keyIsSet = !keyValueInverse.HasDefaultValue();
//                                if (referenceValue != null &&
//                                    keyIsSet &&
//                                    !dataContext.EntityPropertiesMatch(referenceValue, keyValueInverse))
//                                {
//                                    // We only end up here if the data has come from a database,
//                                    // a new key has been provided for a relationship and the old
//                                    // expand is still in place and was not refreshed with the latest
//                                    // entity refresh
//                                    relationshipManager.ProcessOneToManyKeyChange(entity);
//                                    //throw new InconsistentRelationshipAssignmentException();
//                                }
//                                if (referenceValue != null)
//                                {
//                                    relationshipManager.ProcessOneToManyReferenceChange(entity);
//                                }
//                                else if (keyIsSet)
//                                {
//                                    relationshipManager.ProcessOneToManyKeyChange(entity);
//                                }
//                                //var reference
//                            }
//                            break;
//                        case RelationshipType.OneToOne:
//                            if (relationship.ThisIsTarget)
//                            {
//                                var referenceValue =
//                                    entity.GetPropertyValue(relationship.Relationship.Target.Property);
//                                var keyValue =
//                                    relationship.ThisEnd.GetCompositeKey(entity);
//                                var keyValueInverse =
//                                    relationship.ThisEnd.GetCompositeKey(entity, true);

//                                // If the reference value is set, and the key value is set
//                                // make sure the key from the reference value matches the key
//                                // from the reference key value
//                                var keyIsSet = !keyValueInverse.HasDefaultValue();
//                                if (referenceValue != null &&
//                                    keyIsSet &&
//                                    !dataContext.EntityPropertiesMatch(referenceValue, keyValueInverse))
//                                {
//                                    int a = 0;
//                                    //throw new InconsistentRelationshipAssignmentException();
//                                }
//                                if (keyIsSet)
//                                {
//                                    relationshipManager.ProcessOneToOneKeyChange(entity);
//                                }
//                                else if (referenceValue != null)
//                                {
//                                    relationshipManager.ProcessOneToOneReferenceChange(entity);
//                                }
//                            }
//                            else
//                            {
//                                // Find all target relationships that have the same key value
//                                var isEntityNew = dataContext.IsEntityNew(entity, relationship.Relationship.Source.Type);
//                                if (isEntityNew != null && isEntityNew.Value == false)
//                                {
//                                    var key = relationship.Relationship.Source.GetCompositeKey(entity, true);
//                                    var ourTarget =
//                                        entity.GetPropertyValue(relationship.Relationship.Source.Property);
//                                    foreach (var target in relationshipManager.TargetTrackingSet.TrackedEntites())
//                                    {
//                                        if (dataContext.EntityPropertiesMatch(target, key))
//                                        {
//                                            if (ourTarget != null && ourTarget != target)
//                                            {
//                                                // We need to refresh this entity...
//                                                if (allowRefresh && !RefreshQueue.Contains(target))
//                                                {
//                                                    RefreshQueue.Add(target);
//                                                    await dataContext.RefreshEntity(target
//#if TypeScript
//, relationship.Relationship.Target.Type
//#endif
//                                                        );
//                                                    RefreshQueue.Remove(target);
//                                                    if (!dataContext.EntityPropertiesMatch(target, key))
//                                                    {
//                                                        continue;
//                                                    }
//                                                }
//                                            }
//                                            relationshipManager.SourceTrackingSet.SilentlyChangeEntity(entity, () =>
//                                            {
//                                                entity.SetPropertyValue(
//                                                    relationship.Relationship.Source.Property,
//                                                    target);
//                                            });
//                                            relationshipManager.TargetTrackingSet.SilentlyChangeEntity(target, () =>
//                                            {
//                                                target.SetPropertyValue(relationship.Relationship.Target.Property,
//                                                    entity);
//                                            });
//                                        }
//                                    }
//                                }
//                            }
//                            break;
//                    }
//                }
//            },
//            ChangeEntityMode.Silent,
//            allowRefresh);
//            // Here we need to fake the 
//            //foreach (var relationship in DataContext.EntityConfigurationContext.GetEntityByType(entityType).AllRelationships())
//            //{
//            //    // We are:
//            //    // One-To-Many, on the one side
//            //    // - We inspect each child in the collection property
//            //    // - If the child's parent key or value match the parent, ensure both key and value are set
//            //    // - If the child's parent key and value are empty, set to that of the parent
//            //    // - If the child's parent key and value to not match the parent, throw an InconsistentRelationshipException
//            //    //
//            //    // One-To-Many, on the many side
//            //    // - If the entity has no relationship key, do nothing
//            //    // - If the entity has a relationship key, then look up the related entity and insert this entity into the 
//            //    //   related entity's relationship collection property
//            //    // - If the entity has a relationship key and a relationship value, then ensure the relationship key and value's key match,
//            //    //   otherwise throw an InconsistentRelationshipException.
//            //    // - If the entity has a relationship key and a relationship value, and they are consistent, ensure this entity
//            //    //   is in the child entity's relationship collection property, and remove it from any collections for this 
//            //    //   relationship that it currently belongs to
//            //    //
//            //    // One-To-One, on either side
//            //    // - If the entity has a relationship key, then look up the related entity and assign this entity into the 
//            //    //   related entity's relationship property
//            //    if (!relationship.PartnerIsSource)
//            //    {
//            //        continue;
//            //    }
//            //    var key = relationship.ThisEnd.GetCompositeKey(entity);
//            //    //var inverseKey = relationship.ThisEnd.GetCompositeKey(entity);
//            //    if (key.HasDefaultValue())
//            //    {
//            //        continue;
//            //    }
//            //    var set = DataContext.DataStore.GetTracking().TrackingSet(relationship.OtherEnd.Type);
//            //    if (relationship.Relationship.Type == RelationshipType.OneToMany)
//            //    {
//            //        if (relationship.ThisEnd.IsCollection)
//            //        {
//            //            var collection = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName) as IList;
//            //            if (collection != null)
//            //            {
//            //                foreach (var child in collection)
//            //                {
//            //                    foreach (var keyPart in key.Keys)
//            //                    {
//            //                        child.SetPropertyValue(keyPart.Name, keyPart.Value);
//            //                    }
//            //                    child.SetPropertyValue(relationship.OtherEnd.Property.PropertyName, entity);
//            //                }
//            //            }
//            //        }
//            //        else
//            //        {
//            //            foreach (var relatedEntity in set.TrackedEntites())
//            //            {
//            //                if (DataContext.EntityPropertiesMatch(relatedEntity, key))
//            //                {
//            //                    PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
//            //                    PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
//            //                }
//            //            }
//            //        }
//            //    }
//            //    else
//            //    {
//            //        var ourKey = new CompositeKey();
//            //        foreach (var constraint in relationship.Relationship.Constraints)
//            //        {
//            //            ourKey.Keys.Add(new KeyValue(constraint.SourceKeyProperty.PropertyName,
//            //                entity.GetPropertyValue(constraint.TargetKeyProperty.PropertyName),
//            //                relationship.Relationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
//            //        }
//            //        foreach (var relatedEntity in set.TrackedEntites())
//            //        {
//            //            //var constraints = relationship.Relationship.InverseRelationship.Constraints;
//            //            //var invertedKey = new CompositeKey();
//            //            //foreach (var constraint in constraints)
//            //            //{
//            //            //    invertedKey.Keys.Add(new KeyValue(constraint.TargetKeyProperty.PropertyName, 
//            //            //        relatedEntity.GetPropertyValue(constraint.SourceKeyProperty.PropertyName),
//            //            //        relationship.Relationship.InverseRelationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
//            //            //}
//            //            if (DataContext.EntityPropertiesMatch(relatedEntity, ourKey))
//            //            {
//            //                PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
//            //                PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
//            //            }
//            //        }
//            //    }
//            //}
//        }
//    }
}