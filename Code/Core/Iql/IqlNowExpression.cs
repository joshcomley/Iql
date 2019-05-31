using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlNowExpression : IqlSpecialValueExpression
    {
        public IqlNowExpression() : base(IqlExpressionKind.Now)
        {
            ReturnType = IqlType.Date;
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

		public static IqlNowExpression Clone(IqlNowExpression source)
		{
			// #CloneStart

			var expression = new IqlNowExpression();
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
