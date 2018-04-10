using Iql.Queryable.Data.Tracking.State;

namespace Iql.Queryable.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true, bool onlyMergeWithExisting = false);
    }
}