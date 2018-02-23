using System;
using System.Collections.Generic;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedList<TSource, TTarget> : ObservableList<TTarget>, IRelatedList
        where TTarget : class
        where TSource : class
    {
        public RelatedList(TSource owner = null, string property = null, IEnumerable<TTarget> source = null)
        {
            Owner = owner;
            PropertyName = property;
            this.Initialize(source);
        }

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> Changed { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> Changing { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

        public TSource Owner { get; }
        public Type OwnerType => typeof(TSource);
        public Type TargetType => typeof(TTarget);
        public string PropertyName { get; }

        public override void Add(TTarget item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                Emit(item, null, RelatedListChangeKind.Add);
            }
        }

        public override void RemoveAt(int index)
        {
            var item = this[index];
            base.RemoveAt(index);
            Emit(item, null, RelatedListChangeKind.Remove);
        }

        public override bool Remove(TTarget item)
        {
            var result = base.Remove(item);
#if !TypeScript
            Emit(item, null, RelatedListChangeKind.Remove);
#endif
            return result;
        }
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

        public void AssignRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.AssignByKey);
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

        private void Emit(TTarget item, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            Changed.Emit(() => new RelatedListChangeEvent<TSource, TTarget>(Owner, item, itemKey, kind, this));
        }
    }
}