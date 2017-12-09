using System;

namespace Iql.Queryable.Data.Crud.State
{
    public class EntityReference
    {
        public object Entity { get; set; }
        public Type EntityType { get; set; }

        public EntityReference(object entity, Type entityType)
        {
            Entity = entity;
            EntityType = entityType;
        }
    }
}