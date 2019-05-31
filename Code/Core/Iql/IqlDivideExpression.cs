using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDivideExpression : IqlBinaryExpression
    {
        public IqlDivideExpression(
            IqlExpression left = null,
            IqlExpression right = null) : base(IqlExpressionKind.Divide, left, right)
        {
        }

        public IqlDivideExpression() : this(null, null)
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

		public static IqlDivideExpression Clone(IqlDivideExpression source)
		{
			// #CloneStart

			var expression = new IqlDivideExpression();
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
