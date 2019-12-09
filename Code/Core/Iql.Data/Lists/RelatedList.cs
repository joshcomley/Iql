using System;
using System.Collections.Generic;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.Lists.Events;
using Iql.Events;

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
        }

        protected override ObservableListChangeEvent<TTarget> EmitEvent(TTarget item, ObservableListChangeKind kind, Func<ObservableListChangeEvent<TTarget>> eventObjectFactory)
        {
            var ev = base.EmitEvent(item, kind, eventObjectFactory);
            ObservableListChangeEvent<TTarget> evUs = null;
            switch (kind)
            {
                case ObservableListChangeKind.Adding:
                    evUs = EmitRelatedListEvent(eventObjectFactory, null, RelatedListChangeKind.Adding);
                    break;
                case ObservableListChangeKind.Added:
                    evUs = EmitRelatedListEvent(eventObjectFactory, null, RelatedListChangeKind.Added);
                    break;
                case ObservableListChangeKind.Removed:
                    evUs = EmitRelatedListEvent(eventObjectFactory, null, RelatedListChangeKind.Removed);
                    break;
                case ObservableListChangeKind.Removing:
                    evUs = EmitRelatedListEvent(eventObjectFactory, null, RelatedListChangeKind.Removing);
                    break;
            }

            return ev ?? evUs;
        }

        private EventEmitter<RelatedListChangeEvent<TSource, TTarget>> _relatedListChange;

        public EventEmitter<RelatedListChangeEvent<TSource, TTarget>> RelatedListChange => _relatedListChange = _relatedListChange ?? new EventEmitter<RelatedListChangeEvent<TSource, TTarget>>();

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

        private ObservableListChangeEvent<TTarget> EmitRelatedListEvent(Func<ObservableListChangeEvent<TTarget>> observableListChangeEvent, CompositeKey itemKey, RelatedListChangeKind kind)
        {
            if (_relatedListChange != null)
            {
                ObservableListChangeEvent<TTarget> ev = null;
                _relatedListChange.EmitIfExists(() => new RelatedListChangeEvent<TSource, TTarget>(ev = (observableListChangeEvent == null ? null : observableListChangeEvent()), Owner, itemKey, kind, this));
                return ev;
            }

            return null;
        }
    }
}
