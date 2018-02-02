using System.Collections.Generic;
using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackedRelationship : ITrackedRelationship
    {
        public TrackedRelationship(IRelationship relationship)
        {
            Relationship = relationship;
            Mappings = new Dictionary<IEntityStateBase, IEntityStateBase>();
        }

        public Dictionary<IEntityStateBase, IEntityStateBase> Mappings { get; set; }
        public IRelationship Relationship { get; }
    }
}