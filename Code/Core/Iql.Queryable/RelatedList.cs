using System.Collections.Generic;
using Iql.Queryable.Events;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedList<TSource, T> : List<T>, IRelatedList
        where T : class
    {
        private readonly IList<RelatedListChange<TSource, T>> _changes = new List<RelatedListChange<TSource, T>>();

        public RelatedList(TSource owner, string property, IEnumerable<T> source = null)
        {
            Owner = owner;
            Property = property;
            this.Initialize(source);
        }

        public EventEmitter<RelatedListChangeEvent<TSource, T>> Changed { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, T>>();

        public EventEmitter<RelatedListChangeEvent<TSource, T>> Changing { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, T>>();

        public TSource Owner { get; }
        public string Property { get; }

        void IRelatedList.AddChange(IRelatedListChange change)
        {
            AddChange((RelatedListChange<TSource, T>) change);
        }

        void IRelatedList.RemoveChange(IRelatedListChange change)
        {
            RemoveChange((RelatedListChange<TSource, T>) change);
        }

        object IRelatedList.Owner => Owner;

        IEnumerable<IRelatedListChange> IRelatedList.GetChanges()
        {
            return GetChanges();
        }

        IEventSubscriber<IRelatedListChangedEvent> IRelatedList.Changed => Changed;
        IEventSubscriber<IRelatedListChangedEvent> IRelatedList.Changing => Changing;

        void IRelatedList.AssignRelationship(object item)
        {
            AssignRelationship((T) item);
        }

        void IRelatedList.RemoveRelationship(object item)
        {
            RemoveRelationship((T) item);
        }

        public void AssignRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Assign);
        }

        public void RemoveRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Remove);
        }

        public IList<RelatedListChange<TSource, T>> GetChanges()
        {
            return _changes;
        }

        public void AddChange(RelatedListChange<TSource, T> change)
        {
            var existingChange = _changes.FindMatchingChange(change.ItemKey);
            if (existingChange != null)
            {
                if (change.Kind != existingChange.Kind)
                {
                    _changes.Remove(existingChange);
                }
            }
            else
            {
                _changes.Add(change);
            }
        }

        public void RemoveChange(RelatedListChange<TSource, T> change)
        {
            var existingChange = _changes.FindMatchingChange(change.ItemKey);
            if (existingChange != null)
            {
                _changes.Remove(existingChange);
            }
        }

        public void AssignRelationship(T item)
        {
            Emit(item, null, RelatedListChangeKind.Assign);
        }

        private void Emit(T item, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            Changed.Emit(new RelatedListChangeEvent<TSource, T>(Owner, item, itemKey, kind, this));
        }

        public void RemoveRelationship(T item)
        {
            Emit(item, null, RelatedListChangeKind.Remove);
        }
    }
}