using System;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedListChangeEvent<TSource, TRelated> : IRelatedListChangedEvent
        where TRelated : class 
        where TSource : class
    {
        public Type ItemType => typeof(TRelated);
        public Type OwnerType => typeof(TSource);
        public TSource Owner { get; set; }
        public TRelated Item { get; set; }
        public CompositeKey ItemKey { get; }
        public RelatedListChangeKind Kind { get; set; }
        public RelatedList<TSource, TRelated> List { get; set; }

        object IRelatedListChangedEvent.Owner => Owner;

        object IRelatedListChangedEvent.Item => Item;

        RelatedListChangeKind IRelatedListChangedEvent.Kind => Kind;

        IRelatedList IRelatedListChangedEvent.List => List;

        public RelatedListChangeEvent(
            TSource owner, 
            TRelated item,
            CompositeKey itemKey, 
            RelatedListChangeKind kind, 
            RelatedList<TSource, TRelated> list
            )
        {
            Owner = owner;
            Item = item;
            ItemKey = itemKey;
            Kind = kind;
            List = list;
        }
    }
}