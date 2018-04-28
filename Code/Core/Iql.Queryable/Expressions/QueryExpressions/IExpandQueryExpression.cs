using System;
using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public interface IExpandQueryExpression
    {
        Func<IQueryableBase, IQueryableBase> Queryable { get; }
    }
}