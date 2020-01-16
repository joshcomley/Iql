using System;
using Iql.Data.Relationships;
using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract Func<IEntityStateBase> AttachEntityInternal(object entity, bool isLocal);

        internal abstract void RelationshipChanged(RelationshipChangedEvent relationshipChangedEvent);
    }
}