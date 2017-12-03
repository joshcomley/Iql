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

        void IRelatedList.AddChange(IRelatedListChange change)
        {
            AddChange((RelatedListChange<TSource, T>) change);
        }
        void IRelatedList.RemoveChange(IRelatedListChange change)
        {
            RemoveChange((RelatedListChange<TSource, T>) change);
        }

        public EventEmitter<RelatedListChangedEvent<TSource, T>> Changed { get; }
            = new EventEmitter<RelatedListChangedEvent<TSource, T>>();

        public TSource Owner { get; }

        object IRelatedList.Owner => Owner;

        IEnumerable<IRelatedListChange> IRelatedList.GetChanges()
        {
            return GetChanges();
        }

        IEventEmitterBase IRelatedList.Changed => Changed;

        public RelatedList(TSource owner, IEnumerable<T> source = null)
        {
            Owner = owner;
            this.Initialize(source);
        }

        public void AssignRelationship(T item)
        {
            Emit(item, null, RelatedListChangeKind.Assign);
        }

        private void Emit(T item, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            Changed.Emit(new RelatedListChangedEvent<TSource, T>(Owner, item, itemKey, kind, this));
        }

        public void RemoveRelationship(T item)
        {
            Emit(item, null, RelatedListChangeKind.Remove);
        }

        void IRelatedList.AssignRelationship(object item)
        {
            AssignRelationship((T)item);
        }

        void IRelatedList.RemoveRelationship(object item)
        {
            RemoveRelationship((T)item);
        }

        public void AssignRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Assign);
        }

        public void RemoveRelationshipByKey(CompositeKey key)
        {
            Emit(null, key, RelatedListChangeKind.Remove);
        }

        void IRelatedList.AssignRelationshipByKey(CompositeKey item)
        {
            throw new System.NotImplementedException();
        }

        void IRelatedList.RemoveRelationshipByKey(CompositeKey item)
        {
            throw new System.NotImplementedException();
        }
    }
}