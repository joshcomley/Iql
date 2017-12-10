using System;
using System.Collections;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Tracking;
using TypeSharp.Extensions;

namespace Iql.Queryable.Data.DataStores
{
    public class SimplePropertyMerger
    {
        public static void Merge(IDataContext dataContext, TrackingSetCollection trackingSetCollection,
            object newEntity, Type entityType)
        {
            var configuration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            var trackedEntity = trackingSetCollection.TrackingSet(newEntity.GetType()).FindTrackedEntity(newEntity)?.Entity;
            if (trackedEntity == newEntity || trackedEntity == null)
            {
                return;
            }
            trackingSetCollection.TrackingSet(entityType).ChangeEntity(trackedEntity, () =>
            {
                foreach (var property in configuration.Properties)
                {
                    switch (property.Kind)
                    {
                        case PropertyKind.Count:
                        case PropertyKind.Key:
                        case PropertyKind.RelationshipKey:
                        case PropertyKind.Primitive:
                            trackedEntity.SetPropertyValue(property.Name,
                                newEntity.GetPropertyValue(property.Name));
                            break;
                    }
                }
            }, ChangeEntityMode.Silent);
        }

        private static void MergeSimpleProperty(object localEntity, object remoteEntity, string propertyName, Type entityType, IDataContext dataContext)
        {
            dataContext.DataStore.GetTracking().TrackingSet(entityType).ChangeEntity(localEntity, () =>
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
            }, ChangeEntityMode.Silent);
        }
    }
}