using System;

namespace Iql.Entities.Rules
{
    public interface IRuleBase<out TResult> : IRule
    {
        Func<object, TResult> Run { get; }
    }
}