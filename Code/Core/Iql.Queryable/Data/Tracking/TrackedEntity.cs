using System.Collections.Generic;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackedEntity<T> : ITrackedEntity
    {
        public TrackedEntity(T entity, List<ITrackedRelationship> trackedRelationships)
        {
            Entity = entity;
            TrackedRelationships = trackedRelationships;
        }

        public T Entity { get; set; }
        public List<ITrackedRelationship> TrackedRelationships { get; } = new List<ITrackedRelationship>();

        object ITrackedEntity.Entity => Entity;
    }
}