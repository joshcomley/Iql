using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlBitwiseNotExpression : IqlBinaryExpression
    {
        public IqlBitwiseNotExpression(
            IqlExpression left = null,
            IqlExpression right = null) : base(IqlExpressionKind.BitwiseNot, left, right)
        {
        }

        public IqlBitwiseNotExpression() : this(null, null)
        {
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

		public static IqlBitwiseNotExpression Clone(IqlBitwiseNotExpression source)
		{
			// #CloneStart

			var expression = new IqlBitwiseNotExpression();
			expression.Left = source.Left?.Clone();
			expression.Right = source.Right?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
