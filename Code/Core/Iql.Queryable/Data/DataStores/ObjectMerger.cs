using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Tracking;
using TypeSharp.Extensions;

namespace Iql.Queryable.Data.DataStores
{
    public class ObjectMerger
    {
        public static void Merge(IDataContext dataContext, TrackingSetCollection trackingSetCollection,
            object newEntity, Type entityType)
        {
            MergeInternal(new List<object>(), dataContext, trackingSetCollection, newEntity, entityType);
        }

        private static void MergeInternal(
            List<object> alreadyMerged,
            IDataContext dataContext, TrackingSetCollection trackingSetCollection, object newEntity, Type entityType)
        {
            // Prevent infinite recursion
            if (!alreadyMerged.Contains(newEntity))
            {
                alreadyMerged.Add(newEntity);
            }
            else
            {
                return;
            }
            var trackedEntity = trackingSetCollection.TrackingSet(newEntity.GetType()).FindTrackedEntity(newEntity)?.Entity;
            if (trackedEntity == newEntity || trackedEntity == null)
            {
                return;
            }
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(trackedEntity.GetType());
            // Keep track of properties already merged because first we will merge the relationships
            // of whose properties will also be in the property collection
            var propertiesMerged = new List<string>();
            foreach (var relationship in entityConfiguration.Relationships)
            {
                var isSource = relationship.Source.Configuration == entityConfiguration;
                var sourceRelationship = isSource ? relationship.Source : relationship.Target;
                var targetRelationship = isSource ? relationship.Target : relationship.Source;
                var propertyName = sourceRelationship.Property.PropertyName;
                propertiesMerged.Add(propertyName);
                var trackedRelationshipValue = trackedEntity.GetPropertyValue(propertyName);
                var newRelationshipValue = newEntity.GetPropertyValue(propertyName);
                var nonNull = trackedRelationshipValue ?? newRelationshipValue;

                // The new entity has a null value for this relationship, so set the relationship value accordingly
                // Note: we don't need to set the constraints as they will be merged later
                if (newRelationshipValue == null)
                {
                    //ObjectNullifier.ClearProperty(trackedEntity, propertyName);
                }
                // A relationship value exists on the new entity
                else
                {
                    var isCollection = newRelationshipValue is IEnumerable && !(newRelationshipValue is string);
                    // //Single entity, no current value locally so just assign
                    // As we have nothing being tracked yet on this entity, we can just set it accordingly
                    if (trackedRelationshipValue == null && !isCollection)
                    {
                        trackedEntity.SetPropertyValue(propertyName, MergeWithExistingTrackedEntity(alreadyMerged, dataContext, trackingSetCollection, targetRelationship.Type, newRelationshipValue));
                    }
                    // Both new and existing relationship values exist, so we need to merge
                    else
                    {
                        // We have a collection
                        if (isCollection)
                        {
                            var localList = (IList)trackedRelationshipValue;
                            var remoteList = (IList)newRelationshipValue;
                            // We have a null local list so just use the new list and merge all items
                            if (localList == null)
                            {
                                trackedEntity.SetPropertyValue(propertyName, remoteList);
                                foreach (var item in remoteList)
                                {
                                    MergeWithExistingTrackedEntity(alreadyMerged, dataContext, trackingSetCollection, targetRelationship.Type, item);
                                }
                            }
                            // There is nothing in the existing collection, therefore nothing to merge
                            // so we can safely copy all collection values across from the new entity
                            else if (localList.Count == 0)
                            {
                                // There is nothing in the local list to merge so we can safely
                                // add all remote items
                                foreach (var item in remoteList)
                                {
                                    localList.Add(MergeWithExistingTrackedEntity(alreadyMerged, dataContext, trackingSetCollection, targetRelationship.Type, item));
                                }
                            }
                            else
                            {
                                // We care going to be modifying the collection, so make a copy to iterate safely through
                                var localListCopy = new List<object>();
                                foreach (var item in localList)
                                {
                                    localListCopy.Add(item);
                                }
                                localList.Clear();
                                foreach (var newItem in remoteList)
                                {
                                    object existingItem = null;
                                    foreach (var localItem in localListCopy)
                                    {
                                        var isMatch = true;
                                        foreach (var keyProperty in targetRelationship.Configuration.Key.Properties)
                                        {
                                            // As we are in a relationship, even if the source ID is not set it is inferred by its placement
                                            // in the source's collection
                                            if (relationship.Constraints.Any(c => c.SourceKeyProperty.PropertyName == keyProperty.PropertyName))
                                            {
                                                continue;
                                            }
                                            if (!Equals(newItem.GetPropertyValue(keyProperty.PropertyName),
                                                localItem.GetPropertyValue(keyProperty.PropertyName)))
                                            {
                                                isMatch = false;
                                                break;
                                            }
                                        }
                                        if (isMatch)
                                        {
                                            existingItem = localItem;
                                            break;
                                        }
                                    }
                                    if (existingItem != null)
                                    {
                                        // Although we have this entity already, ensure the entity is definitely tracked
                                        trackingSetCollection.Track(existingItem, targetRelationship.Type);
                                    }
                                    else
                                    {
                                        existingItem = trackingSetCollection.FindEntity(newItem)?.Entity ?? newItem;
                                    }
                                    // We've found no matching local item in the list, so add it
                                    localList.Add(MergeWithExistingTrackedEntity(alreadyMerged, dataContext, trackingSetCollection, targetRelationship.Type, newItem, existingItem));
                                }
                            }
                        }
                        else
                        {
                            // Although we have this entity already, ensure the entity is definitely tracked
                            trackingSetCollection.Track(trackedRelationshipValue, targetRelationship.Type);
                            MergeWithExistingTrackedEntity(alreadyMerged, dataContext, trackingSetCollection, targetRelationship.Type, newRelationshipValue, trackedRelationshipValue);
                        }
                    }
                }
            }
            //var entityDefinition = dataContext.EntityConfigurationContext.GetEntityByType(localEntity.GetType());
            foreach (var property in entityConfiguration.Properties)
            {
                if (propertiesMerged.Contains(property.Name))
                {
                    continue;
                }
                MergeSimpleProperty(trackedEntity, newEntity, property.Name, entityType, dataContext);
            }
        }

