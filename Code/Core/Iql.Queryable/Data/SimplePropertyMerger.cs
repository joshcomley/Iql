using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data
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
                switch (property.Kind)
                {
                    case PropertyKind.Count:
                    case PropertyKind.Key:
                    case PropertyKind.RelationshipKey:
                    case PropertyKind.Primitive:
                        entity.SetPropertyValue(property,
                            mergeWith.GetPropertyValue(property));
                        break;
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