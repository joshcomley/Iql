using System.Collections.Generic;

namespace Iql.Entities.Rules
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