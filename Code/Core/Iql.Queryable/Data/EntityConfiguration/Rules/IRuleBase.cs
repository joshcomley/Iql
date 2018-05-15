using System;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public interface IRuleBase<out TResult> : IRule
    {
        Func<object, TResult> Run { get; }
    }
}