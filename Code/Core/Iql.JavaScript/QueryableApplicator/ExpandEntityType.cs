using System;
using Iql.Queryable;
using Iql.Queryable.Data.Queryable;

namespace Iql.JavaScript.QueryableApplicator
{
    public class ExpandEntityType
    {
        public ExpandEntityType(Type queryableType, IQueryableBase queryable = null)
        {
            QueryableType = queryableType;
            Queryable = queryable;
        }

        public Type QueryableType { get; }
        public IQueryableBase Queryable { get; }
    }
}