using System;

namespace Iql.Data.Configuration.Rules
{
    public interface IRuleBase<out TResult> : IRule
    {
        Func<object, TResult> Run { get; }
    }
}