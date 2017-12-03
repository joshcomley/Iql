using System;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class RelatedListChange<TSource, TRelation> : IRelatedListChange
        where TRelation : class
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