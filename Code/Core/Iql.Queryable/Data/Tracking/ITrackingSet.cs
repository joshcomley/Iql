using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackingSet
    {
        void Track(object entity);
        void Merge(IList data);
        List<IEntityCrudOperationBase> GetChanges();
        void Reset();
    }
}