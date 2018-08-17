using System.Linq.Expressions;
using Iql.Entities.Rules;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public abstract class RuleBase : IRule
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public LambdaExpression Expression { get; set; }
        public IqlExpression ExpressionIql { get; set; }
    }
}