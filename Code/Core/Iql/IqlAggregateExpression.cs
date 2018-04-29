using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlAggregateExpression : IqlExpression
    {
        public IqlAggregateExpression(
            params IqlExpression[] expressions)
            : base(IqlExpressionKind.Aggregate)
        {
            Expressions = expressions.ToList();
        }

        public IqlAggregateExpression() : this(null)
        {
        }

        public List<IqlExpression> Expressions { get; set; }

        public override bool ContainsRootEntity()
        {
            return Expressions.Any(e => e != null && e.ContainsRootEntity());
        }
    }
}