using System;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Events
{
    public class RelatedListChangeEvent<TSource, TRelated> : IRelatedListChangeEvent
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

        object IRelatedListChangeEvent.Owner => Owner;

        object IRelatedListChangeEvent.Item => Item;

        RelatedListChangeKind IRelatedListChangeEvent.Kind => Kind;

        IRelatedList IRelatedListChangeEvent.List => List;

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