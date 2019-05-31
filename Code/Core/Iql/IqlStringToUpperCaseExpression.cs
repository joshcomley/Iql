using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlReferenceExpression
    {
        public IqlStringToUpperCaseExpression(IqlReferenceExpression parent = null) : base(IqlExpressionKind.StringToUpperCase,
            IqlType.String, parent)
        {
        }

        public IqlStringToUpperCaseExpression() : this(null)
        {
        }


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Parent);

			// #FlattenEnd
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

		public static IqlStringToUpperCaseExpression Clone(IqlStringToUpperCaseExpression source)
		{
			// #CloneStart

			var expression = new IqlStringToUpperCaseExpression();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
