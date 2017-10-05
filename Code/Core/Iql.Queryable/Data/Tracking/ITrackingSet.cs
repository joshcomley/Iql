using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackingSet
    {
        Type EntityType { get; }
        void Track(object entity);
        void TrackWithClone(object entity, object clone);
        void Merge(IList data);
        List<IEntityCrudOperationBase> GetChanges();
        void Reset();
        object FindClone(object entity);
    }
}