using System;
using Iql.Queryable;

namespace Iql.DotNet.Queryable.Applicators
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