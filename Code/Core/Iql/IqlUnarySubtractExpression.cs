using Iql.Extensions;
using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlUnarySubtractExpression : IqlUnaryExpression
    {
        public IqlUnarySubtractExpression(object value = null) : base(value, IqlExpressionKind.UnarySubtract)
        {
        }

        public IqlUnarySubtractExpression() : this(null)
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

		public static IqlUnarySubtractExpression Clone(IqlUnarySubtractExpression source)
		{
			// #CloneStart

			var expression = new IqlUnarySubtractExpression();
			expression.Value = source.Value?.TryCloneIql();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
