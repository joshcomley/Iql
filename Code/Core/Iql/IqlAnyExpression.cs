using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlAnyExpression : IqlAnyAllExpression
    {
        public IqlAnyExpression(
            string rootVariableName = null,
            IqlReferenceExpression parent = null,
            IqlLambdaExpression expression = null) : base(rootVariableName, IqlExpressionKind.Any, parent, expression)
        {
        }

        public IqlAnyExpression() : this(null, null, null)
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

		public static IqlAnyExpression Clone(IqlAnyExpression source)
		{
			// #CloneStart

			var expression = new IqlAnyExpression();
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
