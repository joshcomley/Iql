using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public interface IRule
    {
        string Key { get; }
        string Message { get; }
        Expression<Func<object, bool>> Expression { get; }
        Func<object, bool> Run { get; }
    }
}