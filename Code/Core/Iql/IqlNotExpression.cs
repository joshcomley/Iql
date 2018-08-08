using System.Collections.Generic;
using System;

namespace Iql
{
    public class IqlNotExpression : IqlExpression
    {
        public IqlNotExpression(
            IqlExpression expression) : base(IqlExpressionKind.Not)
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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlNotExpression(null);
			expression.Expression = Expression?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				Expression?.FlattenInternal(expressions, checker);
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
