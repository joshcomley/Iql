using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringIndexOfExpression : IqlParentValueExpression
    {
        public IqlStringIndexOfExpression(
            IqlReferenceExpression parent = null,
            IqlReferenceExpression value = null)
            : base(parent, value, IqlExpressionKind.StringIndexOf, IqlType.Integer)
        {
        }

        public IqlStringIndexOfExpression() : this(null, null)
        {
        }


        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				context.Flatten(Value);
				context.Flatten(Parent);

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			Value = context.Replace(this, nameof(Value), null, Value);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }

		public static IqlStringIndexOfExpression Clone(IqlStringIndexOfExpression source)
		{
			// #CloneStart

			var expression = new IqlStringIndexOfExpression();
			expression.Value = source.Value?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
