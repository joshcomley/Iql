using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlCurrentLocationExpression : IqlSpecialValueExpression
    {
        public IqlCurrentLocationExpression() : base(IqlExpressionKind.CurrentLocation)
        {
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlCurrentLocationExpression();
			expression.CanFail = CanFail;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

            // #CloneEnd
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
    }
}
