using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class FlattenedEntity
    {
        public object Entity { get; set; }
        public Type EntityType { get; set; }

        public FlattenedEntity(object entity, Type entityType)
        {
            Entity = entity;
            EntityType = entityType;
        }
    }
}