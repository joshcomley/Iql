using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IRelatedList : IList
    {
        string PropertyName { get; }
        //IEnumerable<IRelatedListChange> GetChanges();
        //void AddChange(IRelatedListChange change);
        //void RemoveChange(IRelatedListChange change);
        IEventSubscriber<IRelatedListChangedEvent> Changed { get; }
        IEventSubscriber<IRelatedListChangedEvent> Changing { get; }
        object Owner { get; }
        Type OwnerType { get; }
        Type TargetType { get; }
        void AssignRelationshipByKey(CompositeKey item);
        void RemoveRelationshipByKey(CompositeKey item);
    }
}