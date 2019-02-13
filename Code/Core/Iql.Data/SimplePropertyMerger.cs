using System.Linq;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;

namespace Iql.Data
{
    public class SimplePropertyMerger
    {
        public SimplePropertyMerger(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        public IEntityConfiguration EntityConfiguration { get; }

        public void MergeAllProperties(
            object entity,
            object mergeWith,
            IProperty[] properties = null
        )
        {
            properties = properties ?? EntityConfiguration.Properties.ToArray();
            foreach (var property in properties) MergeProperty(entity, mergeWith, property);
        }

        private static void MergeProperty(object entity, object mergeWith, IProperty property)
        {
            if (property.Kind.HasFlag(PropertyKind.Count) ||
                property.Kind.HasFlag(PropertyKind.Key) ||
                property.Kind.HasFlag(PropertyKind.RelationshipKey) ||
                property.Kind.HasFlag(PropertyKind.Primitive))
            {
                entity.SetPropertyValue(property,
                    mergeWith.GetPropertyValue(property));
            }
        }

        public void MergeUnchangedProperties(
            IEntityStateBase entityState,
            object mergeWith
        )
        {
            foreach (var property in entityState.EntityConfiguration.Properties)
            {
                var propertyState = entityState.PropertyStates.SingleOrDefault(_ => _.Property == property);
                if (propertyState == null || !propertyState.HasChanged)
                {
                    MergeProperty(entityState.Entity, mergeWith, property);
                }

                if (propertyState != null && propertyState.HasChanged)
                {
                    propertyState.RemoteValue = mergeWith.GetPropertyValue(property);
                }
            }
        }
    }
}