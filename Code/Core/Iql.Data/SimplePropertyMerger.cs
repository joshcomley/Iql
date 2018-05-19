using Iql.Data.Configuration;
using Iql.Data.Configuration.Extensions;

namespace Iql.Data
{
    public class SimplePropertyMerger
    {
        public IEntityConfiguration EntityConfiguration { get; }

        public SimplePropertyMerger(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        public void Merge(
            object entity,
            object mergeWith
            )
        {
            foreach (var property in EntityConfiguration.Properties)
            {
                if (property.Kind.HasFlag(PropertyKind.Count) ||
                   property.Kind.HasFlag(PropertyKind.Key) ||
                   property.Kind.HasFlag(PropertyKind.RelationshipKey) ||
                   property.Kind.HasFlag(PropertyKind.Primitive)
                   )
                {
                    entity.SetPropertyValue(property,
                        mergeWith.GetPropertyValue(property));
                }
            }
        }

        //private static void MergeSimpleProperty(object localEntity, object remoteEntity, string propertyName, Type entityType, IDataContext dataContext)
        //{
        //    dataContext.DataStore.GetTracking().TrackingSet(entityType).ChangeEntity(localEntity, () =>
        //    {
        //        var property = dataContext.EntityConfigurationContext.GetEntityByType(entityType).FindProperty(propertyName);
        //        var localValue = localEntity.GetPropertyValue(propertyName);
        //        var remoteValue = remoteEntity.GetPropertyValue(propertyName);
        //        var isCollection = remoteValue is IEnumerable && !(remoteValue is string);
        //        // Local value or remote value is a primitive value or null, so just reassign
        //        if (!isCollection || localValue == null || remoteValue == null)
        //        {
        //            if (!property.Nullable)
        //            {
        //                if (Platform.Name == "JavaScript")
        //                {
        //                    if (property.Type == typeof(DateTime))
        //                    {
        //                        remoteValue = 0;
        //                    }
        //                    else if (property.ConvertedFromType == "Guid")
        //                    {
        //                        remoteValue = Guid.Empty;
        //                    }
        //                }
        //            }
        //            localEntity.SetPropertyValue(propertyName, remoteValue);
        //        }
        //        else
        //        {
        //            var localCollection = (IList)localValue;
        //            var remoteCollection = (IList)remoteValue;
        //            localCollection.Clear();
        //            foreach (var value in remoteCollection)
        //            {
        //                localCollection.Add(value);
        //            }
        //        }
        //    }, ChangeEntityMode.Silent);
        //}
    }
}