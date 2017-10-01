using System;

namespace Iql.Queryable.Expressions.QueryExpressions
{
    public interface IExpandQueryExpression
    {
        Func<IQueryableBase, IQueryableBase> GetQueryable();
    }
}