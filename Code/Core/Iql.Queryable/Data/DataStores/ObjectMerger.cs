using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.Tracking;

namespace Iql.Queryable.Data.DataStores
{
    public class ObjectMerger
    {
        public static void Merge(IDataContext dataContext, TrackingSetCollection trackingSetCollection, object newEntity)
        {
            var trackedEntity = trackingSetCollection.TrackingSet(newEntity.GetType()).FindTrackedEntity(newEntity).Entity;
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
                    trackedEntity.SetPropertyValue(propertyName, null);
                }
                // A relationship value exists on the new entity
                else
                {
                    // //Single entity, no current value locally so just assign
                    // As we have nothing being tracked yet on this entity, we can just set it accordingly
                    if (trackedRelationshipValue == null)
                    {
                        trackedEntity.SetPropertyValue(propertyName, MergeWithExistingTrackedEntity(dataContext, trackingSetCollection, newRelationshipValue));
                    }
                    // Both new and existing relationship values exist, so we need to merge
                    else
                    {
                        var isCollection = newRelationshipValue is IEnumerable && !(newRelationshipValue is string);
                        // We have a collection
                        if (isCollection)
                        {
                            var localList = (IList)trackedRelationshipValue;
                            var remoteList = (IList)newRelationshipValue;
                            // There is nothing in the existing collection, therefore nothing to merge
                            // so we can safely copy all collection values across from the new entity
                            if (localList.Count == 0)
                            {
                                // There is nothing in the local list to merge so we can safely
                                // add all remote items
                                foreach (var item in remoteList)
                                {
                                    localList.Add(MergeWithExistingTrackedEntity(dataContext, trackingSetCollection, item));
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
                                        trackingSetCollection.Track(existingItem);
                                    }
                                    else
                                    {
                                        existingItem = newItem;
                                    }
                                    // We've found no matching local item in the list, so add it
                                    localList.Add(MergeWithExistingTrackedEntity(dataContext, trackingSetCollection, newItem, existingItem));
                                }
                            }
                        }
                        else
                        {
                            // Although we have this entity already, ensure the entity is definitely tracked
                            trackingSetCollection.Track(trackedRelationshipValue);
                            MergeWithExistingTrackedEntity(dataContext, trackingSetCollection, newRelationshipValue, trackedRelationshipValue);
                        }
                    }
                }
            }
            //var entityDefinition = dataContext.EntityConfigurationContext.GetEntityByType(localEntity.GetType());
            foreach (var property in trackedEntity.GetType().GetRuntimeProperties())
            {
                if (propertiesMerged.Contains(property.Name))
                {
                    continue;
                }
                MergeSimpleProperty(trackedEntity, newEntity, property);
            }
            foreach (var property in newEntity.GetType().GetRuntimeProperties())
            {
                if (propertiesMerged.Contains(property.Name))
                {
                    continue;
                }
                MergeSimpleProperty(trackedEntity, newEntity, property);
            }
        }

        private static object MergeWithExistingTrackedEntity(IDataContext dataContext,
            TrackingSetCollection trackingSetCollection, object newEntity, object trackedEntity = null)
        {
            var type = newEntity.GetType();
            if (type.Name == "PersonType")
            {
                int a = 0;
            }
            if (trackedEntity == null)
            {
                trackedEntity = trackingSetCollection.FindEntity(newEntity)?.Entity;
            }
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(type);
            if (trackedEntity != null)
            {
                foreach (var keyProperty in entityConfiguration.Key.Properties)
                {
                    trackedEntity.SetPropertyValue(keyProperty.PropertyName,
                        newEntity.GetPropertyValue(keyProperty.PropertyName));
                }
                Merge(dataContext, trackingSetCollection, newEntity);
                return trackedEntity;
            }
            trackingSetCollection.Track(newEntity);
            return newEntity;
        }

        private static void MergeSimpleProperty(object localEntity, object remoteEntity,
            PropertyInfo property)
        {
            var localValue = property.GetValue(localEntity);
            var remoteValue = property.GetValue(remoteEntity);
            var isCollection = remoteValue is IEnumerable && !(remoteValue is string);
            // Local value or remote value is a primitive value or null, so just reassign
            if (!isCollection || localValue == null || remoteValue == null)
            {
                localEntity.SetPropertyValue(property.Name, remoteValue);
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