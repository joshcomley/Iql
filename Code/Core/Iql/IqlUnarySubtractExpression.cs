using System.Collections.Generic;

namespace Iql
{
    public class IqlUnarySubtractExpression : IqlUnaryExpression
    {
        public IqlUnarySubtractExpression(object value) : base(value, IqlExpressionKind.UnarySubtract)
        {
        }

        public IqlUnarySubtractExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlUnarySubtractExpression(null);
			expression.Value = Value;
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
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
