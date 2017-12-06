using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackedRelationship<TOwner, TEntity> : ITrackedRelationship
    {
        public TrackedRelationship(TOwner owner, TEntity entity, IRelationship relationship)
        {
            Owner = owner;
            Entity = entity;
            Relationship = relationship;
            OwnerDetail = relationship.Target.Type == typeof(TOwner) ? relationship.Target : relationship.Source;
            EntityDetail = relationship.Target.Type == typeof(TEntity) ? relationship.Target : relationship.Source;
        }

        public TOwner Owner { get;}
        public TEntity Entity { get; }
        public IRelationshipDetail OwnerDetail { get; }
        public IRelationshipDetail EntityDetail { get; }
        public IRelationship Relationship { get; }

        object ITrackedRelationship.Owner => Owner;

        object ITrackedRelationship.Entity => Entity;
    }

    public interface ITrackedRelationship
    {
        object Owner { get; }
        object Entity { get;}
        IRelationshipDetail OwnerDetail { get; }
        IRelationshipDetail EntityDetail { get; }
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
        void Watch(object entity);
        void Unwatch(object entity);
        void SilentlyChangeEntity(object entity, Action action);
        EntityState GetEntityState(object entity);
        void EnsureIntegrity();
        IEnumerable<object> TrackedEntites();
        Type EntityType { get; }
        void Track(object entity);
        void Merge(IList data);
        void MergeEntity(object entity);
        List<IUpdateEntityOperation> GetChangesInternal(List<IQueuedOperation> queue, bool reset = false);
        void Reset();
        object FindClone(object entity);
        ITrackedEntity FindTrackedEntity(object entity);
        ITrackedEntity FindTrackedEntityByKey(CompositeKey key);
        IEnumerable<ITrackedRelationship> FindRelationships(object entity, CompositeKey key);
    }
}