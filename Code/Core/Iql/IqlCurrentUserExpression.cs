using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlCurrentUserExpression : IqlSpecialValueExpression
    {
        public IqlCurrentUserExpression() : base(IqlExpressionKind.CurrentUser)
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

		public static IqlCurrentUserExpression Clone(IqlCurrentUserExpression source)
		{
			// #CloneStart

			var expression = new IqlCurrentUserExpression();
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
