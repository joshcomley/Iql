using System;
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

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return Expressions.Any(e => e != null && e.IsOrHas(matches));
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAggregateExpression(null);
			if(Expressions == null)
			{
				expression.Expressions = null;
			}
			else
			{
				var listCopy = new List<IqlExpression>();
				for(var i = 0; i < Expressions.Count; i++)
				{
					listCopy.Add(Expressions[i]?.Clone());
				}
				expression.Expressions = listCopy;
			}
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
