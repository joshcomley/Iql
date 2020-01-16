using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Events;

namespace Iql.Data.Relationships
{
    public interface IRelationshipObserver
    {
        EventEmitter<RelationshipChangedEvent> RelationshipChanged { get; }
        EventEmitter<UntrackedEntityAddedEvent> UntrackedEntityAdded { get; }
        void RunIfNotIgnored(Action action, IProperty property, object entity);
        void ObserveAll(Dictionary<Type, IList> dictionary);
        //void ObserveListTyped<T>(List<T> list) where T : class;
        void ObserveList(IList list, Type entityType);
        void Observe(object entity, Type entityType);
        void Unobserve(object entity, Type entityType);
        bool IsAttachedToAnotherEntity(object entity, Type entityType);
        bool IsDetachedPivot(object entity, Type entityType);
        void DeleteRelationships(object entity, Type type);
        void RestoreRelationships(object entity, Type entityType);
        void Clear();
        void NotifyMarkedForDeletionChange(bool isMarkedForDeletion, IEntityStateBase entityState);
    }
}