        private static object MergeWithExistingTrackedEntity(
            List<object> alreadyMerged,
            IDataContext dataContext,
            TrackingSetCollection trackingSetCollection, Type entityType, object newEntity, object trackedEntity = null)
        {
            if (trackedEntity == null)
            {
                trackedEntity = trackingSetCollection.FindEntity(newEntity)?.Entity;
            }
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            if (trackedEntity != null)
            {
                foreach (var keyProperty in entityConfiguration.Key.Properties)
                {
                    trackedEntity.SetPropertyValue(keyProperty.PropertyName,
                        newEntity.GetPropertyValue(keyProperty.PropertyName));
                }
                MergeInternal(alreadyMerged, dataContext, trackingSetCollection, newEntity, entityType);
                return trackedEntity;
            }
            trackingSetCollection.Track(newEntity, entityType);
            return newEntity;
        }

        private static void MergeSimpleProperty(object localEntity, object remoteEntity, string propertyName, Type entityType, IDataContext dataContext)
        {
            var property = dataContext.EntityConfigurationContext.GetEntityByType(entityType).FindProperty(propertyName);
            var localValue = localEntity.GetPropertyValue(propertyName);
            var remoteValue = remoteEntity.GetPropertyValue(propertyName);
            var isCollection = remoteValue is IEnumerable && !(remoteValue is string);
            // Local value or remote value is a primitive value or null, so just reassign
            if (!isCollection || localValue == null || remoteValue == null)
            {
                if (!property.Nullable)
                {
                    if (Platform.Name == "JavaScript")
                    {
                        if (property.Type == typeof(DateTime))
                        {
                            remoteValue = 0;
                        }
                        else if (property.ConvertedFromType == "Guid")
                        {
                            remoteValue = Guid.Empty;
                        }
                    }
                }
                localEntity.SetPropertyValue(propertyName, remoteValue);
            }
            else
            {
                var localCollection = (IList)localValue;
                var remoteCollection = (IList)remoteValue;
                localCollection.Clear();
                foreach (var value in remoteCollection)
                {
                    localCollection.Add(value);
                }
            }
        }
    }
}