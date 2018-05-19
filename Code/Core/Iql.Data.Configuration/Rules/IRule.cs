using System.Linq.Expressions;

namespace Iql.Data.Configuration.Rules
{
    public interface IRule
    {
        string Key { get; }
        string Message { get; }
        LambdaExpression Expression { get; }
    }
}