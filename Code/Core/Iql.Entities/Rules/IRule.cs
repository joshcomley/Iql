using System.Linq.Expressions;

namespace Iql.Entities.Rules
{
    public interface IRule
    {
        string Key { get; }
        string Message { get; }
        LambdaExpression Expression { get; }
    }
}