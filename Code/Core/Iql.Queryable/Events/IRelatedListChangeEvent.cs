using System;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Events
{
    public interface IRelatedListChangeEvent
    {
        Type OwnerType { get; }
        object Owner { get; }
        Type ItemType { get; }
        object Item { get; }
        RelatedListChangeKind Kind { get; }
        IRelatedList List { get; }
        CompositeKey ItemKey { get; }
    }
}