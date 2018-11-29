using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlLiteralExpression : IqlLiteralExpressionBase<object>
    {
        public IqlLiteralExpression(
            object value = null, IqlType type = IqlType.Unknown) : base(IqlExpressionKind.Literal,
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

			var expression = new IqlLiteralExpression();
			expression.Value = Value;
			expression.InferredReturnType = InferredReturnType;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}
    }
}
