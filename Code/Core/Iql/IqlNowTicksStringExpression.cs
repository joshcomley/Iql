using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlNowTicksStringExpression : IqlSpecialValueExpression
    {
        public IqlNowTicksStringExpression() : base(IqlExpressionKind.NowTicksString)
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

        public static IqlNowTicksStringExpression Clone(IqlNowTicksStringExpression source)
        {
            // #CloneStart

			var expression = new IqlNowTicksStringExpression();
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
