using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlNowTicksExpression : IqlSpecialValueExpression
    {
        public IqlNowTicksExpression() : base(IqlExpressionKind.NowTicks)
        {
            ReturnType = IqlType.Integer;
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

        public static IqlNowTicksExpression Clone(IqlNowTicksExpression source)
        {
            // #CloneStart

			var expression = new IqlNowTicksExpression();
			expression.CanFail = source.CanFail;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

            // #CloneEnd
        }
    }
}
