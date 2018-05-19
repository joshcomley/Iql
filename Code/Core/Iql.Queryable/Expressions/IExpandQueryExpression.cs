using System;

namespace Iql.Queryable.Expressions
{
    public interface IExpandQueryExpression
    {
        Func<IQueryableBase, IQueryableBase> Queryable { get; }
    }
}