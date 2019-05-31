using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringTrimExpression : IqlReferenceExpression
    {
        public IqlStringTrimExpression(IqlReferenceExpression parent = null) : base(IqlExpressionKind.StringTrim,
            IqlType.String, parent)
        {
        }

        public IqlStringTrimExpression() : this(null)
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

		public static IqlStringTrimExpression Clone(IqlStringTrimExpression source)
		{
			// #CloneStart

			var expression = new IqlStringTrimExpression();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
