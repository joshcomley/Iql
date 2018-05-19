using System;
using Iql.Data.Lists;
using Iql.Entities;

namespace Iql.Data.Events
{
    public class RelatedListChangeEvent<TSource, TRelated> : IRelatedListChangeEvent
        where TRelated : class 
        where TSource : class
    {
        public ObservableListChangeEvent<TRelated> ObservableListChangeEvent { get; }
        IObservableListChangeEvent IRelatedListChangeEvent.ObservableListChangeEvent => ObservableListChangeEvent;
        public Type ItemType => typeof(TRelated);
        public Type OwnerType => typeof(TSource);
        public TSource Owner { get; set; }
        public TRelated Item => (TRelated) ObservableListChangeEvent?.Item;
        public CompositeKey ItemKey { get; }
        public RelatedListChangeKind Kind { get; set; }
        public RelatedList<TSource, TRelated> List { get; set; }

        object IRelatedListChangeEvent.Owner => Owner;

        object IRelatedListChangeEvent.Item => Item;

        RelatedListChangeKind IRelatedListChangeEvent.Kind => Kind;

        IRelatedList IRelatedListChangeEvent.List => List;

        public RelatedListChangeEvent(
            ObservableListChangeEvent<TRelated> observableListChangeEvent,
            TSource owner, 
            CompositeKey itemKey, 
            RelatedListChangeKind kind, 
            RelatedList<TSource, TRelated> list
            )
        {
            ObservableListChangeEvent = observableListChangeEvent;
            Owner = owner;
            ItemKey = itemKey;
            Kind = kind;
            List = list;
        }
    }
}