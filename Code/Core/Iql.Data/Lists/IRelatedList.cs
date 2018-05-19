using System;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Events;
using Iql.Data.Events;

namespace Iql.Data.Lists
{
    public interface IRelatedList : IObservableList
    {
        string PropertyName { get; }
        //IEnumerable<IRelatedListChange> GetChanges();
        //void AddChange(IRelatedListChange change);
        //void RemoveChange(IRelatedListChange change);
        object Add(object item);
        IEventSubscriber<IRelatedListChangeEvent> RelatedListChange { get; }
        object Owner { get; }
        Type OwnerType { get; }
        Type TargetType { get; }
        void AssignRelationshipByKey(CompositeKey item);
        void RemoveRelationshipByKey(CompositeKey item);
    }
}