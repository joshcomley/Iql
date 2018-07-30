using Iql.Entities.Rules;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public abstract class RuleBase : IRule
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public LambdaExpression Expression { get; set; }
        public IqlExpression ExpressionIql { get; set; }
    }
}