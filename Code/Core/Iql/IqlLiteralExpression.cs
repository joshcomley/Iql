using Iql.Extensions;
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

		public static IqlLiteralExpression Clone(IqlLiteralExpression source)
		{
			// #CloneStart

			var expression = new IqlLiteralExpression();
			expression.Value = source.Value?.TryCloneIql();
			expression.InferredReturnType = source.InferredReturnType;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
