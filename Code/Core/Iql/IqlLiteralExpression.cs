using System.Collections.Generic;

namespace Iql
{
    public class IqlLiteralExpression : IqlLiteralExpressionBase<object>
    {
        public IqlLiteralExpression(
            object value, IqlType type = IqlType.Unknown) : base(IqlExpressionKind.Literal,
            type)
        {
            Value = value;
        }

        public IqlLiteralExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlLiteralExpression(null);
			expression.Value = Value;
			expression.InferredReturnType = InferredReturnType;
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
