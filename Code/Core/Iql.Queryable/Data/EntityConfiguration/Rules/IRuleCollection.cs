using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public interface IRuleCollection
    {
        IEnumerable<IRule> All { get; }
        IRule Get(string key);
        IRule Add(Expression<Func<object, bool>> expression, string key, string message);
        void Remove(string key);
    }
}