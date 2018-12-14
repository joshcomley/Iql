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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlNowExpression();
			expression.CanFail = CanFail;
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
