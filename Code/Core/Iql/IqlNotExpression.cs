using System.Collections.Generic;
using System;

namespace Iql
{
    public class IqlNotExpression : IqlExpression
    {
        public IqlNotExpression(
            IqlExpression expression = null) : base(IqlExpressionKind.Not)
        {
            Expression = expression;
        }

        public IqlNotExpression() : this(null)
        {
        }

        public IqlExpression Expression { get; set; }

        public override bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            return Expression.IsOrHas(matches);
        }


		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Expression);
				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			Expression = context.Replace(this, nameof(Expression), null, Expression);
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

			// #ReplaceEnd
		}

		public static IqlNotExpression Clone(IqlNotExpression source)
		{
			// #CloneStart

			var expression = new IqlNotExpression();
			expression.Expression = source.Expression?.Clone();
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
