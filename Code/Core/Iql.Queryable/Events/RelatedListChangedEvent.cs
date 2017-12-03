using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedListChangedEvent<TSource, TRelated> : IRelatedListChangedEvent
        where TRelated : class
    {
        public TSource Owner { get; set; }
        public TRelated Item { get; set; }
        public CompositeKey ItemKey { get; }
        public RelatedListChangeKind Kind { get; set; }
        public RelatedList<TSource, TRelated> List { get; set; }

        object IRelatedListChangedEvent.Owner => Owner;

        object IRelatedListChangedEvent.Item => Item;

        RelatedListChangeKind IRelatedListChangedEvent.Kind => Kind;

        IList IRelatedListChangedEvent.List => List;

        public RelatedListChangedEvent(
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