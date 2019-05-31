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

		public static IqlNewGuidExpression Clone(IqlNewGuidExpression source)
		{
			// #CloneStart

			var expression = new IqlNewGuidExpression();
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
