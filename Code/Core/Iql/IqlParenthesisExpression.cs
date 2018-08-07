using System.Collections.Generic;
using System;

namespace Iql
{
    public class IqlParenthesisExpression : IqlExpression
    {
        public IqlParenthesisExpression(
            IqlExpression expression) : base(IqlExpressionKind.Parenthesis)
        {
            Expression = expression;
        }

        public IqlParenthesisExpression() : this(null)
        {
        }

        public IqlExpression Expression { get; set; }

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return Expression.IsOrHas(matches);
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlParenthesisExpression(null);
			expression.Expression = Expression?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Expression?.FlattenInternal(expressions);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
