using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
{
    public interface IRelationshipManager
    {
        ITrackingSet SourceTrackingSet { get; }
        ITrackingSet TargetTrackingSet { get; }
        void ProcessOneToManyCollectionAdd(object entity, object toAdd, CompositeKey toAddKey);
        void ProcessOneToManyCollectionRemove(object entity, object toRemove, CompositeKey toRemoveKey);
        void ProcessOneToManyKeyChange(object entity);
        void ProcessOneToManyReferenceChange(object entity);
        void ProcessOneToOneInverseReferenceChange(object entity);
        void ProcessOneToOneKeyChange(object entity);
        void ProcessOneToOneReferenceChange(object entity);
    }
}