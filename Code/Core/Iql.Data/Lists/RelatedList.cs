using System;
using System.Collections.Generic;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Events;
using Iql.Data.Events;
using Iql.Data.Extensions;

namespace Iql.Data.Lists
{
    public class RelatedList<TSource, TTarget> : EntityList<TTarget>, IRelatedList
        where TTarget : class
        where TSource : class
    {
        public RelatedList(
            TSource owner = null, 
            string property = null, 
            IEnumerable<TTarget> source = null)
        {
            Owner = owner;
            PropertyName = property;
            this.Initialize(source);
            Change.Subscribe(e =>
            {
                switch (e.Kind)
                {
                    case ObservableListChangeKind.Adding:
                        EmitRelatedListEvent(e, null, RelatedListChangeKind.Adding);
                        break;
                    case ObservableListChangeKind.Added:
                        EmitRelatedListEvent(e, null, RelatedListChangeKind.Added);
                        break;
                    case ObservableListChangeKind.Removed:
                        EmitRelatedListEvent(e, null, RelatedListChangeKind.Removed);
                        break;
                    case ObservableListChangeKind.Removing:
                        EmitRelatedListEvent(e, null, RelatedListChangeKind.Removing);
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

        object IRelatedList.Add(object item)
        {
            return base.Add(item);
        }

        object IRelatedList.Owner => Owner;

        IEventSubscriber<IRelatedListChangeEvent> IRelatedList.RelatedListChange => RelatedListChange;

        public void AssignRelationshipByKey(CompositeKey key)
        {
            EmitRelatedListEvent(null, key, RelatedListChangeKind.AssignByKey);
        }

        public void RemoveRelationshipByKey(CompositeKey key)
        {
            EmitRelatedListEvent(null, key, RelatedListChangeKind.Removed);
        }

        private void EmitRelatedListEvent(ObservableListChangeEvent<TTarget> observableListChangeEvent, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            RelatedListChange.Emit(() => new RelatedListChangeEvent<TSource, TTarget>(observableListChangeEvent, Owner, itemKey, kind, this));
        }
    }
}