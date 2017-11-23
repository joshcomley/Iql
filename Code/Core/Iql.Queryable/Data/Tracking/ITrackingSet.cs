using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackedRelationship<TOwner, TEntity> : ITrackedRelationship
    {
        public TrackedRelationship(TOwner owner, TEntity entity, IRelationship relationship)
        {
            Owner = owner;
            Entity = entity;
            Relationship = relationship;
        }

        public TOwner Owner { get;}
        public TEntity Entity { get; }
        public IRelationship Relationship { get; }

        object ITrackedRelationship.Owner => Owner;

        object ITrackedRelationship.Entity => Entity;
    }

    public interface ITrackedRelationship
    {
        object Owner { get; }
        object Entity { get;}
        IRelationship Relationship { get;}
    }

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

    public interface ITrackedEntity
    {
        object Entity { get; }
        List<ITrackedRelationship> TrackedRelationships { get; }
    }

    public interface ITrackingSet
    {
        IEnumerable<object> TrackedEntites();
        Type EntityType { get; }
        void Track(object entity);
        void Merge(IList data);
        List<IEntityCrudOperationBase> GetChangesInternal(bool reset = false);
        void Reset();
        object FindClone(object entity);
        ITrackedEntity FindTrackedEntity(object entity);
    }
}