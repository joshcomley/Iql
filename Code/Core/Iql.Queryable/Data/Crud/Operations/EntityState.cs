using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Iql.Queryable.Data.Crud.Operations
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState
    {
        public object Entity { get; }
        public Type EntityType { get; }

        public EntityState(object entity, Type entityType, List<PropertyChange> changedProperties)
        {
            Entity = entity;
            EntityType = entityType;
            ChangedProperties = changedProperties;
        }

        public List<PropertyChange> ChangedProperties { get; }
    }
}