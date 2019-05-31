using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlAllExpression : IqlAnyAllExpression
    {
        public IqlAllExpression(
            string rootVariableName = null,
            IqlReferenceExpression parent = null,
            IqlLambdaExpression expression = null) : base(rootVariableName, IqlExpressionKind.All, parent, expression)
        {
        }

        public IqlAllExpression() : this(null, null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAllExpression();
			expression.RootVariableName = RootVariableName;
			expression.Value = (IqlLambdaExpression)Value?.Clone();
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
    }
}
