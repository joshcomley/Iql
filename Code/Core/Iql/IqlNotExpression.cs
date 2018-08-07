using System;

namespace Iql
{
    public class IqlNotExpression : IqlExpression
    {
        public IqlNotExpression(
            IqlExpression expression) : base(IqlExpressionKind.Not)
        {
            Expression = expression;
        }

        public IqlNotExpression() : this(null)
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

			var expression = new IqlNotExpression(null);
			expression.Expression = Expression?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
