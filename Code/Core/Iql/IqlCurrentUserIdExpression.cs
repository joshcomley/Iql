using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlCurrentUserIdExpression : IqlSpecialValueExpression
    {
        public IqlCurrentUserIdExpression() : base(IqlExpressionKind.CurrentUserId)
        {
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

        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(Parent);

            // #FlattenEnd
        }

		public static IqlCurrentUserIdExpression Clone(IqlCurrentUserIdExpression source)
		{
			// #CloneStart

			var expression = new IqlCurrentUserIdExpression();
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
