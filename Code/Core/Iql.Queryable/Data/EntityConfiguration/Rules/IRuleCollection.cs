using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public interface IRuleCollection<out TRule>
        where TRule : IRule
    {
        IEnumerable<TRule> All { get; }
        TRule Get(string key);
        TRule Add(object rule);
        void Remove(string key);
    }
}