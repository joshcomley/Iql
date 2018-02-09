using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.State;

namespace Iql.Queryable.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true);
        //internal abstract void UpdateRelationships(List<IEntityStateBase> states);
    }
}