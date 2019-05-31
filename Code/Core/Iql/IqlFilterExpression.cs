using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFilterExpression : IqlParentValueLambdaExpression
    {
        public IqlFilterExpression(
            IqlReferenceExpression parent = null,
            IqlLambdaExpression expression = null) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
        {
        }

        public IqlFilterExpression()
            : base(null, null, IqlExpressionKind.Count, IqlType.Integer)
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

			Value = (IqlLambdaExpression)context.Replace(this, nameof(Value), null, Value);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }

		public static IqlFilterExpression Clone(IqlFilterExpression source)
		{
			// #CloneStart

			var expression = new IqlFilterExpression();
			expression.RootVariableName = source.RootVariableName;
			expression.Value = (IqlLambdaExpression)source.Value?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
