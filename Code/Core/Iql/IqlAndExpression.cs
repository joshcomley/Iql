using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlAndExpression : IqlBinaryExpression
    {
        public IqlAndExpression(
            IqlExpression left = null,
            IqlExpression right = null) :
            base(IqlExpressionKind.And, left, right)
        {
        }

        public IqlAndExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAndExpression(null, null);
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
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

				context.Flatten(Left);
				context.Flatten(Right);
				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Left = context.Replace(this, nameof(Left), null, Left);
			Right = context.Replace(this, nameof(Right), null, Right);
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
