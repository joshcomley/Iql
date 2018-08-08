using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlReferenceExpression
    {
        public IqlStringToUpperCaseExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringToUpperCase,
            IqlType.String, parent)
        {
        }

        public IqlStringToUpperCaseExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringToUpperCaseExpression(null);
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
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
