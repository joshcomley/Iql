using System;
using Iql.Data.Events;
using Iql.Entities;

namespace Iql.Data.Lists
{
    public class RelatedListChange<TSource, TRelation> : IRelatedListChange
        where TRelation : class 
        where TSource : class
    {
        public RelationshipMatch Relationship { get; }
        public CompositeKey ItemKey { get; }
        public TSource Owner { get; }
        public RelatedList<TSource, TRelation> List { get; }
        public TRelation Item { get; }
        public RelatedListChangeKind Kind { get; }

        object IRelatedListChange.Owner => Owner;
        IRelatedList IRelatedListChange.List => List;
        object IRelatedListChange.Item => ItemKey;

        public RelatedListChange(
            RelatedListChangeKind kind,
            RelationshipMatch relationship,
            CompositeKey itemKey,
            TRelation item,
            TSource owner,
            RelatedList<TSource, TRelation> list
            )
        {
            if (owner == null && itemKey == null)
            {
                throw new ArgumentException($"Either {nameof(item)} or {nameof(itemKey)} must be provided when instantiating a RelationshipChange");
            }
            Relationship = relationship;
            ItemKey = itemKey;
            Owner = owner;
            List = list;
            Item = item;
            Kind = kind;
            if (ItemKey == null)
            {
                ItemKey = Relationship.ThisEnd.GetCompositeKey(item);
            }
        }
    }
}