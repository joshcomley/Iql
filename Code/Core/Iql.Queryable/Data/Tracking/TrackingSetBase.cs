using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.State;

namespace Iql.Queryable.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract void ChangeEntity(object entity, Action action, ChangeEntityMode silently);
        internal abstract Task ChangeEntityAsync(object entity, Func<Task> action, ChangeEntityMode silently, bool allowAsync = true);
        internal abstract IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true);
    }
}