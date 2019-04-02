using System.Collections.Generic;
using System;

namespace Iql
{
    public class IqlNewGuidExpression : IqlSpecialValueExpression
    {
        public static Func<Guid> NewGuid = () => Guid.NewGuid();

        public IqlNewGuidExpression() : base(IqlExpressionKind.NewGuid)
        {
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlNewGuidExpression();
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
