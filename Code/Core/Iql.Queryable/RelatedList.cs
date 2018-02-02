using System;
using System.Collections.Generic;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedList<TSource, TTarget> : List<TTarget>, IRelatedList
        where TTarget : class
        where TSource : class
    {
        //private readonly IList<RelatedListChange<TSource, T>> _changes = new List<RelatedListChange<TSource, T>>();
        //public RelatedList()
        //{

        //}
        public RelatedList(TSource owner = null, string property = null, IEnumerable<TTarget> source = null)
        {
            Owner = owner;
            Property = property;
            this.Initialize(source);
        }

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> Changed { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> Changing { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

        public TSource Owner { get; }
        public Type OwnerType => typeof(TSource);
        public Type TargetType => typeof(TTarget);
        public string Property { get; }

        //void IRelatedList.AddChange(IRelatedListChange change)
        //{
        //    AddChange((RelatedListChange<TSource, T>) change);
        //}

        //void IRelatedList.RemoveChange(IRelatedListChange change)
        //{
        //    RemoveChange((RelatedListChange<TSource, T>) change);
        //}

        //IEnumerable<IRelatedListChange> IRelatedList.GetChanges()
        //{
        //    return GetChanges();
        //}

        object IRelatedList.Owner => Owner;

        IEventSubscriber<IRelatedListChangedEvent> IRelatedList.Changed => Changed;
        IEventSubscriber<IRelatedListChangedEvent> IRelatedList.Changing => Changing;

        void IRelatedList.AssignRelationship(object item)
        {
            AssignRelationship((TTarget)item);
        }

        void IRelatedList.RemoveRelationship(object item)
        {
            RemoveRelationship((TTarget)item);
        }

        public void AssignRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Assign);
        }

        public void RemoveRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Remove);
        }

        //public IList<RelatedListChange<TSource, T>> GetChanges()
        //{
        //    return _changes;
        //}

        //public void AddChange(RelatedListChange<TSource, T> change)
        //{
        //    var existingChange = _changes.FindMatchingChange(change.ItemKey);
        //    if (existingChange != null)
        //    {
        //        if (change.Kind != existingChange.Kind)
        //        {
        //            _changes.Remove(existingChange);
        //        }
        //    }
        //    else
        //    {
        //        _changes.Add(change);
        //    }
        //}

        //public void RemoveChange(RelatedListChange<TSource, T> change)
        //{
        //    var existingChange = _changes.FindMatchingChange(change.ItemKey);
        //    if (existingChange != null)
        //    {
        //        _changes.Remove(existingChange);
        //    }
        //}

        public void AssignRelationship(TTarget item)
        {
            Emit(item, null, RelatedListChangeKind.Assign);
        }

        private void Emit(TTarget item, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            Changed.Emit(() => new RelatedListChangeEvent<TSource, TTarget>(Owner, item, itemKey, kind, this));
        }

        public void RemoveRelationship(TTarget item)
        {
            Emit(item, null, RelatedListChangeKind.Remove);
        }
    }
}