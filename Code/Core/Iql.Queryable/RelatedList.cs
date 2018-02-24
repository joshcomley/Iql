using System;
using System.Collections.Generic;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedList<TSource, TTarget> : EntityList<TTarget>, IRelatedList
        where TTarget : class
        where TSource : class
    {
        public RelatedList(TSource owner = null, string property = null, IEnumerable<TTarget> source = null)
        {
            Owner = owner;
            PropertyName = property;
            this.Initialize(source);
            Change.Subscribe(e =>
            {
                switch (e.Kind)
                {
                    case ObservableListChangeKind.Added:
                        EmitRelatedListEvent((TTarget)e.Item, null, RelatedListChangeKind.Add);
                        break;
                    case ObservableListChangeKind.Removed:
                        EmitRelatedListEvent((TTarget)e.Item, null, RelatedListChangeKind.Remove);
                        break;
                }
            });
        }

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> RelatedListChange { get; }
            = new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

        public TSource Owner { get; }
        public Type OwnerType => typeof(TSource);
        public Type TargetType => typeof(TTarget);
        public string PropertyName { get; }

        object IRelatedList.Owner => Owner;

        IEventSubscriber<IRelatedListChangeEvent> IRelatedList.RelatedListChange => RelatedListChange;

        public void AssignRelationshipByKey(CompositeKey key)
        {
            EmitRelatedListEvent(null, key, RelatedListChangeKind.AssignByKey);
        }

        public void RemoveRelationshipByKey(CompositeKey key)
        {
            EmitRelatedListEvent(null, key, RelatedListChangeKind.Remove);
        }

        private void EmitRelatedListEvent(TTarget item, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            RelatedListChange.Emit(() => new RelatedListChangeEvent<TSource, TTarget>(Owner, item, itemKey, kind, this));
        }
    }
}