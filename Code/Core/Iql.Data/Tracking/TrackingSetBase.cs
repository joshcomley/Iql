using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract IEntityStateBase AttachEntityInternal(object entity, bool isLocal);
    }
}