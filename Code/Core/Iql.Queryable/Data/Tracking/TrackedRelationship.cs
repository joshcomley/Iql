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
}