using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public interface IRule
    {
        string Key { get; }
        string Message { get; }
        LambdaExpression Expression { get; }
    }
}